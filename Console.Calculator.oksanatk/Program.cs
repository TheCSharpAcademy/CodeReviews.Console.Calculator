using Spectre.Console;
using CalculatorLibrary;
using Microsoft.CognitiveServices.Speech;
using System.Text.RegularExpressions;

bool SpeechRecognitionInput = false;
string? speechKey = Environment.GetEnvironmentVariable("Azure_SpeechSDK_Key");
string? speechRegion = Environment.GetEnvironmentVariable("Azure_SpeechSDK_Region");
SpeechConfig speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
speechConfig.SpeechRecognitionLanguage = "en-US";

string? readResult;
string[] validOperations = new string[] { "add", "sub", "mult", "div", "sq", "pow", "10x", "sin", "cos", "tan" };
string[] singleNumberOperations = new string[] { "sq", "10x", "sin", "cos", "tan" };
Calculator calculator = new Calculator();
bool endApp = false;

if (args.Contains("--voice-input"))
{
    SpeechRecognitionInput = true;
}

await ShowMainMenu(SpeechRecognitionInput);

async Task ShowMainMenu(bool voiceMode)
{
    while (!endApp)
    {
        double num1 = double.NaN;
        double num2 = double.NaN;
        string op = "";
        double result = double.NaN;

        do
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold yellow]Console Calculator in C#[/]");
            AnsiConsole.MarkupLine("[bold yellow]------------------------\n[/]");

            Panel operationsPanel = ShowOperationsPanel(voiceMode);
            AnsiConsole.Write(operationsPanel);

            if (voiceMode)
            {
                AnsiConsole.MarkupLine("[bold yellow]OR[/] \nSay [aqua]number[/] to see the number of times the calculator has been used.");
                AnsiConsole.MarkupLine("Say [aqua]calculations[/] to see the previous calculations");
                AnsiConsole.MarkupLine("Say [aqua]exit[/] to exit the app.");
                AnsiConsole.Markup("\n[bold yellow]Your option? [/]");

                op = await GetVoiceInput();
                op = ParseVoiceOperator(op);
            }
            else
            {
                AnsiConsole.MarkupLine("[bold yellow]OR[/] \nEnter [aqua]num[/] to see the number of times the calculator has been used.");
                AnsiConsole.MarkupLine("Enter [aqua]calc[/] to see the previous calculations");
                AnsiConsole.MarkupLine("Enter [aqua]exit[/] to exit the app.");
                AnsiConsole.Markup("\n[bold yellow]Your option? [/]");

                readResult = Console.ReadLine();
                if (readResult != null)
                {
                    op = readResult.Trim().ToLower();
                }
            }
            if (op == "exit") { endApp = true; }
            else if (op.StartsWith("num"))
            {
                AnsiConsole.MarkupLine("\nThe calculator has been used [bold yellow]{0}[/] times.", calculator.TimesUsed);
                if (voiceMode)
                {
                    AnsiConsole.MarkupLine("\nPlease wait a few seconds to continue back to the main menu.");
                    Thread.Sleep(3500);
                }
                else
                {
                    AnsiConsole.MarkupLine("\nPress the [yellow]Enter[/] key to continue back to the main menu.");
                    Console.ReadLine();
                }
            }
            else if (op.StartsWith("calc"))
            {
                Console.Clear();
                AnsiConsole.MarkupLine("You are choosing to view previous operations.");
                AnsiConsole.Write(calculator.ShowPreviousCalculations(calculator.RecentCalculations));

                if (voiceMode)
                {
                    AnsiConsole.MarkupLine("\nSay [aqua]Clear[/] to clear the history of recent calculations, or say [yellow]Continue[/] to continue.");
                    readResult = GetVoiceInput().Result;
                }
                else
                {
                    AnsiConsole.MarkupLine("\nEnter [aqua]Clear[/] to clear the history of recent calculations, or just press the [yellow]Enter[/] key to continue.");

                    readResult = Console.ReadLine();
                    if (readResult != null)
                    {
                        readResult = readResult.Trim().ToLower();
                    }
                }

                if (readResult != null && readResult.Contains("clear"))
                {
                    calculator.ClearCalculations();
                }
            }
            else if (validOperations.Contains(op))
            {
                AnsiConsole.MarkupLine("You're choosing the [bold yellow]{0}[/] operation.", op);
            }
            else
            {
                if (voiceMode)
                {
                    AnsiConsole.MarkupLine("[maroon]I'm sorry, but I didn't understand that operator. Press try again in a few seconds.[/]");
                    Thread.Sleep(2500);
                }
                else
                {
                    AnsiConsole.MarkupLine("[maroon]I'm sorry, but I didn't understand that operator. Press [yellow]Enter[/] to try again.[/]");
                    Console.ReadLine();
                }
            }
        } while (!validOperations.Contains(op) && op != "exit");

        if (op != "exit" && op != null)
        {
            num1 = GetNum(false, voiceMode);

            // if not single-num operation, get num2
            if (!singleNumberOperations.Contains(op))
            {
                num2 = GetNum(true, voiceMode);
            }

            result = calculator.DoOperation(num1, num2, op);
            if (double.IsNaN(result))
            {
                AnsiConsole.MarkupLine("[bold maroon]This operation results in a mathematical error.[/]");
            }
            else AnsiConsole.MarkupLine("\nYour result is: [bold yellow]{0}[/]\n", result);

            AnsiConsole.MarkupLine("\t------------------------\n");

            if (voiceMode)
            {
                AnsiConsole.Markup("Say [yellow]exit[/] to close the app, or say [bold yellow]Continue[/]. ");
                if (GetVoiceInput().Result == "exit") endApp = true;
            }
            else
            {
                AnsiConsole.Markup("Enter [yellow]exit[/] to close the app, or press the [bold yellow]Enter[/] key to continue. ");
                if (Console.ReadLine() == "exit") endApp = true;
            }
            AnsiConsole.MarkupLine("\n");
        }
    }
    calculator.Finish();
}

string ParseVoiceOperator(string voiceOperator)
{
    switch (voiceOperator)
    {
        case "addition":
            return "add";
        case string op when voiceOperator.StartsWith("sub"):
            return "sub";
        case string op when voiceOperator.StartsWith("mult"):
            return "mult";
        case string op when voiceOperator.StartsWith("div"):
            return "div";
        case string op when voiceOperator.Contains("square"):
            return "sq";
        case string op when voiceOperator.Contains("pow"):
            return "pow";
        case string op when ((voiceOperator.Contains("10") || voiceOperator.Contains("ten")) && voiceOperator.Contains("x")):
            return "10x";
        // "sine" operator commonly heard by speech recognizer as "sign"
        case "sign":
            return "sin";
        case string op when voiceOperator.StartsWith("si"):
            return "sin";
        case "cosine":
            return "cos";
        case string op when voiceOperator.StartsWith("tan"):
            return "tan";

        default:
            return voiceOperator;
    }
}

double GetNum(bool gettingSecondNum, bool voiceMode)
{
    double userNum = double.NaN;
    bool validNum = false;
    do
    {
        if (voiceMode)
        {
            if (gettingSecondNum)
            {
                AnsiConsole.MarkupLine("\nPlease say a second number to enter.");
            }
            else
            {
                AnsiConsole.MarkupLine("\nPlease say a number to enter.");
            }
            AnsiConsole.MarkupLine("OR say [aqua]calculations[/] to view the previous calculations and results.");
            readResult = GetVoiceInput().Result;
        }
        else
        {
            if (gettingSecondNum)
            {
                AnsiConsole.MarkupLine("\nPlease enter a second number.");
            }
            else
            {
                AnsiConsole.MarkupLine("\nPlease enter a number.");
            }
            AnsiConsole.MarkupLine("OR enter [aqua]calc[/] to view the previous calculations and results.");
            readResult = Console.ReadLine();
        }

        if (readResult != null)
        {
            if (readResult.StartsWith("calc"))
            {
                AnsiConsole.Write(calculator.ShowPreviousCalculations(calculator.RecentCalculations));
                AnsiConsole.MarkupLine("[yellow]\n\t---------------------------\n[/]");

                if (voiceMode)
                {
                    AnsiConsole.MarkupLine("Say [yellow]calculation #[/] to use the result of a previous calculation. IE, [yellow]calculation 1[/]");
                    AnsiConsole.MarkupLine("OR say any number (##) to use it instead.");

                    readResult = GetVoiceInput().Result;
                }
                else
                {
                    AnsiConsole.MarkupLine("Type [yellow]calculation #[/] to use the result of a previous calculation. IE, [yellow]calculation 1[/]");
                    AnsiConsole.MarkupLine("OR enter any number (##) to use it instead.");

                    readResult = Console.ReadLine();
                }

                if (readResult != null && readResult.StartsWith("calc"))
                {
                    string[] parseCalculationNumber = readResult.Split(' ');
                    int previousCalculation = 0;
                    if (int.TryParse(parseCalculationNumber[1], out previousCalculation))
                    {
                        if ((previousCalculation > 0) && (previousCalculation <= calculator.RecentCalculations.Count))
                        {
                            userNum = calculator.RecentCalculations[(previousCalculation - 1)].Result;
                            AnsiConsole.MarkupLine($"Recording your number as [bold yellow]{userNum}[/]");
                            return userNum;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[maroon]Sorry, but we couldn't find a corresponding calculation result.[/]");
                        }
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[maroon]Sorry, but I couldn't understand that number. Press the [yellow]Enter[/] key to try again.[/]");
                    }
                }
            }

            if (double.TryParse(readResult, out userNum))
            {
                validNum = true;
            }
            else if (readResult != null && !readResult.StartsWith("calc"))
            {
                if (voiceMode)
                {
                    AnsiConsole.MarkupLine("[maroon]Sorry, but I couldn't understand that number. Please try again.[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine("[maroon]Sorry, but I couldn't understand that number. Press the [yellow]Enter[/] key to try again.[/]");
                    Console.ReadLine();
                }
            }
        }
    } while (!validNum);
    return userNum;
}

Panel ShowOperationsPanel(bool voiceMode)
{
    Grid grid = new Grid();
    grid.AddColumn();

    if (voiceMode)
    {
        grid.AddRow("[aqua]add[/]      - Add");
        grid.AddRow("[aqua]subtract[/] - Subtract");
        grid.AddRow("[aqua]multiply[/] - Multiply");
        grid.AddRow("[aqua]divide[/]   - Divide");
        grid.AddRow("[aqua]square[/]   - Square Root of a Number");
        grid.AddRow("[aqua]power[/]    - Raise to the Power");
        grid.AddRow("[aqua]10x[/]      - Multiply by 10");
        grid.AddRow("[aqua]sine[/]     - Sine of a Number");
        grid.AddRow("[aqua]cosine[/]   - Cosine of a Number");
        grid.AddRow("[aqua]tangent[/]  - Tangent of a Number");

        return new Panel(grid)
        {
            Header = new PanelHeader("[yellow]Choose an Operation:[/]"),
            Border = BoxBorder.Rounded,
            Padding = new Padding(1)
        };
    }
    else
    {
        grid.AddRow("[aqua]add[/]  - Add");
        grid.AddRow("[aqua]sub[/]  - Subtract");
        grid.AddRow("[aqua]mult[/] - Multiply");
        grid.AddRow("[aqua]div[/]  - Divide");
        grid.AddRow("[aqua]sq[/]   - Square Root of a Number");
        grid.AddRow("[aqua]pow[/]  - Raise to the Power");
        grid.AddRow("[aqua]10x[/]  - 10");
        grid.AddRow("[aqua]sin[/]  - Sine of a Number");
        grid.AddRow("[aqua]cos[/]  - Cosine of a Number");
        grid.AddRow("[aqua]tan[/]  - Tangent of a Number");

        return new Panel(grid)
        {
            Header = new PanelHeader("[yellow]Choose an Operation:[/]"),
            Border = BoxBorder.Rounded,
            Padding = new Padding(1)
        };
    }
}

async Task<String> GetVoiceInput()
{
    string userVoiceInput = "";
    using SpeechRecognizer recognizer = new SpeechRecognizer(speechConfig);
    RecognitionResult recognizedInput;
    bool unrecognizedMessagePrinted = false;

    do
    {
        recognizedInput = await recognizer.RecognizeOnceAsync();

        if (recognizedInput.Reason == ResultReason.RecognizedSpeech)
        {
            userVoiceInput = recognizedInput.Text;
            AnsiConsole.MarkupLine($"[bold yellow]Recognized Voice Input[/]: {userVoiceInput}");

            userVoiceInput = userVoiceInput.Trim().ToLower();
            if (userVoiceInput.EndsWith("."))
            {
                userVoiceInput = userVoiceInput.Substring(0, userVoiceInput.Length - 1);
            }
            userVoiceInput = Regex.Replace(userVoiceInput, @"[^a-z0-9\s-.]", "");
            await Task.Delay(1000);

            return userVoiceInput;
        }
        else if (recognizedInput.Reason == ResultReason.Canceled)
        {
            AnsiConsole.MarkupLine($"[maroon]SPEECH RECOGNITION CANCELLED:[/] {CancellationDetails.FromResult(recognizedInput)}\n");
            endApp = true;
            return "exit";
        }
        else if (recognizedInput.Reason == ResultReason.NoMatch)
        {
            // print error message, but only once.
            if (!unrecognizedMessagePrinted)
            {
                AnsiConsole.MarkupLine("\n[maroon]I'm sorry, I didn't understand that input.[/]");
                unrecognizedMessagePrinted = true;
            }
        }
        else
        {
            AnsiConsole.MarkupLine(recognizedInput.Reason.ToString());
            AnsiConsole.MarkupLine("[maroon]I'm sorry, I didn't understand that input.[/]");
        }
    } while (recognizedInput.Reason != ResultReason.RecognizedSpeech);
    return userVoiceInput;
}
