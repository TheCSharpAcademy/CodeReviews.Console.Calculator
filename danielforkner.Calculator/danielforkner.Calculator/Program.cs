using CalculatorLibrary;

namespace CalculatorProgram;
class Program
{
    public static void ViewOperations(Calculator calculator)
    {
        Console.WriteLine("\n----- Viewing Past Operations -----\n");
        bool viewingOperations = true;
        int idx = 0;
        while (viewingOperations)
            {
                if (calculator.Operations.Count == 0)
                {
                    Console.WriteLine("You don't have any past operations to view!");
                    viewingOperations = false;
                    continue;
                }
                Console.WriteLine($"Operation: {calculator.Operations[idx].Id}");
                Console.WriteLine($"Operand1: {calculator.Operations[idx].Operand1}");
                Console.WriteLine($"Operand2: {calculator.Operations[idx].Operand2}");
                Console.WriteLine($"Result: {calculator.Operations[idx].Result}");
                if (!calculator.Operations[idx].Success)
                {
                    Console.WriteLine($"Success: {calculator.Operations[idx].Success}");
                }
                Console.Write("\n");
                Console.WriteLine($"{(calculator.Operations.Count > idx + 1 ? "Type next to move forward\n" : "")}{(idx != 0 ? "Type back for previous operation\n" : "")}Type delete to remove history\nType q to stop viewing operations");
                Console.Write("Selection: ");
                string selection = Console.ReadLine();
                Console.Write("\n");
                switch (selection.ToLower())
                {
                    case "next":
                        if (calculator.Operations.Count <= idx + 1)
                        {
                            break;
                        } else
                        {
                            idx++;
                            break;
                        }
                    case "back":
                        if (idx <= 0)
                        {
                            break;
                        } else
                        {
                            idx--;
                            break;
                        }
                    case "delete":
                        calculator.Operations.RemoveAt(idx);
                        if (calculator.Operations.Count <= idx)
                    {
                        idx--;
                        break;
                    } 
                    break;
                    case "q":
                        viewingOperations = false;
                        break;
                }
            }
        Console.WriteLine("----- End View -----\n");
    }

    static void Main(string[] args)
    {
        bool endApp = false;
        string[] operationOptions = { "a", "s", "d", "m", "e" };
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new();
        while (!endApp)
        {
            string numInput1;
            string numInput2;

            Console.WriteLine("Information on this Calculator:");
            Console.WriteLine($"Operations Performed: {calculator.OperationsCount}");
            Console.WriteLine($"Highest Result: {(calculator.OperationsCount == 0 ? "N/A" : calculator.Operations.Max(op => op?.Result))}");
            Console.Write("\n");
            Console.WriteLine("---\n");

            Console.WriteLine("Calculator is open for business, what would you like to do?");
            Console.WriteLine("\t1. Perform an operation");
            Console.WriteLine("\t2. View past operations");
            Console.Write("Your selection: ");
            string selection = Console.ReadLine();
            Console.Write("\n");

            while (selection != "1" && selection != "2")
            {
                Console.Write("Choose 1 or 2: ");
                selection = Console.ReadLine();
            }

            if (selection == "2")
            {
                ViewOperations(calculator);
            }
            
            Console.Write("Type a new number" + $"{(calculator.Operations.Count > 0 ? ", or \"r\" to use previous result," : "")}" +  " and then press Enter: ");
            numInput1 = Console.ReadLine();
            double cleanNum1;
            if (numInput1.ToLower() == "r") 
            {
                cleanNum1 = calculator.Operations[calculator.Operations.Count - 1].Result;
            } else
            {
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }
            }

            Console.Write("Type another number" + $"{(calculator.Operations.Count > 0 ? ", or \"r\" to use previous result," : "")}" + " and then press Enter: ");
            numInput2 = Console.ReadLine();
            double cleanNum2;
            if (numInput2.ToLower() == "r")
            {
                cleanNum2 = calculator.Operations[calculator.Operations.Count - 1].Result;
            } else
            {
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }
            }

            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\te - Raise to the Power");
            Console.Write("Your option: ");

            string op = Console.ReadLine();
            while (!operationOptions.Contains(op))
            {
                Console.Write("Supply a valid option: ");
                op = Console.ReadLine();
            }
            try
            {
                double result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                    calculator.Operations.Add(new Calculation
                    {
                        Id = calculator.OperationsCount + 1,
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
                    Id = calculator.OperationsCount + 1,
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
                    Id = calculator.OperationsCount + 1,
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

            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.Write("\n");
        }
        calculator.Finish();
        return;
    }
}