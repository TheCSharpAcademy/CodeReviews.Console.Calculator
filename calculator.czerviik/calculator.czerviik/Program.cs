using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        public void setNumber(string numInput)
        {

        }
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------press any key to start-------\n");
            Console.ReadKey();
            Calculator calculator = new Calculator();

            while (!endApp)
            {
                // Declare variables and set to empty.
                double result = 0;
                double cleanNum1;
                double cleanNum2=double.NaN;
                Console.Clear();
                Console.WriteLine("Calculator used: {0} times.", Calculator.TimesUsed);

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tr - Square Root");
                Console.WriteLine("\tp - Taking the Power");
                Console.WriteLine("\tt - x10");
                Console.WriteLine("\ti - Sinus");
                Console.WriteLine("\to - Cosinus");

                Console.Write("Your option? ");

                string op = Console.ReadLine();

                // Ask the user to type the first number.
                Console.WriteLine("Type a number, and then press Enter");
                Console.WriteLine("or press 'P' to pick from previous results: ");

                calculator.SetInputNumber();
                cleanNum1 = calculator.GetCleanNumber();

                switch (op)
                {
                    case "r":
                    case "t":
                    case "i":
                    case "o":
                        break;
                    default:
                        {
                            // Ask the user to type the second number.
                            Console.Write("Type another number, and then press Enter: ");
                            Console.WriteLine("or press 'P' to pick from previous results: ");

                            calculator.SetInputNumber();
                            cleanNum2 = calculator.GetCleanNumber();
                        }
                        break;

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

                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;
                Console.WriteLine("Do you want to clear the previous results list? Y/N");
                if (Console.ReadLine().ToLower() == "y") calculator.PastResults.Clear();

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;

            void SetNumber(string numInput)
            {

            }
        }
    }
}
