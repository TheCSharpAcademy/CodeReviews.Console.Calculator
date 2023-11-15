using CalculatorLibrary;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            int calculationCount = 0;

            List<string> latestCalculation = new List<string>();

            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                // Declare variables and set to empty.
                string numInput1 = "";
                string numInput2 = "";
                double result = 0;

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\tc - Calculate");
                Console.WriteLine("\ts - Show latest calculations");
                Console.WriteLine("\td - Delete latest calculations");
                Console.WriteLine("\t0 - To exit application");
                Console.Write("Your option? ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "c":
                        // Ask the user to type the first number.
                        Console.Write("Type a number, and then press Enter: ");
                        numInput1 = Console.ReadLine();

                        double cleanNum1 = 0;
                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter an integer value: ");
                            numInput1 = Console.ReadLine();
                        }

                        // Ask the user to type the second number.
                        Console.Write("Type another number, and then press Enter: ");
                        numInput2 = Console.ReadLine();

                        double cleanNum2 = 0;
                        while (!double.TryParse(numInput2, out cleanNum2))
                        {
                            Console.Write("This is not valid input. Please enter an integer value: ");
                            numInput2 = Console.ReadLine();
                        }

                        // Ask the user to choose an operator.
                        Console.WriteLine("Choose an operator from the following list:");
                        Console.WriteLine("\ta - Add");
                        Console.WriteLine("\ts - Subtract");
                        Console.WriteLine("\tm - Multiply");
                        Console.WriteLine("\td - Divide");
                        Console.Write("Your option? ");

                        string op = Console.ReadLine();
                        string operand = string.Empty;

                        if (op == "a") operand = "+";
                        if (op == "s") operand = "-";
                        if (op == "m") operand = "*";
                        if (op == "d") operand = "/";

                        try
                        {
                            result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                            if (double.IsNaN(result))
                            {
                                Console.WriteLine("This operation will result in a mathematical error.\n");
                            }
                            else
                            {
                                Console.WriteLine("Your result: {0:0.##}\n", result);
                                calculationCount++;
                                latestCalculation.Add($"{cleanNum1} {operand} {cleanNum2} = {result}");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                        }

                        Console.WriteLine("------------------------\n");

                        // Calculation count
                        Console.WriteLine($"Calculation count: {calculationCount}");

                        // Wait for the user to respond before closing.
                        Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                        if (Console.ReadLine() == "n") endApp = true;

                        Console.WriteLine("\n"); // Friendly linespacing.
                        break;
                    case "s":
                        foreach (var calculation in latestCalculation)
                        {
                            Console.WriteLine(calculation);
                        }
                        break;
                    case "d":
                        latestCalculation.Clear();
                        break;
                    case "0":
                        endApp = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }


            }
            calculator.Finish();
            return;
        }
    }
}