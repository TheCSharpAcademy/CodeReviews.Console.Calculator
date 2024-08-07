using System.Text.RegularExpressions;

namespace CalculatorLibrary
{
    public class Menu
    {
        /// <summary>
        /// Menu of the game, interaction with the user
        /// </summary>
        public void GetMenu()
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            int count = 0;
            //List of calculations that were made it contains info about calculation and result
            List<Tuple<string, double>> calculations = new List<Tuple<string, double>>();

            Calculator calculator = new Calculator();
            while (!endApp)//Loop's until user quits
            {
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tsqr - Square root (it will operate with first number that you entered!)");
                Console.WriteLine("\tpow - Taking power (it will operate with first number that you entered!)");
                Console.WriteLine("\t10x - 10x (it will operate with first number that you entered!)");
                Console.WriteLine("\tsin - Sin(x) (it will operate with first number that you entered!)");
                Console.WriteLine("\tcos - Cos(x) (it will operate with first number that you entered!)");
                Console.WriteLine("\ttan - Tan(x) (it will operate with first number that you entered!)");
                Console.WriteLine("\th - History");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Declare variables and set to empty.

                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;

                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = Helpers.NumberValidation(numInput1);
                
                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = Helpers.NumberValidation(numInput2);
                

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "^(?:a|s|m|d|h|cos|sin|10x|sqr|tan|pow)$"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else if (op == "h")//Case then user choose to go through history
                {
                    int i = 1;
                    foreach (var cal in calculations)
                    {
                        Console.WriteLine($"{i}. " + cal.Item1);
                        i++;
                    }
                    Console.WriteLine();
                    Console.WriteLine($"Operations that were made: {i}");
                    Console.WriteLine("Do you want to clear history? yes/no");
                    string? answer = Console.ReadLine();
                    if (answer == "yes")
                    {
                        calculations.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Do you want to use previuos result? yes/no");
                        answer = Console.ReadLine();
                        if (answer == "yes")
                        {
                            int parsedNumber;
                            Console.WriteLine("Enter number of calculation result that you want to use:");
                            string? numberOfCalculation = Console.ReadLine();

                            while (!int.TryParse(numberOfCalculation, out parsedNumber) && parsedNumber >= 1 && parsedNumber < calculations.Count)
                            {
                                Console.Write("This is not valid input. Please enter a numeric value: ");
                                numberOfCalculation = Console.ReadLine();
                            }
                            double numH1 = calculations[parsedNumber - 1].Item2;


                            Console.WriteLine("Choose an operator from the following list:");

                            Console.WriteLine("\ta - Add");
                            Console.WriteLine("\ts - Subtract");
                            Console.WriteLine("\tm - Multiply");
                            Console.WriteLine("\td - Divide");
                            Console.WriteLine("\tsqr - Square root (it will operate with first number that you entered!)");
                            Console.WriteLine("\tpow - Taking power (it will operate with first number that you entered!)");
                            Console.WriteLine("\t10x - 10x (it will operate with first number that you entered!)");
                            Console.WriteLine("\tsin - Sin(x) (it will operate with first number that you entered!)");
                            Console.WriteLine("\tcos - Cos(x) (it will operate with first number that you entered!)");
                            Console.WriteLine("\ttan - Tan(x) (it will operate with first number that you entered!)");

                            op = Console.ReadLine();

                            while (op == null || !Regex.IsMatch(op, "^(?:a|s|m|d|h|cos|sin|10x|sqr|tan|pow)$"))
                            {
                                Console.WriteLine("Error: Unrecognized input. enter again:");
                                op = Console.ReadLine();
                            }
                            if (Regex.IsMatch(op, "^(?:a|s|m|d)$"))
                            {
                                Console.WriteLine("Wich operand do you want your result to be? 1 or 2 (enter the number) ");
                                string? operandPosition = Console.ReadLine();
                                while (!int.TryParse(operandPosition, out parsedNumber))
                                {
                                    Console.Write("This is not valid input. Please enter a numeric value: ");
                                    operandPosition = Console.ReadLine();
                                }

                                int parsedNumber1 = 0;
                                Console.WriteLine("Enter second operand");
                                string? numH2 = Console.ReadLine();
                                parsedNumber1 = Helpers.NumberValidation1(numH2);

                                if (parsedNumber == 1)
                                {
                                    try
                                    {
                                        result = calculator.DoOperationTwoNumber(numH1, parsedNumber1, op, ref calculations);
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
                                else
                                {
                                    try
                                    {
                                        result = calculator.DoOperationTwoNumber(parsedNumber1, numH1, op, ref calculations);
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
                            }
                            else
                            {
                                try
                                {
                                    result = calculator.DoOperationOneNumber(numH1, op, ref calculations);
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
                        }
                    }
                }
                else if (Regex.IsMatch(op, "^(?:a|s|m|d)$"))
                {
                    try
                    {
                        result = calculator.DoOperationTwoNumber(cleanNum1, cleanNum2, op, ref calculations);
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
                else
                {
                    try
                    {
                        result = calculator.DoOperationOneNumber(cleanNum1, op, ref calculations);
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

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
                count++;
            }
            calculator.Finish();
            return;
        }

      
    }
}
