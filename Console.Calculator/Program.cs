using CalculatorLibrary;
class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;

        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\r");

        Calculator calculator = new Calculator();
        while (!endApp)
        {
            string numInput1 = "";
            string numInput2 = "";
            double result = 0;
            double cleanNum1 = 0;
            double cleanNum2 = 0;

            Console.WriteLine("Type a number, and then press Enter");
            numInput1 = Console.ReadLine();

            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter a number.");
                numInput1 = Console.ReadLine();
            }
            
            Console.WriteLine("Type another number, and then press Enter");
            numInput2 = Console.ReadLine();

            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter a number.");
                numInput2 = Console.ReadLine();
            }

            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine(@"
a - Add
s - Subtract
m - Multiply
d - Divide");

            string operation = Console.ReadLine();
            operation?.Trim().ToLower();

            try
            {
                result = calculator.DoOperation(cleanNum1, cleanNum2, operation);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                } else
                {
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine("------------------------\n");
            Console.WriteLine("Press 'n' and Enter to close the app, or any other key followed by Enter to continue.");

            if (Console.ReadLine() == "n") endApp = true;
            Console.WriteLine("\n");
        }
        calculator.Finish();
        return;
    }
}