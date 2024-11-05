using CalculatorLibrary;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        

        Calculator calculator = new Calculator();

        while (!endApp)
        {
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            Console.Clear();
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("\t1 - Perform a new calculation");
            Console.WriteLine("\t2 - View previous results");
            Console.WriteLine("\t3 - Clear previous results");
            Console.WriteLine("\t4 - Exit");
            Console.Write("Your choice: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    double cleanNum1 = GetNumberFromUser(calculator, "Type a number for num1, or enter 'p' to use a previous result: ");
                    double cleanNum2 = GetNumberFromUser(calculator, "Type a number for num2, enter 'p' to use a previous result or press any key for single calculations: ");
                    Console.WriteLine("Choose an operator:");
                    // For single - operand calculation
                    if (double.IsNaN(cleanNum2))
                    {
                        Console.WriteLine("\tr - Square Root");
                        Console.WriteLine("\tx - 10x");
                        Console.WriteLine("\tq - Sin");
                        Console.WriteLine("\tw - Cos");
                        Console.WriteLine("\tt - Tan");
                    }
                    else
                    {
                        Console.WriteLine("\ta - Add");
                        Console.WriteLine("\ts - Subtract");
                        Console.WriteLine("\tm - Multiply");
                        Console.WriteLine("\td - Divide");
                        Console.WriteLine("\tp - Power");
                    }
                    
                    Console.Write("Your option? ");
                    string? op = Console.ReadLine();

                    if (op == null || !Regex.IsMatch(op, "[a|s|m|d|p|r|x|q|w|t]"))
                    {
                        Console.WriteLine("Error: Unrecognized input.");
                    } 
                    else
                    {
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
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("An exception occurred trying to do the math.\n - Details: " + e.Message);
                        }
                    }
                    break;

                case "2":
                    calculator.ShowPreviousResults();
                    break;

                case "3":
                    calculator.ClearPreviousCalculations();
                    break;

                case "4":
                    endApp = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }

            Console.WriteLine("------------------------\n");
            if (!endApp)
            {
                Console.Write("Press any key and Enter to continue...");
                Console.ReadLine();
            }
        }

        calculator.Finish();
        Console.WriteLine("Total calculator usage: {0} times.", calculator.GetUsageCount());
    }

    static double GetNumberFromUser(Calculator calculator, string prompt)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        double number;

        if (input == "p")
        {
            if (calculator.GetPreviousCalculations().Count > 0)
            {
                calculator.ShowPreviousResults();
                while (true)
                {
                    Console.WriteLine("Enter the ID of the previous result to use: ");
                    if (calculator.GetPreviousCalculations().Count == 1)
                    {
                        Console.WriteLine("You can just choose result number 1.");
                    }
                    else
                    {
                        Console.WriteLine($"Choose between 1 and {calculator.GetPreviousCalculations().Count}");
                    }
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        double? result = calculator.GetResultById(id);
                        if (result.HasValue)
                        {
                            return result.Value;
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID, try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please enter a numeric ID.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Previous Results are empty!");
                Console.WriteLine("Please enter a number: ");
                input = Console.ReadLine();
            }
        }
        else if (string.IsNullOrEmpty(input))
        {
            return double.NaN;
        }

        while (!double.TryParse(input, out number))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            input = Console.ReadLine();
        }

        return number;
    }
}