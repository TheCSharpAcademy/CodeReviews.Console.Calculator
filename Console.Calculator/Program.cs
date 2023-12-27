using CalculatorLibrary;
class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        int timesUsed = 0;

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
                    timesUsed++;
                    Console.WriteLine($"Amount of times the calculator was used: {timesUsed}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine("Want to take a look at your calculation history? Type: y - yes | n - no");

            if (Console.ReadLine() == "y") calculator.ShowHistory();

            Console.Clear();
            Console.WriteLine("------------------------");
            Console.WriteLine("Press 'n' and Enter to close the app, or Enter to continue.");

            if (Console.ReadLine() == "n") endApp = true;            
        }
        calculator.Finish();
        return;
    }
}