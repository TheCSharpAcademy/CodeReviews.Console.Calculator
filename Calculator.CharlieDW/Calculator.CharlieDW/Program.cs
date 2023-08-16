namespace Calculator.CharlieDW;

internal class Program
{
    private static void Main(string[] args)
    {
        bool isGameOn = true;
        int timesCalcUsed = 0;

        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        while (isGameOn)
        {
            if(timesCalcUsed != 0)
            {
                Console.WriteLine("So far you've used the calculator {0} time(s)\n", timesCalcUsed);
            }

            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tr - Square Root");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("Your option? ");

            string? operation = Console.ReadLine();

            double num1;
            double num2;
            double result = 0;

            Console.WriteLine("Type a number, and then press Enter");
            string? num1Input = Console.ReadLine();

            while (Helpers.IsNotANumber(num1Input))
            {
                Console.WriteLine("Please provide a numeric value:");
                num1Input = Console.ReadLine();
            }

            num1 = Convert.ToDouble(num1Input);

            if(operation != "r")
            {
                Console.WriteLine("Type another number, and then press Enter");
                string? num2Input = Console.ReadLine();

                while (Helpers.IsNotANumber(num2Input))
                {
                    Console.WriteLine("Pleaser provide a numeric value:");
                    num2Input = Console.ReadLine();
                }

                num2 = Convert.ToDouble(num2Input);
            } else
            {
                num2 = 0;
            }

            try
            {
                result = Calculator.DoOperation(num1, num2, operation);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else
                {
                    Console.WriteLine($"Your result: {Math.Round(result, 2)}\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An error occurred trying to do the math. \nMore details: " + e.Message);
            }

            Console.WriteLine("--------------------------\n");

            Console.WriteLine("Press 'n' and Enter to close the app, or press any other key to continue");
            if (Console.ReadLine() == "n") isGameOn = false;

            Console.WriteLine("\n");
            Console.Clear();

            timesCalcUsed++;
        }
    }
}