using System.Text.RegularExpressions;
using CalculatorLibrary;


class Program
{
    private static List <HistoryItem> history = new List<HistoryItem>();
    class HistoryItem
    {
        public int id;
        public double num1;
        public double num2;
        public string operation;
        public double result;

        public HistoryItem(int id, double num1, double num2, string operation, double result)
        {
            this.id = id;
            this.num1 = num1;
            this.num2 = num2;
            this.operation = operation;
            this.result = result;
        }
    }

    private static void AddToHistory(double num1, double num2, string operation, double result)
    {
        history.Add(new HistoryItem(history.Count+ 1, num1, num2, operation, result));
    }

    private static void showTimesUsed(List <HistoryItem> history)
    {
        Console.WriteLine($"The app was used: {history.Count} times");
    }

    private static void showHistory(List<HistoryItem> history)
    {
        foreach (HistoryItem item in history) {
            string? operation = null;
            double num1 = item.num1;
            double num2 = item.num2;
            switch (item.operation)
            {
                case "a":
                    operation = "+";
                    break;
                case "s":
                    operation = "-";
                    break;
                case "m":
                    operation = "*";
                    break;
                case "d":
                    operation = "/";
                    break;
                case "p1":
                    operation = "^";
                    break;
                case "p2":
                    operation = "^";
                    num1 = item.num2;
                    num2 = item.num1;
                    break;
                default:
                    Console.WriteLine(item.num1);
                    if (item.operation.Contains("2")) {
                        num1 = item.num2;
                        Console.WriteLine(num1);
                    }
                    break;
            }
            Console.WriteLine($"#{item.id} ..... {num1} {operation ?? ""} {(operation != null ? num2: "")} = {item.result}");
        }
        Console.WriteLine("Press 'd' to delete the history, or press any other button to exit to the main menu.");
        string userResponse = Console.ReadLine();
        if (userResponse == "d")
        {
            history.Clear();
        }
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
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            // Ask the user to type the first number.
            Console.Write("Type a number, or Id of the result to use it as a number like '#1' and then press Enter: ");
            numInput1 = Console.ReadLine();

            double cleanNum1 = 0;
            bool existNum1 = false;

            while (!existNum1)
            {
                if (numInput1.Contains('#'))
                {
                    numInput1 = numInput1.Remove(0, 1);
                    int numIndex1;
                    if (int.TryParse(numInput1, out numIndex1))
                    {
                        if (history[numIndex1 - 1].id == numIndex1)
                        {
                            cleanNum1 = history[numIndex1 - 1].result;
                            existNum1 = true;
                        }
                        else
                        {
                            Console.WriteLine("It seems we could not find the mentioned id, please try again.");
                            numInput1 = Console.ReadLine();
                        }
                    }
                }
                else
                {
                    if (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput1 = Console.ReadLine();
                    }
                    else
                    {
                        existNum1 = true;
                    }
                }
            }

            // Ask the user to type the second number.
            Console.Write("Type another number, or Id of the result to use it as a number like '#1' and then press Enter: ");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            bool existNum2 = false;


            while (!existNum2)
            {
                if (numInput2.Contains('#'))
                {
                    numInput2 = numInput2.Remove(0, 1);
                    int numIndex2;
                    if (int.TryParse(numInput2, out numIndex2))
                    {
                        if (history[numIndex2 - 1].id == numIndex2)
                        {
                            cleanNum2 = history[numIndex2 - 1].result;
                            existNum2 = true;
                        }
                        else
                        {
                            Console.WriteLine("It seems we could not find the mentioned id, please try again.");
                            numInput2 = Console.ReadLine();
                        }
                    }
                }
                else
                {
                    if (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput2 = Console.ReadLine();
                    } else
                    {
                        existNum2 = true;
                    }
                }
            }


            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operation from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tsq1 - Square Root for the First Number");
            Console.WriteLine("\tsq2 - Square Root for the Second Number");
            Console.WriteLine("\tp1 - Taking to the Power (num1^num2)");
            Console.WriteLine("\tp2 - Taking to the Power (num2^num1)");
            Console.WriteLine("\tten1 - 10x for the First Number");
            Console.WriteLine("\tten2 - 10x for the Second Number");
            Console.WriteLine("\ttri1 - Trigonometric Funtions for the First Number");
            Console.WriteLine("\ttri2 - Trigonometric Funtions for the Second Number");

            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            if (op != null && (op == "tri1" || op == "tri2"))
            {
                Console.WriteLine("Choose a trigonometric operation from the following list:");
                Console.WriteLine("\tacos1 - ACos for the First Number");
                Console.WriteLine("\tcos1 - Cos for the First Number");
                Console.WriteLine("\tasin1 - ASin Funtions for the First Number");
                Console.WriteLine("\tsin1 - Sin Funtions for the First Number");
                Console.WriteLine("\tatan1 - ATan for the First Number");
                Console.WriteLine("\ttan1 - Tan for the First Number");
                Console.WriteLine("\tacos2 - ACos for the Second Number");
                Console.WriteLine("\tcos2 - Cos for the Second Number");
                Console.WriteLine("\tasin2 - ASin Funtions for the Second Number");
                Console.WriteLine("\tsin2 - Sin Funtions for the Second Number");
                Console.WriteLine("\tatan2 - ATan for the Second Number");
                Console.WriteLine("\ttan2 - Tan for the Second Number");

                op = Console.ReadLine();
            }

            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "^(a|s|m|d|sq1|p1|ten1|sq2|p2|ten2|acos1|cos1|asin1|sin1|atan1|tan1|acos2|cos2|asin2|sin2|atan2|tan2)$"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            } 
            else
            {
                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    Console.WriteLine(result);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else {
                        if (Regex.IsMatch(op, "^(sq1|ten1|tri1|sq2|ten2|tri2)$")) cleanNum2 = 0;
                        AddToHistory(cleanNum1, cleanNum2, op, result);
                        Console.WriteLine("Your result: {0:0.##}\n", result); 
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, press 'h' to display the history, press 't' to see how many times the app was used, or press any other key and Enter to continue: ");
            string response = Console.ReadLine();
            if (response == "n") endApp = true;
            else if (response == "h") Program.showHistory(history);
            else if (response == "t") Program.showTimesUsed(history);
            Console.WriteLine("\n"); // Friendly linespacing.
        }
        calculator.Finish();
        return;
    }
}