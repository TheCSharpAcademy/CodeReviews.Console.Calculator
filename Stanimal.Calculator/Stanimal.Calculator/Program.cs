using CalculatorLibrary;
using Stanimal.Calculator.Models;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;

            // count number of times the calculator was used.
            int numCalculatorUsed = 0;
            // Store a list with latest calculations.
            List<Calculation> calculations = new();

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

                // Display list of calculations
                if (calculations.Count > 0)
                {
                    displayCalculations(calculations);
                }

                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input.  Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input.  Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                if (calculations.Count > 0)
                {
                    Console.WriteLine("\treset - Delete Calculations");
                }
                Console.Write("Your option? ");

                string op = Console.ReadLine();
                if (op == "reset")
                {
                    calculations.Clear();
                    numCalculatorUsed = 0;
                    continue;
                }
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
                        // only count a successful result as part of number of times the calculator was used.
                        numCalculatorUsed++;
                        // add calculation to list
                        var calculation = new Calculation();
                        calculation.Operation = op;
                        calculation.OperandOne = cleanNum1;
                        calculation.OperandTwo = cleanNum2;
                        calculation.Result = result;
                        calculations.Add(calculation);
                        Console.WriteLine($"You used the calculator {numCalculatorUsed} times");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("---------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }

        static void displayCalculations(List<Calculation> calculations)
        {
            foreach (Calculation calculation in calculations)
            {
                // probably can add switch logic to correlate a to plus, s to minus either here or when I set a calculation's attributes above...
                Console.WriteLine($"{calculation.Operation}: {calculation.OperandOne} {calculation.Operation} {calculation.OperandTwo} = {calculation.Result}");
            }
        }
    }
}