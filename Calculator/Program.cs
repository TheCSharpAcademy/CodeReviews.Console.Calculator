using CalculatorLibrary;

namespace CalculatorProgram
{

    class Program
    {
        static Calculator calculator = new Calculator();
        static bool endApp;
        static double firstNumber;
        static double secondNumber;
        
        static Program()
        {
            calculator = new Calculator();
            endApp = false;
            firstNumber = double.NaN;
            secondNumber = double.NaN;
        }
        
        static void Main(string[] args)
        {
            MainMenu();

            // calculator.Finish();
            return;
        }

        static void Calculate()
        {
            while (true)
            {
                // Declare variables and set to empty.
                string numInput1 = "";
                string numInput2 = "";
                double cleanNum1 = 0;
                double cleanNum2 = 0;
                double result = 0;

                // Ask the user to type the first number.
                if (double.IsNaN(firstNumber))
                {
                    Console.Write("Type first number, and then press Enter: ");
                    numInput1 = Console.ReadLine();


                    cleanNum1 = 0;
                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput1 = Console.ReadLine();
                    }
                }
                else
                {
                    cleanNum1 = firstNumber;
                }

                // Ask the user to type the second number.
                if (double.IsNaN(secondNumber))
                {
                    Console.Write("Type second number, and then press Enter: ");
                    numInput2 = Console.ReadLine();

                    cleanNum2 = 0;
                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput2 = Console.ReadLine();
                    }
                } 
                else
                {
                    cleanNum2 = secondNumber;
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("Please note, only the first number will be used for square root,");
                Console.WriteLine("10x, sine, cosine, tangent and cotangent.");
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

                string op = Console.ReadLine();

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
                Console.Write("Press 'n' to close the app, 'm' for main menu or press any other key and Enter to continue: ");
                
                var input = Console.ReadLine().ToLower();
                
                if (input.Equals("n")) Environment.Exit(0);
                if (input.Equals("m")) return;
                
                Console.WriteLine("\n"); // Friendly linespacing.
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
                string input = Console.ReadLine().ToLower();
                
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
                string input = Console.ReadLine().ToLower();
                
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
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }
    }
}