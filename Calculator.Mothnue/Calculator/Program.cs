using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        Calculator calculator = new Calculator();
        bool endApp = false;
        // Title
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------------\n");

        while (!endApp)
        {
            // Declare variables and set to empty
            string numInput1 = "";
            string numInput2 = "";
            double result = 0;

            // Ask the user to type the first number
            Console.Write("Type a number and press Enter: ");
            numInput1 = Console.ReadLine();

            double cleanNum1 = 0;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("Enter a valid input: ");
                numInput1 = Console.ReadLine();
            }

            // Ask the user for the second number
            Console.Write("Type a number and press Enter: ");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("Enter a valid input: ");
                numInput2 = Console.ReadLine();
            }

            // Ask the user to choose an operator
            Console.WriteLine("Choose an operator:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write(">? ");

            string op = Console.ReadLine();

            try
            {
                result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in an error.\n");
                }
                else
                {
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }

            // Wait for the user before closing
            Console.WriteLine("----------------------------------------------------\n");
            Console.WriteLine("Press 'n' and Enter to close the app.");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Creating a space
        }
        return;
    }
}
