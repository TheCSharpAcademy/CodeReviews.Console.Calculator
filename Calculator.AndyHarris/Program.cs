using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            List<string> problems = new List<string>();
            Dictionary<string, string> operators = new Dictionary<string, string>
            {
                { "a", "+" },
                { "s", "-" },
                { "m", "*" },
                { "d", "/" }
            };

            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;
                string? op = Console.ReadLine();

                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tg - Get number of operations");
                Console.WriteLine("\tp - Get problem history");
                Console.WriteLine("\tx - Delete problem history");
                Console.Write("Your option? ");

                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|g|p|x]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else if (op == "g")
                {
                    calculator.Finish();
                    int totalOperations = calculator.GetOperationCount();
                    Console.WriteLine($"Total operations so far: {totalOperations}");
                }
                else if (op == "p")
                {
                    if (problems.Count() > 0)
                    {
                        foreach (string problem in problems)
                        {
                            Console.WriteLine(problem);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No problems yet");
                    }
                }
                else if (op == "x")
                {
                    if (problems.Count() > 0)
                    {
                        problems = new List<string>();
                        Console.WriteLine("Problem history deleted");
                    }
                    else
                    {
                        Console.WriteLine("No problems yet");
                    }
                }
                else
                {
                    try
                    {
                        Console.Write("Type a number, and then press Enter: ");
                        numInput1 = Console.ReadLine();

                        double cleanNum1 = 0;
                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter an integer value: ");
                            numInput1 = Console.ReadLine();
                        }

                        Console.Write("Type another number, and then press Enter: ");
                        numInput2 = Console.ReadLine();

                        double cleanNum2 = 0;
                        while (!double.TryParse(numInput2, out cleanNum2))
                        {
                            Console.Write("This is not valid input. Please enter an integer value: ");
                            numInput2 = Console.ReadLine();
                        }

                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        problems.Add($"{cleanNum1} {operators[op]} {cleanNum2} = {result}");

                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                Console.WriteLine("------------------------\n");

                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n");
            }
            calculator.Finish();
            return;
        }
    }
}