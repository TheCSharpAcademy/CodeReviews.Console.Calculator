using System;
using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            int calculationCount = 0;
            List<CalculationEntry> history = new List<CalculationEntry>();
            Calculator calculator = new Calculator();

            while (!endApp)
            {
                string? numInput1 = "";
                string? numInput2 = "";

                CalculationEntry record = new CalculationEntry();
                record.Date = DateTime.Now;

                Console.WriteLine("Console Calculator in C#\r");
                Console.WriteLine("------------------------");
                Console.WriteLine("Please select an option from the menu below:");
                Console.WriteLine("\tc - Start calculator");
                Console.WriteLine("\th - View history");
                Console.WriteLine("\tu - Use previous result for a new calculation");
                Console.WriteLine("\te - Erase history");
                Console.WriteLine("\tq - Quit");
                Console.WriteLine("Your option? ");
                string? menuChoice = Console.ReadLine()?.ToLower();

                if (menuChoice == "c")
                {
                    Console.WriteLine("Choose an operator from the following list:");
                    Console.WriteLine("\ta - Add");
                    Console.WriteLine("\ts - Subtract");
                    Console.WriteLine("\tm - Multiply");
                    Console.WriteLine("\td - Divide");
                    Console.WriteLine("\tr - Square Root");
                    Console.Write("Your option? ");

                    string? op = Console.ReadLine();

                    if (new[] { "a", "s", "m", "d", }.Contains(op))
                    {
                        Console.Write("Type a number, and then press Enter: ");
                        numInput1 = Console.ReadLine();

                        double cleanNum1 = 0;
                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput1 = Console.ReadLine();
                        }

                        Console.Write("Type another number, and then press Enter: ");
                        numInput2 = Console.ReadLine();

                        double cleanNum2 = 0;
                        while (!double.TryParse(numInput2, out cleanNum2))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput2 = Console.ReadLine();
                        }

                        ExecuteCalculation(cleanNum1, cleanNum2, op, calculator, history, ref calculationCount);
                    }
                    else if (op == "r")
                    {
                        Console.WriteLine("Type the number you want the square root of, and the press Enter: ");
                        numInput1 = Console.ReadLine();

                        double cleanNum1 = 0;
                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.WriteLine("This is not a valid input. Please enter a numeric value: ");
                            numInput1 = Console.ReadLine();
                        }

                        ExecuteCalculation(cleanNum1, 0, op, calculator, history, ref calculationCount);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                        return;
                    }
                }
                else if (menuChoice == "h")
                {
                    Console.WriteLine("Calculation History:");
                    foreach (var entry in history)
                    {
                        if (entry.Operation == "\u221A")
                        {
                            Console.WriteLine($"Date: {entry.Date}, calculation#{entry.CalculationCount}: {entry.Operation}{entry.Operand1} = {entry.Result}");
                        }
                        else
                        {
                            Console.WriteLine($"Date: {entry.Date}, calculation #{entry.CalculationCount}: {entry.Operand1} {entry.Operation} {entry.Operand2} = {entry.Result}");
                        }
                    }
                    Console.WriteLine("------------------------");
                }
                else if (menuChoice == "e")
                {
                    history.Clear();
                    calculationCount = 0;
                    Console.WriteLine("History erased.");
                    Console.WriteLine("------------------------");
                }
                else if (menuChoice == "u")
                {
                    if (calculationCount == 0)
                    {
                        Console.WriteLine("No calculations found");
                    }
                    else
                    {
                        Console.WriteLine("Choose an operator from the following list:");
                        Console.WriteLine("\ta - Add");
                        Console.WriteLine("\ts - Subtract");
                        Console.WriteLine("\tm - Multiply");
                        Console.WriteLine("\td - Divide");
                        Console.WriteLine("\tr - Square root");
                        Console.Write("Your option? ");

                        string? op = Console.ReadLine();
                        if (op == "r")
                        {
                            Console.WriteLine("History:");
                            foreach (var entry in history)
                            {
                                if (entry.Operation == "\u221A")
                                {
                                    Console.WriteLine($"{entry.CalculationCount}: {entry.Operand1} {entry.Operation} = {entry.Result}");
                                }
                                else
                                {
                                    Console.WriteLine($"{entry.CalculationCount}: {entry.Operand1} {entry.Operation} {entry.Operand2} = {entry.Result}");
                                }
                            }

                            Console.WriteLine($"Chose a previous result to use (1-{calculationCount})");
                            string? choice = Console.ReadLine();

                            if (int.TryParse(choice, out int index))
                            { 
                                var selectedEntry = history.FirstOrDefault(e => e.CalculationCount == index);
                                if (selectedEntry == null)
                                {
                                    Console.WriteLine("Invalid selection.");
                                }
                                else
                                {
                                    Console.WriteLine($"Using previous result: {selectedEntry.Result}");
                                    ExecuteCalculation(selectedEntry.Result, 0, op, calculator, history, ref calculationCount);
                                }
                            }
                        }
                        else if (new[] { "a", "s", "m", "d" }.Contains(op))
                        {

                            Console.WriteLine("History:");
                            foreach (var entry in history)
                            {
                                Console.WriteLine($"{entry.CalculationCount}: {entry.Operand1} {entry.Operation} {entry.Operand2} = {entry.Result}");
                            }

                            Console.WriteLine($"Chose a previous result to use (1-{calculationCount})");
                            string? choice = Console.ReadLine();

                            double num1 = 0;
                            double num2 = 0;

                            if (int.TryParse(choice, out int index))
                            {
                                var selectedEntry = history.FirstOrDefault(e => e.CalculationCount == index);
                                if (selectedEntry == null)
                                {
                                    Console.WriteLine("Invalid selection.");
                                }
                                else
                                {
                                    Console.WriteLine("Use previous result as:");
                                    Console.WriteLine("\t1 - First operand");
                                    Console.WriteLine("\t2 - Second operand");
                                    string? positionChoice = Console.ReadLine();

                                    if (positionChoice == "1")
                                    {
                                        num1 = selectedEntry.Result;
                                        Console.WriteLine("Enter the second operand:");
                                        string? input = Console.ReadLine();
                                        while (!double.TryParse(input, out num2))
                                        {
                                            Console.Write("This is not valid input. Please enter a numeric value: ");
                                            input = Console.ReadLine();
                                        }
                                    }
                                    else if (positionChoice == "2")
                                    {
                                        num2 = selectedEntry.Result;
                                        Console.WriteLine("Enter the first operand:");
                                        string? input = Console.ReadLine();
                                        while (!double.TryParse(input, out num1))
                                        {
                                            Console.Write("This is not valid input. Please enter a numeric value: ");
                                            input = Console.ReadLine();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid choice.");
                                        return;
                                    }
                                }
                            }

                            ExecuteCalculation(num1, num2, op, calculator, history, ref calculationCount);

                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                            return;
                        }
                    }
                }
                else if (menuChoice == "q")
                {
                    endApp = true;
                    calculator.Finish();
                }
                else
                {
                    Console.WriteLine("Invalid menu choice.");
                }
            }
        }

        static void ExecuteCalculation(double num1, double num2, string? op, Calculator calculator, List<CalculationEntry> history, ref int calculationCount)
        {
            string opSymbol = op switch
            {
                "a" => "+",
                "s" => "-",
                "m" => "*",
                "d" => "/",
                "r" => "\u221A",
                _ => "?",
            };

            double result = 0;
            CalculationEntry record = new CalculationEntry
            {
                Operand1 = num1,
                Operand2 = num2,
                Operation = opSymbol ?? "Unknown",
                Date = DateTime.Now
            };
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    result = calculator.DoOperation(num1, num2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        calculationCount++;
                        Console.WriteLine("Your result: {0:0.##}", result);
                        Console.WriteLine($"Calculation #{calculationCount} completed successfully.");
                        record.Result = result;
                        record.CalculationCount = calculationCount;
                        history.Add(record);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }

            Console.WriteLine("------------------------");
        }

        public class CalculationEntry
        {
            public double Operand1 { get; set; }
            public double Operand2 { get; set; }
            public string Operation { get; set; } = "";
            public double Result { get; set; } = double.NaN;
            public int CalculationCount { get; set; }
            public DateTime Date { get; set; }
        }
    }
}