using CalculatorLibrary;
using System.Text.RegularExpressions;
using System.Threading;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();

            double lastResult = 0;

            while (!endApp)
            {
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;

                Console.Clear();
                Console.WriteLine(@$"Choose an operator from the following list:
 a - Add
 s - Subtract
 m - Multiply
 d - Divide
 sr - Square Root
 p - Power
 p10 - Power of 10
 sin - Sine
 cos - Cosine
 tan - Tangent
 v - View operation history
 c - Clear operation history");
                Console.Write("Your option?: ");

                string? op = Console.ReadLine().ToLower();

                if (op == null || !Regex.IsMatch(op, "^(a|s|m|d|sr|p|p10|sin|cos|tan|v|c)$"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                    Thread.Sleep(1000);
                    continue;
                }
                else
                {
                    if (op == "c")
                    {
                        calculator.ClearLog();
                        Console.WriteLine("Clearing operation log.");
                    }
                    else if (op == "v")
                    {
                        var log = calculator.OperationLog();
                        Console.WriteLine("Operation log: ");
                        foreach (var operation in log)
                        {
                            Console.WriteLine(operation);
                        }
                    }
                    else
                    {
                        double cleanNum1 = 0;
                        double cleanNum2 = 0;

                        Console.Write("Type the first number or enter 'r' to use the last operation result: ");
                        numInput1 = Console.ReadLine().ToLower();

                        if (numInput1 == "r")
                        {
                            cleanNum1 = lastResult;
                        }
                        else
                        {
                            while (!double.TryParse(numInput1, out cleanNum1))
                            {
                                Console.Write("This is not valid input. Please enter an integer value: ");
                                numInput1 = Console.ReadLine();
                            }
                        }
                        if (op != "sr" && op != "p10" && op != "sin" && op != "cos" && op != "tan")
                        {
                            Console.Write("Type the second number or enter 'r' to use the last operation result: ");
                            numInput2 = Console.ReadLine().ToLower();

                            if (numInput2 == "r")
                            {
                                cleanNum2 = lastResult;
                            }
                            else
                            {
                                while (!double.TryParse(numInput2, out cleanNum2))
                                {
                                    Console.Write("This is not valid input. Please enter an integer value: ");
                                    numInput2 = Console.ReadLine();
                                }
                            }
                        }
                        try
                        {
                            result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                            if (double.IsNaN(result))
                            {
                                Console.WriteLine("This operation will result in a mathematical error.\n");
                            }
                            else
                            {
                                Console.WriteLine("Your result: {0:0.##}\n", result);
                                lastResult = result;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                        }
                    }
                    Console.WriteLine("------------------------\n");

                    Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                    if (Console.ReadLine().ToLower() == "n")
                    {
                        endApp = true;
                    }

                    Console.WriteLine("\n");
                }
            }
            calculator.Finish();
            return;
        }
    }
}