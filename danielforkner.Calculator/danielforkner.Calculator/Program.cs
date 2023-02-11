using CalculatorLibrary;
using System.Linq;

namespace CalculatorProgram
{

    class Program
    {

        class Calculation
        {
            public double Operand1 { get; set; }
            public double Operand2 { get; set; }
            public string Operation { get; set;}
            public double Result { get; set; }
            public bool Success { get; set; }
        }
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new();
            int operationsCount = 0;
            List<Calculation> operations = new List<Calculation>();
            while (!endApp)
            {
                // Declare variables and set to empty.
                string numInput1;
                string numInput2;

                Console.WriteLine("Information on this Calculator\r");
                Console.WriteLine("---");
                Console.WriteLine($"Operations Performed: {operationsCount}");
                Console.WriteLine($"Highest Result: {(operationsCount == 0 ? "N/A" : operations.Max(op => op?.Result))}");
                Console.WriteLine("---\n");
                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\te - Raise to the Power");
                Console.Write("Your option? ");

                string op = Console.ReadLine();

                try
                {
                    double result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                        operations.Add(new Calculation
                        {
                            Operand1 = cleanNum1,
                            Operand2 = cleanNum2,
                            Operation = op,
                            Result = double.NaN,
                            Success = false
                        });
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                    operations.Add(new Calculation
                    {
                        Operand1 = cleanNum1,
                        Operand2 = cleanNum2,
                        Operation = op,
                        Result = result,
                        Success = true

                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    operations.Add(new Calculation
                    {
                        Operand1 = cleanNum1,
                        Operand2 = cleanNum2,
                        Operation = op,
                        Result = double.NaN,
                        Success = false
                    });
                }
                finally
                {
                    Console.WriteLine("iterating");
                    operationsCount++;
                }

                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            return;
            calculator.Finish();
        }
    }
}