using CalculatorLibrary;

namespace CalculatorProgram;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        bool leaveList;
        bool secondIsNumber;
        char[] singleOps = { 'r', 's', 'c', 't', 'p' };
        double cleanNum1 = double.NaN;
        Calculator calculator = new();
        List<Operation> operations = new();
        while (!endApp)
        {
            Console.WriteLine("Console Calculator in C#\tTimes used: {0}\r", operations.Count);
            Console.WriteLine("------------------------\n");

            string? numInput1;
            string? input2;

            // Ask the user to type the first number if not using any previous number.
            if (double.IsNaN(cleanNum1) || operations.Count == 0 )
            {
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }
            }
            // Ask the user to type the second number or to choose an operand to transform the first.
            Console.Write("\n");
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\tr - Square Root\tsqrt({0}) = ?", cleanNum1);
            Console.WriteLine("\ts - sin\t\tsin({0}) = ?", cleanNum1);
            Console.WriteLine("\tc - cos\t\tcos({0}) = ?", cleanNum1);
            Console.WriteLine("\tt - tan\t\ttan({0}) = ?", cleanNum1);
            Console.WriteLine("\tp - 10\t\t10 ^ {0} = ?", cleanNum1);
            Console.Write("\n");
            Console.Write("Or type another number, and then press Enter: ");
            Console.Write("\n");
            Console.Write("Your option? ");
            input2 = Console.ReadLine();

            double cleanNum2;
            
            // Guarantees the input is valid
            while (true)
            {
                if (double.TryParse(input2, out cleanNum2))
                {
                    secondIsNumber = true;
                    break;
                }
                else if (input2.Length == 1 && singleOps.Contains(input2[0]))
                {
                    secondIsNumber = false;
                    break;
                }
                else
                Console.Write("This is not valid input. Please enter a valid input: ");
                input2 = Console.ReadLine();
            }

            //If the user chose an operand instead of a number
            if (!secondIsNumber)
            {
                try
                {
                    operations.Add(calculator.DoOperation(cleanNum1, input2));
                    if (double.IsNaN(operations.Last().GetResult()))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", operations.Last().GetResult());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
                calculator.DoOperation(cleanNum1, input2);
            }

            else
            {
                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine($"\ta - Add\t\t\t {cleanNum1} + {cleanNum2} = ?");
                Console.WriteLine($"\ts - Subtract\t\t {cleanNum1} - {cleanNum2} = ?");
                Console.WriteLine($"\tm - Multiply\t\t {cleanNum1} x {cleanNum2} = ?");
                Console.WriteLine($"\td - Divide\t\t {cleanNum1} / {cleanNum2} = ?");
                Console.WriteLine($"\tp - Taking the power\t {cleanNum1} ^ {cleanNum2} = ?");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                try
                {
                    operations.Add(calculator.DoOperation(cleanNum1, cleanNum2, op));
                    if (double.IsNaN(operations.Last().GetResult()))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", operations.Last().GetResult());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }   
            }

            // Wait for the user to respond before closing.
            cleanNum1 = double.NaN;
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Press 'l' and Enter to view recent calculations");
            Console.WriteLine("Press 'n' and Enter to close the app");
            Console.WriteLine("Or press any other key and Enter to continue");

            string? input = Console.ReadLine();

            if (input == "n") endApp = true;

            else if (input == "l") 
            {
                leaveList = false;
                while (!leaveList)
                {
                    Console.Clear();
                    Console.WriteLine("List");
                    Console.WriteLine("------------------------\n");

                    for (int i = 0; i < operations.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}: {operations[i].GetOperationString()}");
                    }

                    Console.WriteLine("\n");
                    Console.WriteLine("------------------------\n");
                    Console.WriteLine("Choose an index to use as first operand");
                    Console.WriteLine("Press 'd' and Enter to delete the list");
                    Console.WriteLine("Press 'n' and Enter to close the app");
                    Console.WriteLine("Or press any other key and Enter to continue");

                    input = Console.ReadLine();

                    if (double.TryParse(input, out double aux) && (int)aux <= operations.Count )
                    {
                        cleanNum1 = operations[(int)aux - 1].GetResult();
                        break;
                    }
                    else if (input == "n")
                    {
                        endApp = true;
                        break;
                    }
                    else if (input == "d")
                    {
                        operations.Clear();
                        Console.Clear();
                    }
                    else { leaveList = true; Console.Clear(); }
                }
            }
            else { Console.Clear(); }
        }
        calculator.Finish();
        return;
    }
}