using Newtonsoft.Json;
using System.Text.RegularExpressions;
using CalculatorLibrary.Models;
using System.IO;

namespace CalculatorLibrary;

public class CalculatorEngine
{
    public double CalculateResult(double number1, double number2, string operation, string previousScreen = "MainMenu")
    {
        string operationString = "";
        string operationName = "";
        double result = double.NaN;

        if (previousScreen == "MainMenu")
        {
            switch (operation.ToLower())
            {
                case "a":
                    result = number1 + number2;
                    operationName = "Addition";
                    break;
                case "s":
                    result = number2 - number1;
                    operationName = "Subtraction";
                    break;
                case "m":
                    result = number1 * number2;
                    operationName = "Multiplication";
                    break;
                case "d":
                    result = number1 / number2;
                    operationName = "Division";
                    break;
                case "p10":
                    result = number1 * Math.Pow(10, number2);
                    operationName = "Mulitplicaton of power of 10";
                    break;
                case "p":
                    result = Math.Pow(number1, number2);
                    operationName = "Power";
                    break;
                case "sr":
                    result = Math.Pow(number1, 0.5d);
                    operationName = "Square root";
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
                    break;
                case "c":
                    result = Math.Cos(number1 * Math.PI / 180.0d);
                    operationName = "Cosine function";
                    break;
                case "t":
                    if ((number1 + 90) % 180 != 0)
                        result = Math.Tan(number1 * Math.PI / 180.0d);
                    operationName = "Tangent function";
                    break;
                case "as":
                    result = Math.Asin(number1) * 180.0d / Math.PI;
                    operationName = "Arcsine function";
                    break;
                case "ac":
                    result = Math.Acos(number1) * 180.0d / Math.PI;
                    operationName = "Arccosine function";
                    break;
                case "at":
                    result = Math.Atan(number1) * 180.0d / Math.PI;
                    operationName = "Arctangent function";
                    break;
            }
        }
        LogResults(number1, number2, result, operationName);
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

    private void LogResults(double operandOne, double operandTwo, double result, string operation)
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
                FirstOperand = operandOne,
                SecondOperand = operandTwo,
                Result = result,
                Operation = operation
            });
           
            serializer.Serialize(writeStream, OperationalData.previousOperations);
        }     
    }
}
public class HelperMethods
{
    static public string ReadNumericInput(ref double number, string ordinalString, bool divisor = false, bool trigonometricValue = false, bool radicant = false, bool power10 = false, bool specialInput = false)
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
                    if (Regex.IsMatch(userInput.ToLower(), @"^(e|p)$"))
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
        Console.WriteLine($"{"Date:".PadRight(16)}{"Time:".PadRight(12)}{"Operation:".PadRight(25)}{"Result:".PadRight(25)}");
        Console.WriteLine();
        foreach (OperationalData.DataFormat data in OperationalData.previousOperations)
        {
            Console.WriteLine($"{data.OperationDate.ToString("dd/MM/yyyy").PadRight(16)}{data.OperationDate.ToString("hh:mm").PadRight(12)}{data.Operation.PadRight(25)}{data.Result.ToString().PadRight(25)}");
        }
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

