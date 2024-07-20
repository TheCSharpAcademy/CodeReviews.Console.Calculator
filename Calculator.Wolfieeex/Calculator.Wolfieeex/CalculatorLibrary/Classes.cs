using Newtonsoft.Json;
using System.Text.RegularExpressions;
using CalculatorLibrary.Models;
using System.IO;

namespace CalculatorLibrary;

public class CalculatorEngine
{
    public double CalculateResult(double number1, double number2, string operation, string previousScreen = "MainMenu")
    {
        string operationName = "";
        string calculationString = "";
        double result = double.NaN;

        if (previousScreen == "MainMenu")
        {
            switch (operation.ToLower())
            {
                case "a":
                    result = number1 + number2;
                    operationName = "Addition";
                    calculationString = $"{number1} + {number2}";
                    break;
                case "s":
                    result = number1 - number2;
                    operationName = "Subtraction";
                    calculationString = $"{number1} - {number2}";
                    break;
                case "m":
                    result = number1 * number2;
                    operationName = "Multiplication";
                    calculationString = $"{number1} * {number2}";
                    break;
                case "d":
                    result = number1 / number2;
                    operationName = "Division";
                    calculationString = $"{number1} / {number2}";
                    break;
                case "p10":
                    result = number1 * Math.Pow(10, number2);
                    operationName = "10th power multiplicand";
                    calculationString = $"{number1} * 10";
                    foreach (char letter in number2.ToString())
                    {
                        string escapeSequence = "207" + letter; 
                        int escapeCode = int.Parse(escapeSequence, System.Globalization.NumberStyles.HexNumber);
                        calculationString += char.ConvertFromUtf32(escapeCode);
                    }
                    break;
                case "p":
                    result = Math.Pow(number1, number2);
                    operationName = "Power";
                    calculationString = $"{number1}";
                    foreach (char letter in number2.ToString())
                    {
                        string escapeSequence = "207" + letter;
                        int escapeCode = int.Parse(escapeSequence, System.Globalization.NumberStyles.HexNumber);
                        calculationString += char.ConvertFromUtf32(escapeCode);
                    }
                    break;
                case "sr":
                    result = Math.Pow(number1, 0.5d);
                    operationName = "Square root";
                    calculationString = $"\u221A{number1}";
                    break;
            }
        }
        else
        {
            switch (operation.ToLower())
            {
                case "s":
                    result = Math.Sin(number1 * Math.PI / 180.0d);
                    operationName = "Sine function";
                    calculationString = $"Sin({number1}\u00B0)";
                    break;
                case "c":
                    result = Math.Cos(number1 * Math.PI / 180.0d);
                    operationName = "Cosine function";
                    calculationString = $"Cos({number1}\u00B0)";
                    break;
                case "t":
                    if ((number1 + 90) % 180 != 0)
                        result = Math.Tan(number1 * Math.PI / 180.0d);
                    operationName = "Tangent function";
                    calculationString = $"Tan({number1}\u00B0)";
                    break;
                case "as":
                    result = Math.Asin(number1) * 180.0d / Math.PI;
                    operationName = "Arcsine function";
                    calculationString = $"Arcsin({number1})";
                    break;
                case "ac":
                    result = Math.Acos(number1) * 180.0d / Math.PI;
                    operationName = "Arccosine function";
                    calculationString = $"Arccos({number1})";
                    break;
                case "at":
                    result = Math.Atan(number1) * 180.0d / Math.PI;
                    operationName = "Arctangent function";
                    calculationString = $"Arctan({number1})";
                    break;
            }
        }
        LogResults(calculationString, result, operationName);
        UpdateCalculatorOperationsCount();
        return result;
    }

    private void UpdateCalculatorOperationsCount()
    {
        if (!File.Exists("calculatorOperationsCount.log"))
        {
            FileStream file = File.Create("calculatorOperationsCount.log");
            file.Close();
        }
        string? stringNumber = File.ReadAllText("calculatorOperationsCount.log");
        if (String.IsNullOrEmpty(stringNumber) == null)
        {
            File.WriteAllText("calculatorOperationsCount.log", "1");
        }
        else
        {
            int number;
            if (int.TryParse(stringNumber, out number))
            {
                number++;
                File.WriteAllText("calculatorOperationsCount.log", number.ToString());
            }
            else
            {
                File.WriteAllText("calculatorOperationsCount.log", "1");
            }
        }
    }

    private void LogResults(string operationString, double result, string operation)
    {
        string path = "operationsLog.json";

        JsonSerializer serializer = new JsonSerializer();
        serializer.Formatting = Formatting.Indented;

        if (File.Exists(path))
        {
            using (StreamReader openStream = File.OpenText(path))
            {
                JsonTextReader jsonReader = new JsonTextReader(openStream);
                OperationalData.previousOperations = serializer.Deserialize<List<OperationalData.DataFormat>>(jsonReader);
            }                          
        }

        using (StreamWriter writeStream = File.CreateText(path))
        {
            OperationalData.previousOperations.Add(new OperationalData.DataFormat
            {
                OperationDate = DateTime.Now,
                OperationString = operationString,
                Result = Math.Round(result, 4),
                Operation = operation
            });
           
            serializer.Serialize(writeStream, OperationalData.previousOperations);
        }     
    }
}
public class HelperMethods
{
    static public string ReadNumericInput(ref double number, string ordinalString, bool divisor = false, bool trigonometricValue = false, bool radicant = false, bool power10 = false, bool specialInput = false, string specialInputRegex = "")
    {
        bool checkNumber = true;
        while (checkNumber)
        {
            string? userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                Console.Write($"\rYour input was empty. Please try again- insert {ordinalString.ToLower()} and press ENTER: ");
                continue;
            }
            if (!double.TryParse(userInput, out number))
            {
                if (specialInput)
                {
                    if (Regex.IsMatch(userInput.ToLower(), specialInputRegex))
                    {
                        return userInput;
                    }
                }
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                Console.Write($"\rYour input is not a number nor a valid option. Please try again- insert {ordinalString.ToLower()} and press ENTER: ");
                continue;
            }
            if (divisor && number == 0)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                Console.Write($"\rYour divisor must not equal 0. Please try again- reinsert {ordinalString.ToLower()} and press ENTER: ");
                continue;
            }
            if (trigonometricValue && (number < -1 || number > 1))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                Console.Write($"\rTrigonometric value needs to be in range of -1 and 1. Please try again- reinsert {ordinalString.ToLower()} and press ENTER: ");
                continue;
            }
            if (radicant && number < 0)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                Console.Write($"\rYour radicant must not be lower than 0. Please try again- reinsert {ordinalString.ToLower()} and press ENTER: ");
                continue;
            }
            if (power10 && number != (int)number)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                Console.Write($"\rYour exponent of power of 10 must be a whole number. Please try again- reinsert {ordinalString.ToLower()} and press ENTER: ");
                continue;
            }
            checkNumber = false;
        }
        return "";
    }
    static public void ReadMatchingInput(ref string? letter, string RegexOptionsPattern)
    {
        bool checkNumber = true;
        while (checkNumber)
        {
            string? userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                Console.Write($"\rYour input was empty. Please try to select your option again and press ENTER: ");
                continue;
            }
            if (!Regex.IsMatch(userInput.ToLower(), RegexOptionsPattern))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                Console.Write($"\rYour selection doesn't match any of the options above. Please try to select your option again and press ENTER: ");
                continue;
            }
            checkNumber = false;
            letter = userInput;
        }
    }
    static public void AskForNumber(string ordinalString)
    {
        Console.Write($"Please type in your {ordinalString.ToLower()}, and then press ENTER: ");
    }
    static public void DeletePreviousOperations()
    {
        if (File.Exists("operationsLog.json"))
        {
            File.Delete("operationsLog.json");
            File.Delete("calculatorOperationsCount.log");
            OperationalData.previousOperations.Clear();
        }
    }
    static public void DisplayPreviousOperations()
    {
        string path = "operationsLog.json";

        if (File.Exists(path))
        {
            using (JsonTextReader jsonTextReader = new JsonTextReader(File.OpenText(path)))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                OperationalData.previousOperations = jsonSerializer.Deserialize<List<OperationalData.DataFormat>>(jsonTextReader);
            }
        }
        Console.WriteLine("Previous operations: ");
        Console.WriteLine($"{new string('-', Console.BufferWidth)}");
        Console.WriteLine();

        int longestOperationIterationLength = 10;
        foreach (OperationalData.DataFormat data in OperationalData.previousOperations)
        {
            if (data.OperationString.Length > longestOperationIterationLength)
            {
                longestOperationIterationLength = data.OperationString.Length;
            }
        }

        Console.WriteLine($"{"Date:".PadRight(16)}{"Time:".PadRight(12)}{"Operation:".PadRight(longestOperationIterationLength + 3)}{"Result:".PadRight(25)}");
        Console.WriteLine();
        foreach (OperationalData.DataFormat data in OperationalData.previousOperations)
        {
            string stringResult = data.Result.ToString();
            if (stringResult == "NaN") stringResult = "Undefined";
            if (stringResult == "-0") stringResult = "0";
            Console.WriteLine($"{data.OperationDate.ToString("dd/MM/yyyy").PadRight(16)}{data.OperationDate.ToString("hh:mm").PadRight(12)}{data.OperationString.PadRight(longestOperationIterationLength + 3)}{stringResult.PadRight(25)}");
        }
    }
    static public double PreviousResultSelectionScreen()
    {
        string path = "operationsLog.json";

        if (File.Exists(path))
        {
            using (JsonTextReader jsonTextReader = new JsonTextReader(File.OpenText(path)))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                OperationalData.previousOperations = jsonSerializer.Deserialize<List<OperationalData.DataFormat>>(jsonTextReader);
            }
        }

        Console.Clear();
        Console.WriteLine("You are on a previous opeations screen.");
        Console.Write("Choose a result from any previous operation by typing in its index number to use it for your current calculation: ");

        int typingY = Console.CursorTop;
        int typingX = Console.CursorLeft;

        Console.WriteLine("\nE - Return to the calculator screen without choosing a value");
        Console.WriteLine($"{new string('-', Console.BufferWidth)}");
        Console.WriteLine();
        Console.WriteLine("Last 10 previous operations displaying:\n");

        int longestOperationIterationLength = 10;
        foreach (OperationalData.DataFormat data in OperationalData.previousOperations)
        {
            if (data.OperationString.Length > longestOperationIterationLength)
            {
                longestOperationIterationLength = data.OperationString.Length;
            }
        }
    
        Console.WriteLine($"{"Index number".PadRight(20)}{"Operation:".PadRight(longestOperationIterationLength + 3)}{"Result:".PadRight(25)}\n");

        int indexN = 1;
        foreach (OperationalData.DataFormat data in OperationalData.previousOperations)
        {
            string stringResult = data.Result.ToString();
            if (stringResult == "NaN") stringResult = "Undefined";
            if (stringResult == "-0") stringResult = "0";
            Console.WriteLine($"{indexN.ToString().PadRight(20)}{data.OperationString.PadRight(longestOperationIterationLength + 3)}{stringResult.PadRight(25)}");
            indexN++;
        }
        Console.ReadKey();
        return 0;
    }
    static public int ReturnOperationsCount()
    {
        if (!File.Exists("calculatorOperationsCount.log"))
        {
            FileStream file = File.Create("calculatorOperationsCount.log");
            file.Close();
        }

        string? stringNumber = File.ReadAllText("calculatorOperationsCount.log");
        if (String.IsNullOrEmpty(stringNumber))
        {
            return 0;
        }
        else if (int.TryParse(stringNumber, out int result))
        {
            return result;
        }
        else
        {
            return 0;
        }
    }
}

