namespace CalculatorProgram;

using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        var calculator = new Calculator();
        var database = new Database("calculatordb.json");

        while (!endApp)
        {
            Console.WriteLine($"Calculator used {database.GetUsageCount()} times.");
            Console.WriteLine("------------------------");
            database.AddUsage();

            // Declare variables and set to empty.
            string numInput1 = "";
            string numInput2 = "";
            double result = 0;
            double cleanNum1 = 0;

            // Ask the user to type the first number.
            bool askForNum1;
            do
            {
                Console.Write("Type a number or 's' to select a result from your latest calculations, and then press Enter: ");
                numInput1 = Console.ReadLine();

                if (numInput1 == "s")
                {
                    var calculations = database.GetCalculations();
                    if (calculations != null && calculations.Count > 0)
                    {
                        cleanNum1 = SelectResult(calculations);    
                        askForNum1 = false;
                    }
                    else
                    {
                        Console.WriteLine("There are no latest calculations to select from yet.");
                        askForNum1 = true;
                    }
                }
                else
                {
                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput1 = Console.ReadLine();
                    }
                    askForNum1 = false;
                }
            } while (askForNum1);

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
                    database.AddCalculation(cleanNum1, cleanNum2, op, result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            string? input;
            do
            {
                Console.Write("Press 'n' and Enter to close the app, 'd' and Enter to delete latest calculations, or press any other key and Enter to continue: ");
                input = Console.ReadLine();
                if (input == "n")
                {
                    endApp = true;
                }
                else if (input == "d")
                {
                    database.DeleteCalculations();
                    Console.WriteLine("Latest calculations deleted.");
                }
            } while (input == "d");
            Console.WriteLine("\n"); // Friendly linespacing.
        }
        calculator.Finish();
        database.Close();
        return;
    }

    static double SelectResult(List<Calculation> calculations)
    {
        Console.WriteLine("Latest Calculations:");
        for (int i = 0; i < calculations.Count; i++)
        {
            Console.WriteLine($"#{i + 1}: {FormatCalculation(calculations[i])}");
        }

        int line;
        do
        {
            Console.Write("Please enter the line number to select a result: ");
            if (!int.TryParse(Console.ReadLine(), out line))
            {
                line = 0;
            }
        } while (line < 1 || line > calculations.Count);
        var result = calculations[line - 1].Result;
        Console.WriteLine($"Selected result: {result}");
        return result;
    }

    static string FormatCalculation(Calculation calculation)
    {
        return calculation.Op switch
        {
            "a" => String.Format("{0} + {1} = {2}", calculation.Num1, calculation.Num2, calculation.Result),
            "s" => String.Format("{0} - {1} = {2}", calculation.Num1, calculation.Num2, calculation.Result),
            "m" => String.Format("{0} * {1} = {2}", calculation.Num1, calculation.Num2, calculation.Result),
            "d" => String.Format("{0} / {1} = {2}", calculation.Num1, calculation.Num2, calculation.Result),
            _ => throw new NotImplementedException()
        };
    }
}