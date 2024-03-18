using CalculatorLibrary;

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
            double result = double.NaN;
            int counter = 0;

            while (!endApp)
            {
                string? numInput1 = "";
                string? numInput2 = "";
                string? letterInput = "";
                double cleanNum1 = 0;

                if (!double.IsNaN(result))
                {
                    cleanNum1 = result;
                }
                else
                {
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();

                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput1 = Console.ReadLine();
                    }
                }

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
                Console.WriteLine("\tr - Square root (√num1)");
                Console.WriteLine("\te - Exponentiation (num1 raised to the power of num2)");
                Console.WriteLine("\te10 - Exponential of 10 (10 raised to the power of num1)");
                Console.WriteLine("\tsin - Sine (the sine of num1 degrees)");
                Console.WriteLine("\tcos - Cosine (the cosine of num1 degrees)");
                Console.WriteLine("\ttan - Tangent (the tangent of num1 degrees)");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                string[] validOperators = { "a", "s", "m", "d", "r", "e", "e10", "sin", "cos", "tan" };

                if (op == null || !validOperators.Contains(op.ToLower()))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
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
                            counter++;
                            if (counter <= 1)
                            {
                                Console.WriteLine($"Calculator was used {counter} time");
                            }
                            else
                                Console.WriteLine($"Calculator was used {counter} times");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }

                    Console.WriteLine("------------------------\n");

                    do
                    {
                        Console.WriteLine("\tEnter - continue (use last result as a first number)");
                        Console.WriteLine("\t'n' and Enter - new calculation");
                        Console.WriteLine("\t's' and Enter - show previous calculations");
                        Console.WriteLine("\t'd' and Enter - delete previous calculations");
                        Console.WriteLine("\t'c' and Enter - close the app");
                        letterInput = Console.ReadLine();

                        if (letterInput == "c")
                            endApp = true;

                        else if (letterInput == "n")
                        {
                            result = double.NaN;
                            break;
                        }
                        else if (letterInput == "s")
                        {
                            if (calculator.calculations.Count == 0)
                            {
                                Console.WriteLine("There are no calculations saved");
                            }
                            else
                            {
                                foreach (string calculation in calculator.calculations)
                                {
                                    Console.WriteLine($"{calculation}");
                                }
                            }
                        }
                        else if (letterInput == "d")
                        {
                            calculator.calculations.Clear();
                            Console.WriteLine("All calculations were deleted");
                        }
                        else if (letterInput == "")
                            break;

                        else
                            Console.WriteLine("Error: Unrecognized input.");

                        Console.WriteLine("\n");
                    }
                    while (endApp != true);
                }
            }
            // Add call to close the JSON writer before return
            calculator.Finish();
            return;
        }
    }
}