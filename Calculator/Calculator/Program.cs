using System.Text.RegularExpressions;
using CalculatorLibrary;

class Program

{
    private static double[] getGeneralArguments(CalculationHistory history)
    {

        string? argChoice = null;
        string? numInput1 = "";
        string? numInput2 = "";
        double cleanNum1 = 0;
        double cleanNum2 = 0;

        do
        {
            Console.Write("Enter 'Y' to enter your own data and 'N' to use data from computation history ");
            argChoice = Console.ReadLine();
            if (argChoice == "Y" || argChoice == "N")
            {
                break;
            } else
            {
                continue;
            }
        } while (argChoice == null || argChoice == "" || argChoice != "Y" || argChoice != "N");

        if (argChoice == "Y")
        {

            // Ask the user to type the first number.
            Console.Write("Type a number, and " +
                "then press Enter: ");
            numInput1 = Console.ReadLine();

            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput1 = Console.ReadLine();
            }

            // Ask the user to type the second number.
            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();


            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput2 = Console.ReadLine();
            }

        }
        else if (argChoice == "N")
        {
            DisplayHistory(history);
            // Ask the user to type choice of index number.
            Console.Write("Enter the index of the first result to use: ");
            int index = 0;
            while (!int.TryParse(Console.ReadLine(), out index))
            {
                Console.Write("Enter the index of the first result to use: ");
            }

            try
            {
                double result = history.ReuseResult(index - 1);
                Console.WriteLine($"Using result: {result}");
                cleanNum1 = result;


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Ask the user to type choice of index number.
            Console.Write("Enter the index of the 2nd result to use: ");
            int index2 = 0;
            while (!int.TryParse(Console.ReadLine(), out index2))
            {
                Console.Write("Enter the index of the 2nd result to use: ");
            }

            try
            {
                double result2 = history.ReuseResult(index2 - 1);
                Console.WriteLine($"Using result: {result2}");
                cleanNum2 = result2;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

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

        
        while (numInput1 == "" || numInput1 == null)
        {
            Console.Write("This is not valid input. Please enter sin, cos or tan: ");
            numInput1 = Console.ReadLine();
        }

        // Ask the user to type the second number.
        Console.Write("Type the angle value, and then press Enter: ");
        numInput2 = Console.ReadLine();

       
        while (numInput2 == "" || numInput2 == null)
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput2 = Console.ReadLine();
        }
        return [numInput1, numInput2];
    }

    private static double getSquareRootArgument(CalculationHistory history)
    {
        string? numInput1 = "";
        string? argChoice = null;
        double cleanNum1 = 0;
        do
        {
            Console.Write("Enter 'Y' to enter your own data and 'N' to use data from computation history ");
            argChoice = Console.ReadLine();
            if (argChoice == "Y" || argChoice == "N")
            {
                break;
            }
            else
            {
                continue;
            }
        } while (argChoice == null || argChoice == ""  || argChoice != "Y" || argChoice != "N");

        if (argChoice == "Y")
        {

            // Ask the user to type the first number.
            Console.Write("Type a number whose Square root you wish to compute, and then press Enter: ");
            numInput1 = Console.ReadLine();


            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput1 = Console.ReadLine();
            }
        } 
        else if (argChoice == "N")
        {
            DisplayHistory(history);
            // Ask the user to type choice of index number.
            Console.Write("Enter the index of the result to use: ");
            int index = 0;
            while (!int.TryParse(Console.ReadLine(), out index))
            {
                Console.Write("Enter the index of the result to use: ");
            }

            try
            {
                double result = history.ReuseResult(index - 1);
                Console.WriteLine($"Using result: {result}");
                cleanNum1 = result;


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        return cleanNum1;
    }

    private static double getTenToPowerArgument(CalculationHistory history)
    {
        string? numInput1 = "";
        string? argChoice = null;
        double cleanNum1 = 0;
        do
        {
            Console.Write("Enter 'Y' to enter your own data and 'N' to use data from computation history ");
            argChoice = Console.ReadLine();
            if (argChoice == "Y" || argChoice == "N")
            {
                break;
            }
            else
            {
                continue;
            }
        } while (argChoice == null || argChoice == "" || argChoice != "Y" || argChoice != "N");

        if (argChoice == "Y")
        {

            // Ask the user to type the exponent to use.
            Console.Write("Type an exponent, and then press Enter: ");
            numInput1 = Console.ReadLine();

            
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput1 = Console.ReadLine();
            }
        }
        else if (argChoice == "N")
        {
            DisplayHistory(history);
            // Ask the user to type choice of index number.
            Console.Write("Enter the index of the result to use: ");
            int index = 0;
            while (!int.TryParse(Console.ReadLine(), out index))
            {
                Console.Write("Enter the index of the result to use: ");
            }

            try
            {
                double result = history.ReuseResult(index - 1);
                Console.WriteLine($"Using result: {result}");
                cleanNum1 = result;


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        return cleanNum1;
    }

    static void DisplayHistory(CalculationHistory history)
    {
        var calculations = history.GetHistory();
        if (calculations.Count == 0)
        {
            Console.WriteLine("No calculations in history.");
        }
        else
        {
            Console.WriteLine("--- Calculation History ---");
            for (int i = 0; i < calculations.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {calculations[i]}");
            }
        }
    }

    static void UseHistoryResult(Calculator calculator, CalculationHistory history)
    {
        DisplayHistory(history);
        Console.Write("Enter the index of the result to use: ");
        
        int.TryParse(Console.ReadLine(),out int index);
        double result = history.ReuseResult(index - 1);

        Console.WriteLine($"Using result: {result}");
       
    }

    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();
        CalculationHistory history = new CalculationHistory();

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
            Console.WriteLine("\tv - To view Calculation History");
            Console.WriteLine("\tw - To Clear History");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r|p|x|t|c|v|w]"))
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
                            double[] entriesArray = getGeneralArguments(history);
                            result = calculator.DoOperation(op, entriesArray[0], entriesArray[1]);
                            if(op == "a")
                            {
                                history.AddToHistory($"{entriesArray[0]} + {entriesArray[1]}={result}");
                            }
                            else if(op == "s")
                            {
                                history.AddToHistory($"{entriesArray[0]} - {entriesArray[1]}={result}");
                            }
                            else if (op == "m")
                            {
                                history.AddToHistory($"{entriesArray[0]} * {entriesArray[1]}={result}");
                            }
                            else if (op == "d")
                            {
                                history.AddToHistory($"{entriesArray[0]} / {entriesArray[1]}={result}");
                            }
                            else if (op == "p")
                            {
                                history.AddToHistory($"{entriesArray[0]} ^ {entriesArray[1]}={result}");
                            }

                                break;
                        case "r":
                            calculator.setNumberOfTimes();
                            double argVal = getSquareRootArgument(history);
                            result = calculator.DoOperation(op, argVal);
                            history.AddToHistory($"Sqrt Of {argVal}={result}");
                            break;
                        case "x":
                            calculator.setNumberOfTimes();
                            double argTenToPowerVal = getTenToPowerArgument(history);
                            result = calculator.DoOperation(op, argTenToPowerVal);
                            history.AddToHistory($" 10^{argTenToPowerVal}={result}");
                            break;
                        case "t":
                            calculator.setNumberOfTimes();
                            string[] entriesArray2 = getTrigFunctionArguments();
                            double v = Convert.ToDouble(entriesArray2[1]);
                            string w = entriesArray2[0];
                            result = calculator.DoOperation(op, functionName: w, angle: v);
                            history.AddToHistory($" {w} {v} degrees ={result}");
                            break;
                         case "c":
                            result = calculator.getNumberOfTimes();
                            break;
                         case "v":
                            DisplayHistory(history);
                            break;
                        case "w":
                            history.ClearHistory();
                            break;
                        
                        default:
                            break;
                    }

                    
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