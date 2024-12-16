// Program.cs
using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            bool IsReused = false;
            bool InsertAndReplaceCheck = false;

            int count = 0;
            int saveNumber = 0;
            int firstTempValue = -1;


            String Select = "";

            List<double> resultNumbers = new List<double>();
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                saveNumber = resultNumbers.Count;

                while (!Select.Equals("x"))
                {
                    Console.WriteLine("\tb - Calculator usage counter");
                    Console.WriteLine("\tz - chaining calculations");
                    Console.WriteLine("\tk - result remove");
                    Console.WriteLine("\tx - skip");
                    Console.Write("Your option? ");

                    Select = Console.ReadLine();

                    if (Select == null || !Regex.IsMatch(Select, "[b|z|x|k]"))
                    {
                        Console.WriteLine("Error: Unrecognized input.");
                        continue;
                    }
                    else if (Select == "b")
                    {
                        calculator.UsageCount(count);
                    }
                    else if (Select == "z")
                    {
                        saveNumber = calculator.ChainingCalculations(Select, resultNumbers, ref IsReused, ref firstTempValue);
                        break;
                    }
                    else if (Select == "k")
                    {
                        calculator.ResultRemove(resultNumbers);
                    }
                }

                Select = "";


                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";

                double result = 0;

                // Ask the user to type the first number.

                if (!IsReused)
                {
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();
                }
                else
                {
                    numInput1 = resultNumbers[saveNumber].ToString();
                    IsReused = false;
                    InsertAndReplaceCheck = true;
                }


                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
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
                Console.WriteLine("\tr - Square root");
                Console.WriteLine("\te - Exponentiation");
                Console.WriteLine("\tt - Tenfold - Only num1 is applied.");
                Console.WriteLine("\ti - Sine - Only num1 is applied.");
                Console.WriteLine("\tc - Cosine - Only num1 is applied.");
                Console.WriteLine("\tg - Tangent - Only num1 is applied.");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r|e|t|i|c|g]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op, count, resultNumbers, IsReused);

                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else if (IsReused == true)
                        {
                            continue;
                        }
                        else
                        {
                            count++;

                            if (InsertAndReplaceCheck == false)
                            {
                                resultNumbers.Add(result);
                            }
                            else
                            {
                                resultNumbers[saveNumber] = result;
                                InsertAndReplaceCheck = false;
                            }


                            Console.WriteLine("Your result: {0:0.##}\n", result);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }
    }
}