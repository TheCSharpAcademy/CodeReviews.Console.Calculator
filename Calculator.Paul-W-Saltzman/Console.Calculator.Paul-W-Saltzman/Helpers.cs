using CalculatorLibrary;

namespace CalculatorProgram
{
    internal class Helpers
    {

        internal static double GetNumber(int number)
        {
            Console.Clear();
            double cleanNumber = 0;
            string numInput = "";
            if (number == 1)
            { Console.Write("Type a number, and then press Enter: "); }
            else
            { Console.Write("Type another number, and then press Enter: "); }

            numInput = Console.ReadLine();
            while (!double.TryParse(numInput, out cleanNumber))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput = Console.ReadLine();
            }

            return cleanNumber;


        }

        internal static void Result(double cleanNum, string op, Calculator calculator)
        {
            calculator.AddUse();
            try
            {
                double result = calculator.DoOperation(cleanNum, op);
                
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
        }

        internal static void Result(double cleanNum1, double cleanNum2, string op, Calculator calculator)
        {
            calculator.AddUse();
            try
            {
                double result = calculator.DoOperation(cleanNum1, cleanNum2, op);
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
        }

        internal static bool EndApp()
        {
        bool endApp = false;
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
            return endApp;
        }
    }

}

  

