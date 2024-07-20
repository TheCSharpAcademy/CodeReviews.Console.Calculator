using CalculatorLibrary;
using Calculator.Wolfieeex.Models;

namespace Calculator.Wolfieeex;

public enum Screens
{
    MainMenu,
    Trigonometry,
    Calculation,
    PreviousOperations,
    ClearScreen
}

public class ScreenEngine
{


    private Screens queuedScreen;
    private Screens previousScreen;
    private string? nextCalculation;
    private bool runScreensLoop;

    public void RunScreens()
    {
        queuedScreen = Screens.MainMenu;

        runScreensLoop = true;
        while (runScreensLoop)
        {
            switch (queuedScreen)
            {
                case Screens.MainMenu:
                    MainMenu();
                    break;
                case Screens.Trigonometry:
                    TrigonometryMenu();
                    break;
                case Screens.Calculation:
                    CalculateScreen(nextCalculation);
                    break;
                case Screens.PreviousOperations:
                    ViewPreviousCalculations();
                    break;
                case Screens.ClearScreen:
                    ClearPreviousOperations();
                    break;
            }
            Console.Clear();
        }
    }

    private void Exit()
    {
        runScreensLoop = false;
    }

    private void ClearPreviousOperations()
    {
        Console.WriteLine("This operation will clear all previous calculation data. Are you sure you want to proceed? (Y/N)");
        Console.WriteLine($"{new string('-', Console.BufferWidth)}");
        Console.Write("Your option: ");
        string? userOption = null;
        HelperMethods.ReadMatchingInput(ref userOption, @"^y|n$");

        if (userOption.ToLower() == "y")
        {
            if (File.Exists("operationsLog.json"))
                HelperMethods.DeletePreviousOperations();
            Console.Write($"\nAll previous calculations have been deleted. Please press Enter to return to main menu: ");
        }
        else
        {
            Console.Write("\nOperation cancelled. Press Enter to return to main menu: ");
        }
        Console.ReadLine();
        queuedScreen = Screens.MainMenu;
    }

    private void ViewPreviousCalculations()
    {
        if (File.Exists("operationsLog.json"))
        {
            HelperMethods.DisplayPreviousOperations();
            Console.WriteLine();
            Console.WriteLine($"{new string('-', Console.BufferWidth)}");
            Console.Write("Please press Enter to return to main menu: ");
        }
        else
        {
            Console.Write("There are no previous operations to display. Please press Enter to return to main menu: ");
        }
        Console.ReadLine();
        queuedScreen = Screens.MainMenu;
    }

    private void CalculateScreen(string calculationType)
    {
        double firstNumber = double.NaN;
        double secondNumber = double.NaN;
        string stringTypeInput;

        int previousY = Console.CursorTop;

        bool calculatorScreenWhile = true;
        while (calculatorScreenWhile)
        {
            if (previousScreen == Screens.MainMenu)
            {
                string operationsCount = HelperMethods.ReturnOperationsCount().ToString();

                Console.Write($"You are currently on the {OperationalDetails.menuOptions[calculationType.ToLower()].Name.ToLower()} screen.");
                Console.WriteLine(("This calculator was used a total of: " + operationsCount + (operationsCount != "1" ? " times" : " time")).PadLeft(60));
                Console.WriteLine($"{new string('-', Console.BufferWidth)}");
                Console.WriteLine();

                previousY = Console.CursorTop;
                Console.SetCursorPosition(0, 12);
                Console.WriteLine($"{new string('-', Console.BufferWidth)}");
                Console.WriteLine("Optional Input: ");
                Console.WriteLine("P - Choose a previous calculator result for your current opertaion");
                Console.WriteLine("E - Return to Main Menu");
                Console.SetCursorPosition(0, previousY);

                HelperMethods.AskForNumber(OperationalDetails.menuOptions[calculationType.ToLower()].OperandOne);
                if (double.IsNaN(firstNumber))
                {
                    if (calculationType.ToLower() == "sr")
                    {
                        stringTypeInput = HelperMethods.ReadNumericInput(ref firstNumber, OperationalDetails.menuOptions[calculationType.ToLower()].OperandOne, radicant: true, specialInput: true, specialInputRegex: @"^(e|p)$");
                        if (stringTypeInput.ToLower() == "p")
                        {
                            firstNumber = HelperMethods.PreviousResultSelectionScreen(radicant: true);
                            Console.Clear();
                            continue;
                        }
                    }
                    else
                    {
                        stringTypeInput = HelperMethods.ReadNumericInput(ref firstNumber, OperationalDetails.menuOptions[calculationType.ToLower()].OperandOne, specialInput: true, specialInputRegex: @"^(e|p)$");
                    }
                    if (stringTypeInput.ToLower() == "e")
                    {
                        previousScreen = queuedScreen;
                        queuedScreen = Screens.MainMenu;
                        return;
                    }
                    if (stringTypeInput.ToLower() == "p")
                    {
                        firstNumber = HelperMethods.PreviousResultSelectionScreen();
                        Console.Clear();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine(firstNumber);
                }

                if (!String.IsNullOrEmpty(OperationalDetails.menuOptions[calculationType.ToLower()].OperandTwo))
                {
                    previousY = Console.CursorTop;
                    Console.SetCursorPosition(0, 16);
                    Console.Write($"D - Reinsert {OperationalDetails.menuOptions[calculationType.ToLower()].OperandOne.ToLower()}");
                    Console.SetCursorPosition(0, previousY);

                    HelperMethods.AskForNumber(OperationalDetails.menuOptions[calculationType.ToLower()].OperandTwo);
                    if (double.IsNaN(secondNumber))
                    {
                        if (calculationType.ToLower() == "d")
                        {
                            stringTypeInput = HelperMethods.ReadNumericInput(ref secondNumber, OperationalDetails.menuOptions[calculationType.ToLower()].OperandTwo, divisor: true, specialInput: true, specialInputRegex: @"^(e|p|d)$");
                            if (stringTypeInput.ToLower() == "p")
                            {
                                secondNumber = HelperMethods.PreviousResultSelectionScreen(divisor: true);
                                Console.Clear();
                                continue;
                            }
                        }
                        else if (calculationType.ToLower() == "p10")
                        {
                            stringTypeInput = HelperMethods.ReadNumericInput(ref secondNumber, OperationalDetails.menuOptions[calculationType.ToLower()].OperandTwo, power10: true, specialInput: true, specialInputRegex: @"^(e|p|d)$");
                            if (stringTypeInput.ToLower() == "p")
                            {
                                secondNumber = HelperMethods.PreviousResultSelectionScreen(power10: true);
                                Console.Clear();
                                continue;
                            }
                        }
                        else
                        {
                            stringTypeInput = HelperMethods.ReadNumericInput(ref secondNumber, OperationalDetails.menuOptions[calculationType.ToLower()].OperandTwo, specialInput: true, specialInputRegex: @"^(e|p|d)$");
                        }
                        if (stringTypeInput.ToLower() == "e")
                        {
                            previousScreen = queuedScreen;
                            queuedScreen = Screens.MainMenu;
                            return;
                        }
                        if (stringTypeInput.ToLower() == "d")
                        {
                            firstNumber = double.NaN;
                            secondNumber = double.NaN;
                            Console.Clear();
                            continue;
                        }
                        if (stringTypeInput.ToLower() == "p")
                        {
                            secondNumber = HelperMethods.PreviousResultSelectionScreen();
                            Console.Clear();
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine(secondNumber);
                    }
                }
            }
            else
            {
                string operationsCount = HelperMethods.ReturnOperationsCount().ToString();
                Console.Write($"You are currently on the {OperationalDetails.trigonometryOptions[calculationType.ToLower()].Name.ToLower()} screen.");
                Console.WriteLine(("This calculator was used a total of: " + operationsCount + (operationsCount != "1" ? " times" : " time")).PadLeft(60));
                Console.WriteLine($"{new string('-', Console.BufferWidth)}");
                Console.WriteLine();

                previousY = Console.CursorTop;
                Console.SetCursorPosition(0, 13);
                Console.WriteLine($"{new string('-', Console.BufferWidth)}");
                Console.WriteLine("Optional Input: ");
                Console.WriteLine("P - Choose a previous calculator result for your current opertaion");
                Console.WriteLine("E - Return to Main Menu");
                Console.SetCursorPosition(0, previousY);

                HelperMethods.AskForNumber(OperationalDetails.trigonometryOptions[calculationType.ToLower()].OperandOne);

                if (double.IsNaN(firstNumber))
                {
                    if (calculationType.ToLower()[0] != 'a' || calculationType.ToLower() == "at")
                    {
                        stringTypeInput = HelperMethods.ReadNumericInput(ref firstNumber, OperationalDetails.trigonometryOptions[calculationType.ToLower()].OperandOne, specialInput: true, specialInputRegex: @"^(e|p)$");
                    }
                    else
                    {
                        stringTypeInput = HelperMethods.ReadNumericInput(ref firstNumber, OperationalDetails.trigonometryOptions[calculationType.ToLower()].OperandOne, trigonometricValue: true, specialInput: true, specialInputRegex: @"^(e|p)$");
                        if (stringTypeInput.ToLower() == "p")
                        {
                            firstNumber = HelperMethods.PreviousResultSelectionScreen(trigonometricValue: true);
                            Console.Clear();
                            continue;
                        }
                    }
                    if (stringTypeInput.ToLower() == "e")
                    {
                        previousScreen = queuedScreen;
                        queuedScreen = Screens.MainMenu;
                        return;
                    }
                    if (stringTypeInput.ToLower() == "p")
                    {
                        firstNumber = HelperMethods.PreviousResultSelectionScreen();
                        Console.Clear();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine(firstNumber);
                }
            }
            calculatorScreenWhile = false;
        }

        previousY = Console.CursorTop;
        Console.SetCursorPosition(0, 12);
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"{new string(' ', Console.BufferWidth)}");
        }
        Console.SetCursorPosition(0, previousY);

        Console.WriteLine($"{new string('-', Console.BufferWidth)}");

        CalculatorEngine calculatorEngine = new CalculatorEngine();
        double result = calculatorEngine.CalculateResult(firstNumber, secondNumber, calculationType, Enum.GetName(previousScreen));

        string output = !Double.IsNaN(result) ? String.Format("{0:0.####}", result) : "Not a valid number";
        if (output == "-0")
            output = "0";

        if (previousScreen == Screens.MainMenu)
            Console.WriteLine($"The {OperationalDetails.menuOptions[calculationType.ToLower()].ResultingOperand.ToLower()}: {output}");
        else
            Console.WriteLine($"The {OperationalDetails.trigonometryOptions[calculationType.ToLower()].ResultingOperand.ToLower()}: {output}");

        Console.Write("\n\nPress ENTER to continue: ");
        Console.ReadLine();

        previousScreen = queuedScreen;
        queuedScreen = Screens.MainMenu;
    }

    private void MainMenu()
    {
        Console.WriteLine("Welcome to Console Calculator in C#!\r");
        Console.WriteLine($"{new string('-', Console.BufferWidth)}\n");

        string regexCheckString = @"^(";

        Console.WriteLine("Calculator main menu: \n");
        foreach (KeyValuePair<string, Operation> record in OperationalDetails.menuOptions)
        {
            Console.WriteLine(record.Key.ToUpper() + "\t - " + record.Value.Name);
            regexCheckString += record.Key.ToLower() + "|";
        }
        Console.WriteLine($"{new string('-', Console.BufferWidth)}\n");
        Console.WriteLine($"V\t - View previous operations");
        Console.WriteLine($"C\t - Clear previous operations");
        Console.WriteLine($"E\t - Exit the program");
        regexCheckString += "e|v|c)$";
        Console.WriteLine($"{new string('-', Console.BufferWidth)}\n");
        Console.Write($"Please select your option: ");

        string? userOption = null;
        HelperMethods.ReadMatchingInput(ref userOption, regexCheckString);

        previousScreen = queuedScreen;
        switch (userOption.ToLower())
        {
            case "e":
                Exit();
                break;
            case "t":
                queuedScreen = Screens.Trigonometry;
                break;
            case "v":
                queuedScreen = Screens.PreviousOperations;
                break;
            case "c":
                queuedScreen = Screens.ClearScreen;
                break;
            default:
                nextCalculation = userOption;
                queuedScreen = Screens.Calculation;
                break;
        }
    }

    private void TrigonometryMenu()
    {
        string regexCheckString = @"^(";

        Console.WriteLine("Trigonometric functions menu: ");
        Console.WriteLine($"{new string('-', Console.BufferWidth)}");
        Console.WriteLine();
        foreach (KeyValuePair<string, Operation> record in OperationalDetails.trigonometryOptions)
        {
            Console.WriteLine(record.Key.ToUpper() + "\t - " + record.Value.Name);
            regexCheckString += record.Key.ToLower() + "|";
        }
        Console.WriteLine($"E\t - Return to main menu\n");
        regexCheckString += "e)$";
        Console.WriteLine($"{new string('-', Console.BufferWidth)}\n");
        Console.Write($"Please select your option: ");

        string? userOption = null;
        HelperMethods.ReadMatchingInput(ref userOption, regexCheckString);

        previousScreen = queuedScreen;
        switch (userOption.ToLower())
        {
            case "e":
                queuedScreen = Screens.MainMenu;
                break;
            default:
                nextCalculation = userOption;
                queuedScreen = Screens.Calculation;
                break;
        }
    }
}
