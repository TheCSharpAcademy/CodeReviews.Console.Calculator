using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new();
            int useCount = 0;
            while (!endApp)
            {
                // Declare variables and set to empty.
                string numInput1 = "";
                string numInput2 = "";
                double result = 0;

                // Ask the user to type the first number.
                Console.Write("Type a number, or 'p' to choose a previous result, and then press Enter: ");
                numInput1 = Console.ReadLine()!;
                double cleanNum1 = 0;
                cleanNum1 = GetNumber(calculator, ref numInput1);



                // Ask the user to type the second number.
                Console.Write("Type another number, or 'p' to choose a previous result, and then press Enter: ");
                numInput2 = Console.ReadLine()!;
                double cleanNum2 = 0;
                cleanNum2 = GetNumber(calculator, ref numInput2);

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.Write("Your option? ");

                string op = Console.ReadLine()!;
                while (op == "d" && cleanNum2 == 0)
                {
                    Console.Write("Cannot divide by 0, Please enter a value: ");
                    numInput2 = Console.ReadLine()!;
                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter a value: ");
                        numInput2 = Console.ReadLine()!;
                    }
                }

                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                    useCount++;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");
                Console.WriteLine($"Times used: {useCount}");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }

        private static double GetNumber(Calculator calculator, ref string numInput1)
        {
            double cleanNum1;
            if (numInput1 == "p")
            {
                List<string> previousResults = new List<string>();
                foreach (Calculation calc in calculator.calculations)
                {
                    Console.Write($"\t{calculator.calculations.IndexOf(calc) + 1}: ");
                    Console.WriteLine(calc.ToString());
                    previousResults.Add(calc.Result.ToString());
                }
                Console.Write("Select one: ");
                numInput1 = Console.ReadLine()!;
                while (int.Parse(numInput1) > previousResults.Count || int.Parse(numInput1) < 1)
                {
                    Console.Write("That is not a valid choice. Select another: ");
                    numInput1 = Console.ReadLine()!;
                }
                numInput1 = previousResults[int.Parse(numInput1) - 1];
            }

            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter a value: ");
                numInput1 = Console.ReadLine()!;
            }

            return cleanNum1;
        }
    }
}