using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            Calculator calculator = new();

            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            while (!endApp)
            {
                double cleanNum1 = GetNumber(calculator, "Enter a number ");
                string op = GetOperation();
                double cleanNum2 = GetNumber(calculator, "Enter another number ");
                double result;

                if (op == "D")
                {
                    cleanNum2 = PreventZeroDivision(calculator, cleanNum2);
                }

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

                Console.WriteLine("--------------------------");
                Console.WriteLine($"Calculations performed: {calculator.UseCount}");
                Console.WriteLine("--------------------------\n");

                endApp = DisplayMenu (calculator);

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }

        private static double PreventZeroDivision(Calculator calc, double divisor)
        {
            while (divisor == 0)
            {
                Console.WriteLine("Cannot divide by zero");
                divisor = GetNumber(calc);
            }
            return divisor;
        }

        private static string GetOperation()
        {
            string[] options = new[] {"A", "S", "M", "D", "P", "R"  };
            string choice = string.Empty;

            while (options.Contains(choice) == false)
            {
                Console.WriteLine("Choose an operation from the following list:");
                Console.WriteLine($"\t{options[0]} - Add");
                Console.WriteLine($"\t{options[1]} - Subtract");
                Console.WriteLine($"\t{options[2]} - Multiply");
                Console.WriteLine($"\t{options[3]} - Divide");
                Console.WriteLine($"\t{options[4]} - To the Power Of");
                Console.WriteLine($"\t{options[5]} - Root");
                Console.Write("Your option? ");
                choice = Console.ReadLine()!.ToUpper();
                if (options.Contains(choice) == false)
                {
                    Console.WriteLine("\nThat is not a valid choice\n");
                }
            }
            Console.WriteLine();
            return choice;
        }

        private static double GetNumber(Calculator calc, string prompt = "")
        {
            double output = 0;
            bool isValid = false;

            while (isValid == false)
            {
                Console.Write($"{prompt} (P - Previous result): ");
                string input = Console.ReadLine()!;

                if (input.ToUpper() == "P" && calc.Calculations.Count > 0) 
                {
                    DisplayPreviousCalculations(calc);
                    Console.Write("Choose a result: ");
                    string choice = Console.ReadLine()!.ToUpper();
                    char c = choice[0];
                    int index = c - 65;

                    output = calc.Calculations[index].Result;
                    Console.WriteLine($"You chose {output}");
                    isValid = true;
                }
                else if (double.TryParse(input, out output) == false)
                {
                    Console.WriteLine("That is not a valid number");
                }
                else
                {
                    isValid = true;
                }
            }
            return output;
        }

        private static bool DisplayMenu(Calculator calculator)
        {
            string choice;
            bool quit = false;
            do
            {
                Console.WriteLine("Choose from the following options:");
                Console.WriteLine("\tP: View previous calculations");
                Console.WriteLine("\tC: Clear previous calculations");
                Console.WriteLine("\tN: Perform a new calculation");
                Console.WriteLine("\tQ: Quit");
                Console.Write("Select an option: ");
                choice = Console.ReadLine()!.ToUpper();

                if (choice == "Q")
                {
                    quit = true;
                }
                else if (choice == "P")
                {
                    DisplayPreviousCalculations(calculator);
                }
                else if (choice == "C")
                {
                    Console.Clear();
                    Console.WriteLine("Previous calculations deleted");
                    Console.WriteLine();
                    calculator.Calculations.Clear();
                    choice = "N";
                }
                else if (choice != "N")
                {
                    Console.Clear();
                    Console.WriteLine("Sorry, that is not a valid option, please try again.");
                }
            } while (choice != "N" && choice != "Q");

            return quit;
        }

        private static void DisplayPreviousCalculations(Calculator calculator)
        {
            Console.Clear();
            foreach (Calculation calc in calculator.Calculations)
            {
                Console.Write($"\t{Char.ConvertFromUtf32(calculator.Calculations.IndexOf(calc) + 65)}: ");
                Console.WriteLine(calc.ToString());
                Console.WriteLine();
            }
        }

    }
}