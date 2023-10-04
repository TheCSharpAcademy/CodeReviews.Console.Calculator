namespace Calculator.wkktoria;

internal class Program
{
    private static void Main(string[] args)
    {
        var endApp = false;

        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        var calculator = new CalculatorLibrary.Calculator();

        while (!endApp)
        {
            var numInput1 = "";
            var numInput2 = "";
            double result = 0;

            Console.Write("Type a number, and then press enter: ");
            numInput1 = Console.ReadLine();

            double cleanNum1 = 0;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput1 = Console.ReadLine();
            }

            Console.Write("Type another number, and then press enter: ");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput2 = Console.ReadLine();
            }

            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            var op = Console.ReadLine();

            try
            {
                result = calculator.DoOperation(cleanNum1, cleanNum2, op);

                if (double.IsNaN(result))
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                else
                    Console.WriteLine("Your result: {0:0.##}\n", result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine("------------------------\n");


            Console.Write("Press 'n' and enter to close the app, or press any other key and enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n");
        }

        calculator.Finish();
    }
}