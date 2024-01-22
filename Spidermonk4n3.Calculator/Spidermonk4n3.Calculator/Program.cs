// See https://aka.ms/new-console-template for more information

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            while (!endApp)
            {

                double result = 0;

                Console.Write("Type a number, and then press Enter: ");
                string numInput = Console.ReadLine();

                double cleanNum1 = ParseNumberInputs(numInput);

                Console.Write("Type another number, and then press Enter: ");

                numInput = Console.ReadLine();
                double cleanNum2 = ParseNumberInputs(numInput);

                ChooseOperator();

                string op = Console.ReadLine();

                try
                {
                    result = Calculator.DoOperation(cleanNum1, cleanNum2, op);
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
                Console.WriteLine("\n");
            }
            return;
        }

        private static void ChooseOperator()
        {
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");
        }

        private static double ParseNumberInputs(string numInput)
        {
            double cleanNum = 0;
            while (!double.TryParse(numInput, out cleanNum))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                //Check if input is 
                numInput = Console.ReadLine();
            }

            return cleanNum;
        }
    }
}