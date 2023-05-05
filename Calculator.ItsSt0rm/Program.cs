using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();
        while (!endApp)
        {
            // Declare variables and set to empty.
            string menuOption = "";
            double result = 0;
            double cleanNum1 = 0;
            double cleanNum2 = 0;

            // Shows first menu
            calculator.ShowMenuOptions();

            menuOption = Console.ReadLine();
            Console.WriteLine(""); // Friendly linespacing.       
            calculator.ReadMenuOptions(menuOption);

            if (menuOption.Equals("n"))
            {
                return;
            }

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tr - Square Root");
            Console.WriteLine("\tp - Taking the power");
            Console.WriteLine("\tt - 10x");
            Console.WriteLine("\tz - Sin");
            Console.WriteLine("\tc - Cos");
            Console.WriteLine("\tx - Tan");
            Console.WriteLine("\tv - Cot");
            Console.WriteLine("\tb - Sec");
            Console.WriteLine("\tn - Csc");
            Console.Write("Your option? ");

            string op = Console.ReadLine();

            bool twoOperators = true;
            if (op.Equals("a") || op.Equals("s") || op.Equals("m") || op.Equals("d") || op.Equals("p"))
            {
                // Ask the user to type the first number.           
                cleanNum1 = calculator.ReadNumber();
                Console.WriteLine("First number saved");
                Console.WriteLine(""); // Friednly linespacing.

                // Ask the user to type the second number.
                cleanNum2 = calculator.ReadNumber();
                Console.WriteLine("Second number saved");
                Console.WriteLine(); // Friednly linespacing.
            }
            else
            {
                // Ask the user to type a number.           
                cleanNum1 = calculator.ReadNumber();
                Console.WriteLine("Number saved");
                Console.WriteLine(); // Friednly linespacing.
                twoOperators = false;
            }            

            try
            {
                result = (twoOperators == true) ? calculator.DoOperation(cleanNum1, cleanNum2, op) : calculator.DoOperation(cleanNum1, op);
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
            Console.WriteLine("Total operations done: {0}\n", calculator.OperationsDone);
            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        // Add call to close the JSON writer before return
        calculator.Finish();
        return;
    }
}