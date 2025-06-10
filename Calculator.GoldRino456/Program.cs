using System.Text.RegularExpressions;
using CalculatorLibrary.GoldRino456;

class Program
{
    static void Main(string[] args)
    {
        int totalCalculationsMade = 0;
        List<double> calculationsMade = new List<double>();
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");


        Calculator calculator = new Calculator();
        while (!endApp)
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            // Ask the user to type the first number.
            Console.Write("Type a number (or 'r' to use a previous result), and then press Enter: ");
            numInput1 = Console.ReadLine();

            //Validated Num Vars for calculations
            double cleanNum1 = 0;
            double cleanNum2 = 0;

            while (!double.TryParse(numInput1, out cleanNum1))
            {
                if(numInput1 != null && (numInput1.Equals("r") || numInput1.Equals("R")))
                {
                    if(calculationsMade.Count > 0)
                    {
                        double? previousResult = GetPreviousResult(calculationsMade);

                        if(previousResult != null)
                        {
                            cleanNum1 = previousResult.Value;
                            break;
                        }
                        else
                        {
                            Console.Write("Type a number (or 'r' to use a previous result), and then press Enter: ");
                            numInput1 = Console.ReadLine();
                            continue;
                        }
                    }
                    else
                    {
                        Console.Write("No previous results to select from! Please enter a numeric value: ");
                        numInput1 = Console.ReadLine();
                        continue;
                    }
                }

                Console.Write("This is not valid input. Please enter a numeric value (or 'r' to select a previous result): ");
                numInput1 = Console.ReadLine();
            }

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tr - Square Root");
            Console.WriteLine("\te - Exponential (Power Of...)");
            Console.WriteLine("\tx - 10x");
            Console.WriteLine("\tn - Sine");
            Console.WriteLine("\tc - Cosine");
            Console.WriteLine("\tt - Tangent");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r|e|x|n|c|t]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                //Prompt for second number if needed for operation
                if(Regex.IsMatch(op, "[a|s|m|d|e]"))
                {
                    // Ask the user to type the second number.
                    Console.Write("Type another number (or 'r' to use a previous result), and then press Enter: ");
                    numInput2 = Console.ReadLine();

                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        if (numInput2 != null && (numInput2.Equals("r") || numInput2.Equals("R")))
                        {
                            if (calculationsMade.Count > 0)
                            {
                                double? previousResult = GetPreviousResult(calculationsMade);

                                if (previousResult != null)
                                {
                                    cleanNum2 = previousResult.Value;
                                    break;
                                }
                                else
                                {
                                    Console.Write("Type another number (or 'r' to use a previous result), and then press Enter: ");
                                    numInput2 = Console.ReadLine();
                                    continue;
                                }
                            }
                            else
                            {
                                Console.Write("No previous results to select from! Please enter a numeric value: ");
                                numInput2 = Console.ReadLine();
                                continue;
                            }
                        }

                        Console.Write("This is not valid input. Please enter a numeric value (or 'r' to select a previous result): ");
                        numInput2 = Console.ReadLine();
                    }
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
                        totalCalculationsMade++;
                        calculationsMade.Add(result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.Clear();
        }

        Console.WriteLine($"Number of Calculations Made: {totalCalculationsMade}");
        calculator.Finish();
        return;
    }

    public static double? GetPreviousResult(List<double> calculationsMade)
    {
        Console.WriteLine("Choose a result from the following list (or type 'n' to return):");
        int i = 0;

        foreach(double result in calculationsMade)
        {
            Console.WriteLine($"\t{i} - {result}");
            i++;
        }

        Console.Write("Type a number (or 'n'), and then press Enter: ");
        string? input = Console.ReadLine();

        int cleanNum = -1;
        bool isValidInput = false;

        while (!isValidInput)
        {
            bool isValidIntegerInput = Int32.TryParse(input, out cleanNum);

            //Check if input outside list range
            if (input != null && isValidIntegerInput && cleanNum < calculationsMade.Count && cleanNum >= 0)
            {
                isValidInput = true;
                break;
            }
            else if(input != null && (input.Equals("n") || input.Equals("N")))
            {
                return null;
            }

            Console.Write("This is not valid input. Please enter a numeric value or 'n' to return: ");
            input = Console.ReadLine();
        }

        return calculationsMade[cleanNum];
    }
}