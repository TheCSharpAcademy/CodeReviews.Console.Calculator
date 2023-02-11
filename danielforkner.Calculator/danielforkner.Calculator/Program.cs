using CalculatorLibrary;

namespace CalculatorProgram
{

    class Program
    {

        public static void ViewOperations(Calculator calculator)
        {
            Console.WriteLine("\n----- Viewing Past Operations -----\n");
            bool viewingOperations = true;
            int idx = 0;
            string selection = "";
            if (calculator.Operations.Count == 0)
            {
                Console.WriteLine("You don't have any past operations to view!");
            }
            else while (viewingOperations)
                {
                    // operation
                    Console.WriteLine($"Operation: {calculator.Operations[idx].id}");
                    Console.WriteLine($"Operand1: {calculator.Operations[idx].Operand1}");
                    Console.WriteLine($"Operand2: {calculator.Operations[idx].Operand2}");
                    Console.WriteLine($"Result: {calculator.Operations[idx].Result}");
                    if (!calculator.Operations[idx].Success)
                    {
                        Console.WriteLine($"Success: {calculator.Operations[idx].Success}");
                    }
                    Console.WriteLine("\n");
                    Console.WriteLine($"{(calculator.Operations.Count > idx + 1 ? "Type next to move forward\n" : "")}{(idx != 0 ? "Type back for the previous operation\n" : "")}Type q to stop viewing operations");
                    Console.Write("Selection: ");
                    selection = Console.ReadLine();
                    // convert to switch
                    while (selection != "next".ToLower() && selection != "back".ToLower() && selection != "q")
                    {
                        Console.WriteLine("Your options are \"next\", \"back\", or \"q\"");
                        selection = Console.ReadLine();
                    }
                    if (selection == "q")
                    {
                        viewingOperations = false;
                    }
                    else if (selection == "next")
                    {
                        idx++;
                    }
                    else
                    {
                        idx--;
                    }

                }
            Console.WriteLine("----- End View -----\n");
        }
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new();
            while (!endApp)
            {
                // Declare variables and set to empty.
                string numInput1;
                string numInput2;

                Console.WriteLine("Information on this Calculator:");
                Console.WriteLine($"Operations Performed: {calculator.OperationsCount}");
                Console.WriteLine($"Highest Result: {(calculator.OperationsCount == 0 ? "N/A" : calculator.Operations.Max(op => op?.Result))}");
                Console.WriteLine("\n");
                Console.WriteLine("---\n");

                Console.WriteLine("Calculator is open for business, what would you like to do?");
                Console.WriteLine("\t1. Perform an operation");
                Console.WriteLine("\t2. View past operations");
                Console.Write("Your selection: ");

                string selection = Console.ReadLine();
                while (selection != "1" && selection != "2")
                {
                    Console.WriteLine("Choose 1 or 2: ");
                    selection = Console.ReadLine();
                }

                if (selection == "2")
                {
                    ViewOperations(calculator);
                }
                
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
                Console.Write("Your option: ");

                string op = Console.ReadLine();

                try
                {
                    double result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                        calculator.Operations.Add(new Calculation
                        {
                            id = calculator.OperationsCount + 1,
                            Operand1 = cleanNum1,
                            Operand2 = cleanNum2,
                            Operation = op,
                            Result = double.NaN,
                            Success = false
                        });
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                    calculator.Operations.Add(new Calculation
                    {
                        id = calculator.OperationsCount + 1,
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
                    calculator.Operations.Add(new Calculation
                    {
                        id = calculator.OperationsCount + 1,
                        Operand1 = cleanNum1,
                        Operand2 = cleanNum2,
                        Operation = op,
                        Result = double.NaN,
                        Success = false
                    });
                }
                finally
                {
                    calculator.OperationsCount++;
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