using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        bool useLastResult = false;
        double result = 0;
        double cleanNum1 = 0;
        string menuOption = string.Empty;

        // Display title as the C# console calculator app.
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();
        while (!endApp)
        {
            // Declare variables and set to empty.
            string numInput1 = "";
            string numInput2 = "";


            if (!useLastResult)
            {
                cleanNum1 = 0;
                // Ask the user to type the first number.
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Type a number, and then press Enter: ");
                Console.ForegroundColor = ConsoleColor.White;
                numInput1 = Console.ReadLine();

                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    numInput1 = Console.ReadLine();
                }
            }
            else
            {
                useLastResult = true;
            }

            // Ask the user to type the second number.
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Type another number, and then press Enter: ");
            Console.ForegroundColor = ConsoleColor.White;
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("This is not valid input. Please enter an integer value: ");
                Console.ForegroundColor = ConsoleColor.White;
                numInput2 = Console.ReadLine();
            }

            // Ask the user to choose an operator.
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tr - Square Root (applied to the last number)");
            Console.WriteLine("\tp - Taking the Power (first number is base, second number is exponent)");
            Console.WriteLine("\tx - 10x (applied to the last number)");
            Console.WriteLine("\tt - Trigonometry Function Sin (applied to the last number)");
            Console.Write("Your option? ");

            Console.ForegroundColor = ConsoleColor.White;
            string op = Console.ReadLine();

            try
            {
                result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                if (double.IsNaN(result))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Your result: {result}");
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }


            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("------------------------\n");
                // Wait for the user to respond before closing.
                Console.WriteLine("Press 'n' to close the app");
                Console.WriteLine("Press 'u' to view the amount of times the calculator was used");
                Console.WriteLine("Press 'v' to view the latest calculations");
                Console.WriteLine("Press 'l' to use the last result");
                Console.Write("Press any other key and Enter to continue: ");

                Console.ForegroundColor = ConsoleColor.White;
                menuOption = Console.ReadLine();
                switch (menuOption)
                {
                    case "n":
                        endApp = true;
                        break;
                    case "u":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Uses: " + calculator.GetUsesCount());
                        break;
                    case "v":
                        calculator.PrintLastCalculations();
                        break;
                    case "l":
                        cleanNum1 = result;
                        useLastResult = true;
                        break;
                }
            } while (menuOption.Equals("u") || menuOption.Equals("v"));

            

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        calculator.Finish();
        return;
    }
}