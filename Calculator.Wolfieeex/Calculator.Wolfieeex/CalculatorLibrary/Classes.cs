using System.Text.RegularExpressions;
using System.Diagnostics;

namespace CalculatorLibrary;

public class Classes
{
    public class CalculatorEngine
    {
        public CalculatorEngine() 
        {
            StreamWriter logFile = File.CreateText("calculator.log");
            Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            Trace.AutoFlush = true;
            Trace.WriteLine("Starting Calculator Log");
            Trace.WriteLine(String.Format("Started {0}", System.DateTime.Now.ToString()));
        }
        public static string GetMathematicalSign(string operation)
        {
            switch (operation.ToLower())
            {
                case "a":
                    return "+";
                case "s":
                    return "-";
                case "m":
                    return "*";
                case "d":
                    return "/";
            }
            return "";
        }
        public double CalculateResult(ref double number1, ref double number2, string operation)
        {
            switch (operation.ToLower())
            {
                case "a":
                    return number1 + number2;
                case "s":
                    return number1 - number2;
                case "m":
                    return number1 * number2;
                case "d":
                    if (number2 == 0)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                        Console.Write($"\rYour divisor must not equal 0. Please try again- reinsert your second number and press ENTER: ");
                        HelperMethods.ReadNumericInput(ref number2, "second", true);
                    }
                    return number1 / number2;
            }
            return double.NaN;
        }
    }
    public class HelperMethods
    {
        static public void ReadNumericInput(ref double number, string ordinalString, bool divisor = false)
        {
            bool checkNumber = true;
            while (checkNumber)
            {
                string? userInput = Console.ReadLine();
                ordinalString = ordinalString == "" ? ordinalString : ordinalString.Trim() + " ";

                if (string.IsNullOrEmpty(userInput))
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                    Console.Write($"\rYour input was empty. Please try again- insert your {ordinalString}number and press ENTER: ");
                    continue;
                }
                if (!double.TryParse(userInput, out number))
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                    Console.Write($"\rYour input must not include letters or symbols. Please try again- insert your {ordinalString}number and press ENTER: ");
                    continue;
                }
                if (divisor && number == 0)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                    Console.Write($"\rYour divisor must not equal 0. Please try again- reinsert your {ordinalString}number and press ENTER: ");
                    continue;
                }
                checkNumber = false;
            }
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
            Console.Write($"Please type in your {ordinalString} number, and then press ENTER: ");
        }

    }
}
