

using System.ComponentModel.DataAnnotations;

class Calculator
{
    public static double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        bool isSingleOperation = true;

        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                break;
            case "s":
                result = num1 - num2;
                break;
            case "m":
                result = num1 * num2;
                break;
            case "^":
                result = Math.Pow(num1, num2);
                break;
            case "sq":
                if (isSingleOperation)
                {
                    result = Math.Sqrt(num1);
                }
                else
                {
                    Console.WriteLine("Square root is a single operation, enter only one number.");
                }
                break;
            case "sin":
                if (isSingleOperation)
                {
                    result = Math.Sin(num1 * (Math.PI / 180));
                }
                else
                {
                    Console.WriteLine("sin is a single operation, enter only one number.");
                }

                break;
            case "cos":
                if (isSingleOperation)
                {

                    result = Math.Cos(num1 * (Math.PI / 180));

                }
                else
                {
                    Console.WriteLine("cos is a single operation, enter only one number.");
                }
                break;
            case "tan":
                if (isSingleOperation)
                {

                    result = Math.Tan(num1 * (Math.PI / 180));

                }
                else
                {
                    Console.WriteLine("tan is a single operation, enter only one number.");
                }
                break;
            case "10x":
                result = 10 * num1;
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                break;

            // Return text for an incorrect option entry.
            default:
                break;
        }
        return result;
    }



}

class Program
{
    static int count = 0;
    static List<string> calcHistory = new List<string>();

    static void Main(string[] args)
    {

        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");



        while (!endApp)
        {

            count++;
            // Declare variables and set to empty.
            string numInput1 = "";
            string numInput2 = "";
            double result = 0;





            // Ask the user to type the first number.
            Console.Write("Type the first number, and then press Enter: ");
            numInput1 = Console.ReadLine();


            //I'm thinking of a way to make the code skip asking for the 2nd number once the operator is Sqr or trigonmetry functions.
            double cleanNum1 = 0;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput1 = Console.ReadLine();
            }

            // Ask the user to type the second number.
            Console.Write("Type the 2nd number and then press Enter (or press only enter for a single operation):");
            numInput2 = Console.ReadLine();
            double cleanNum2 = 0;
            bool isSingleOperation = false;

            if (string.IsNullOrEmpty(numInput2))
            {
                isSingleOperation = true;
            }
            else
            {
                //cleanNum2 = double.Parse(numInput2);
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }
            }



            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tsq - Square root");
            Console.WriteLine("\t^ - Exponential");
            Console.WriteLine("\tsin - Sin");
            Console.WriteLine("\tcos - Cos");
            Console.WriteLine("\ttan - Tan");
            Console.WriteLine("\t10x - 10x");
            Console.WriteLine("\t");
            Console.Write("Your option? ");

            string op = Console.ReadLine();

            try
            {
                result = Calculator.DoOperation(cleanNum1, cleanNum2, op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");

                }
                else if (!isSingleOperation)
                {
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                    calcHistory.Add($"{cleanNum1} {op} {cleanNum2} = {result}");
                    //calcHistory.Add(string.Format("{0} {1} {2} = {3:0.##}", cleanNum1, op, cleanNum2, result));
                }
                else
                {
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                    calcHistory.Add($"{op} {cleanNum1} = {result}");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine("------------------------\n");

            Console.WriteLine("Calculator has been used " + count + " time(s)");

            Console.WriteLine("Calculation history:");
            foreach (string calc in calcHistory)
            {
                Console.WriteLine(calc);
            }

            while (true)
            {
                Console.WriteLine("Clear calculation history? (Y/N)");

                string clearHistory = Console.ReadLine();

                if (clearHistory == "y")
                {
                    calcHistory.Clear();
                    Console.WriteLine("Calculation history cleared");
                    break;
                }
                else if (clearHistory == "n")
                {
                    Console.WriteLine("Ok!");
                    Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                    if (Console.ReadLine() == "n")
                    {
                        endApp = true;
                    }

                    Console.WriteLine("\n"); // Friendly linespacing.

                    break;

                }
                else
                {
                    Console.WriteLine("Error! Please enter Y or N");
                }

            }
        }
        return;
    }
}