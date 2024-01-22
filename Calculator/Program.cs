using CalculatorLibrary;

namespace CalculatorProgram
{

    class Program
    {
        static Calculator calculator = new Calculator();
        static bool endApp;
        static double firstNumber = double.NaN;
        static double secondNumber = double.NaN;
        
        static void Main(string[] args)
        {
            MainMenu();

            calculator.Finish();
            return;
        }

        static void Calculate()
        {
            while (true)
            {
                // Declare variables and set to empty.
                double cleanNum1 = 0;
                double cleanNum2 = 0;
                double result = 0;
                
                Console.Clear();
                Console.WriteLine("Only the first number will be used for square root,");
                Console.WriteLine("10x, sine, cosine, tangent and cotangent.\n");
                Console.WriteLine("Trigonometric functions are in radians.\n");
                Console.WriteLine("If you want to convert degrees to radians, type 'd'.");
                Console.WriteLine("Otherwise, press any key and enter to continue.");
                
                var userChoice = Console.ReadLine().ToLower().Trim();

                if (userChoice.Equals("d"))
                {
                    Console.Write("Please enter degrees needed to be converted to radians: ");
                    cleanNum1 = AskForNumber();
                    
                    firstNumber = calculator.ConvertToRadians(cleanNum1);
                    // Assigning secondNumber to 1 to avoid divide by zero errors
                    // in case if the user will not choose trigonometric operation.
                    secondNumber = 1d;
                }

                // Ask the user to type the first number.
                if (double.IsNaN(firstNumber))
                {
                    Console.Write("Type first number, and then press Enter: ");
                    
                    cleanNum1 = AskForNumber();
                }
                else
                {
                    cleanNum1 = firstNumber;
                }

                // Ask the user to type the second number.
                if (double.IsNaN(secondNumber))
                {
                    Console.Write("Type second number, and then press Enter: ");

                    cleanNum2 = AskForNumber();
                } 
                else
                {
                    cleanNum2 = secondNumber;
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tp - Power");
                Console.WriteLine("\tr - Square Root");
                Console.WriteLine("\tx - 10x");
                Console.WriteLine("\tsin - Sine");
                Console.WriteLine("\tcos - Cosine");
                Console.WriteLine("\ttan - Tangent");
                Console.WriteLine("\tcot - Cotangent");
                Console.Write("Your option? ");

                string op = AskForOperation();

                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
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
                firstNumber = double.NaN;
                secondNumber = double.NaN;

                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'm' to return to the main menu or press any other key and Enter to continue: ");
                
                var input = Console.ReadLine().ToLower();
                
                if (input.Equals("m")) return;
                
                Console.WriteLine("\n"); // Friendly line spacing.
            }
        }

        static void ShowHistory()
        {
            Console.Clear();
            int historySize = calculator.GetHistoryCount();
            double pickedNumber = double.NaN;
            string[] availableOptions = new string[historySize];

            for (int i = 0; i < historySize; i++)
            {
                availableOptions[i] = (i + 1).ToString();
            }
            
            Console.WriteLine("History of Calculations\r");
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Type in the number of the calculation you want to use or 'm' to return.");
            Console.WriteLine("Type 'd' to delete history.");
            Console.WriteLine("Choose an option from the following list:");
            calculator.ShowCalculationsHistory();
            Console.WriteLine();
            
            Console.Write("Your option? ");

            while (true)
            {
                string input = Console.ReadLine().ToLower().Trim();
                
                if (input.Equals("m")) return;

                if (input.Equals("d"))
                {
                    calculator.DeleteHistory();
                    Console.WriteLine("History deleted.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }
                
                if (string.IsNullOrWhiteSpace(input) || !availableOptions.Contains(input))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
                else
                {
                    int index = Convert.ToInt32(input);
                    pickedNumber = calculator.GetHistoryEntry(index);
                    break;
                }
            }

            Console.WriteLine("If you want to use the result of the calculation, " +
                              "as first operand type 'f', otherwise 's'.");
            Console.Write("Your option? ");

            while (true)
            {
                string input = Console.ReadLine().ToLower().Trim();
                
                    if (input.Equals("f"))
                    {
                        firstNumber = pickedNumber;
                        break;
                    }

                    if (input.Equals("s"))
                    {
                        secondNumber = pickedNumber;
                        break;
                    }
                    
                Console.WriteLine("Invalid input. Please try again.");
            }

            Console.WriteLine("You will be redirected to the main menu, choose calculate entry and ");
            Console.WriteLine("your result will be in place of chosen operand.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void MainMenu()
        {
            while (!endApp)
            {
                Console.Clear();
                // Display title as the C# console calculator app.
                Console.WriteLine("Console Calculator in C#\r");
                Console.WriteLine("------------------------\n");
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\tc - Calculate");
                Console.WriteLine("\th - History");
                Console.WriteLine("\tq - Quit");
                Console.Write("Your option? ");
                
                string input = Console.ReadLine();
                
                switch (input)
                {
                    case "c":
                        Calculate();
                        break;
                    case "h":
                        ShowHistory();
                        break;
                    case "q":
                        endApp = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }

        static string AskForOperation()
        {
            string[] validCommands =
            {
                "a", "s", "m", "d", 
                "p", "r", "x", "sin", 
                "cos", "tan", "cot"
            };
            
            while (true)
            {
                var input = Console.ReadLine().ToLower().Trim();

                if (validCommands.Contains(input))
                {
                    return input;
                }
                
                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        static double AskForNumber()
        {
            double result = 0;
            
            string? input = Console.ReadLine();
            
            while (!double.TryParse(input, out result))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                
                input = Console.ReadLine();
            }

            return result;
        }
    }
}