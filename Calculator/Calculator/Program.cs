using System.Text.RegularExpressions;
using CalculatorLibrary;

class Program

{
    private static double[] getGeneralArguments()
    {
        string? numInput1 = "";
        string? numInput2 = "";

        // Ask the user to type the first number.
        Console.Write("Type a number, and " +
            "then press Enter: ");
        numInput1 = Console.ReadLine();

        double cleanNum1 = 0;
        while (!double.TryParse(numInput1, out cleanNum1))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput1 = Console.ReadLine();
        }

        // Ask the user to type the second number.
        Console.Write("Type another number, and then press Enter: ");
        numInput2 = Console.ReadLine();

        double cleanNum2 = 0;
        while (!double.TryParse(numInput2, out cleanNum2))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput2 = Console.ReadLine();
        }
        return [cleanNum1, cleanNum2];
    }

    private static string[] getTrigFunctionArguments()
    {
        string? numInput1 = "";
        string? numInput2 = "";

        // Ask the user to type the first number.
        Console.Write("Type either sin, cos or tan, and then press Enter: ");
        numInput1 = Console.ReadLine();

        //double cleanNum1 = 0;
        while (numInput1 == "" || numInput1 == null)
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput1 = Console.ReadLine();
        }

        // Ask the user to type the second number.
        Console.Write("Type another number, and then press Enter: ");
        numInput2 = Console.ReadLine();

        //double cleanNum2 = 0;
        while (numInput2 == "" || numInput2 == null)
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput2 = Console.ReadLine();
        }
        return [numInput1, numInput2];
    }

    private static double getSquareRootArgument()
    {
        string? numInput1 = "";

        // Ask the user to type the first number.
        Console.Write("Type a number, and then press Enter: ");
        numInput1 = Console.ReadLine();

        double cleanNum1 = 0;
        while (!double.TryParse(numInput1, out cleanNum1))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput1 = Console.ReadLine();
        }

        return cleanNum1;
    }

    private static double getTenToPowerArgument()
    {
        string? numInput1 = "";

        // Ask the user to type the first number.
        Console.Write("Type a number, and then press Enter: ");
        numInput1 = Console.ReadLine();

        double cleanNum1 = 0;
        while (!double.TryParse(numInput1, out cleanNum1))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput1 = Console.ReadLine();
        }

        return cleanNum1;
    }
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();

        while (!endApp)
        {
            double result = 0;

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tr - Square root");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("\tx - 10th Power");
            Console.WriteLine("\tt - Trigonometric Function");
            Console.WriteLine("\tc - To see the number of times the Calculator has been used");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r|p|x|t|c]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    
                    switch (op)
                    {
                        case "a":
                        case "s":
                        case "m":
                        case "d":
                        case "p":
                            calculator.setNumberOfTimes();
                            double[] entriesArray = getGeneralArguments();
                            result = calculator.DoOperation(op, entriesArray[0], entriesArray[1]);
                            break;
                        case "r":
                            calculator.setNumberOfTimes();
                            result = calculator.DoOperation(op, getSquareRootArgument());
                            break;
                        case "x":
                            calculator.setNumberOfTimes();
                            result = calculator.DoOperation(op, getTenToPowerArgument());
                            break;
                        case "t":
                            calculator.setNumberOfTimes();
                            string[] entriesArray2 = getTrigFunctionArguments();
                            double v = Convert.ToDouble(entriesArray2[1]);
                            string w = entriesArray2[0];
                            result = calculator.DoOperation(op, functionName: w, angle: v);
                            break;
                         case "c":
                            result = calculator.getNumberOfTimes();
                            break;
                        // Return text for an incorrect option entry.
                        default:
                            break;
                    }

                    //result = calculator.DoOperation(op);
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred.\n - Details: " + e.Message);
                }
            }
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