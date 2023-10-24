using CalculatorLibrary;

namespace CalculatorProgram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Initialize Calculator Class
            Calculator calculator = new();
            bool endApp = false;

            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            while (!endApp)
            {
                double num1;
                double? num2 = null;
                double retrievedResult = 0;
                bool retrievedResults = false;

                do
                {
                    // Ask the user to choose an operator.
                    Menu();

                    //validate user input for operation
                    int userChoice = ValidateInt("Your option? ");

                    // Ask the user to type the first number or check for history retrieve
                    if (retrievedResults)
                    {
                        num1 = retrievedResult;
                        retrievedResults = false;
                    }
                    else
                    {
                        num1 = ValidateDouble("Type a number, and then press Enter: ");
                    }


                    //if operation requires two numbers
                    if (userChoice <= (int)Operation.Power)
                    {
                        num2 = ValidateDouble("Type another number, and then press Enter: ");
                    }


                    try
                    {
                        double result = calculator.DoOperation(num1, (Operation)userChoice, num2);
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

                    Console.WriteLine("------------------------\n");


                    // Wait for the user to respond before closing.
                    Console.Write("Press 'q' and Enter to close the app\nPress 'h' to view your calculation history\nPress any other key and Enter to continue: ");

                    string userInput = Console.ReadLine();

                    switch (userInput.ToLower())
                    {
                        case "q":
                            Console.WriteLine($"Times used: {calculator.TimesUsed}");
                            endApp = true;
                            break;
                        case "h":
                            retrievedResults = DisplayHistory(calculator);
                            break;
                        default:
                            Console.WriteLine("Invalid input.");
                            break;
                    }

                } while (!endApp);

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            // Add call to close the JSON writer before return
            calculator.Finish();

        }

        static void Menu()
        {
            Console.WriteLine("Choose an operator from the following list:");

            foreach (var op in Enum.GetValues(typeof(Operation)))
            {
                Console.WriteLine($"\t{(int)op} - {op}");
            }
        }

        private static int ValidateInt(string prompt)
        {
            Console.WriteLine(prompt);
            int cleanInput;
            while (!int.TryParse(Console.ReadLine(), out cleanInput))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
            }

            return cleanInput;
        }

        private static double ValidateDouble(string prompt)
        {
            Console.WriteLine(prompt);
            double cleanInput;
            while (!double.TryParse(Console.ReadLine(), out cleanInput))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
            }

            return cleanInput;
        }

        static bool DisplayHistory(Calculator calculator)
        {
            calculator.PrintHistory();
            Console.WriteLine("Enter the entry number to retrieve result or 'd' to delete history.");
            string input = Console.ReadLine();

            if (input.ToLower() == "d")
            {
                calculator.DeleteHistory();
                Console.WriteLine("History deleted");
                return false;
            }
            else if (int.TryParse(input, out int num))
            {
                calculator.RetrieveCalculationFromHistory(num);
                return true;
            }
            return false;
        }


    }
}
