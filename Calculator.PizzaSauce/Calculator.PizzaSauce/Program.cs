namespace Calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string operation = "";
            var counter = 0;
            var calcUses = 0;
            bool endApp = false;
            string op = "";
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            while (!endApp)
            {
                // Declare variables and set to empty.
                string numInput1 = "";
                string numInput2 = "";
                double cleanNum1 = 0;
                double cleanNum2 = 0;
                double result = 0;

                PrintOperators();
                Console.Write("Your option?");

                while (operation == "")
                {

                    op = Console.ReadLine();
                    switch (op)
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
                        case "q":
                            operation = "sqrt";
                            break;
                        case "p":
                            operation = "^";
                            break;
                        default:
                            Console.Clear();
                            PrintOperators();
                            Console.WriteLine("Please select a valid operator.");
                            break;
                    }
                }


                // Ask the user to type the first number.
                Console.Write("Type a number or press h to retrieve a previous result, and then press Enter: ");
                numInput1 = Console.ReadLine();
                numInput1 = ProcessInput(numInput1);

                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value or press h to retrieve a previous result: ");
                    numInput1 = Console.ReadLine();
                    numInput1 = ProcessInput(numInput1);
                }
                if (operation != "sqrt")
                {
                    // Ask the user to type the second number.
                    Console.Write("Type another number or press h to retrieve a previous result, and then press Enter: ");
                    numInput2 = Console.ReadLine();
                    numInput2 = ProcessInput(numInput2);

                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter a value or press h to retrieve a previous result: ");
                        numInput2 = Console.ReadLine();
                        numInput2 = ProcessInput(numInput2);
                    }
                }


                try
                {
                    result = Operations.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else if (operation == "sqrt")
                    {
                        Console.WriteLine($"Your result: {operation} {cleanNum1} = {result}");
                    }
                    else Console.WriteLine($"Your result: {cleanNum1} {operation} {cleanNum2} = {result}");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                if (Helpers.calcs.Count == 0)
                {
                    counter = 0;
                }

                counter++;
                calcUses++;
                Helpers.AddToHistory(cleanNum1, cleanNum2, operation, result, counter);
                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.WriteLine($"Calculations this session: {calcUses}");
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                operation = "";
                Console.Clear();
            }
            return;
        }
        static void PrintOperators()
        {
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tq - Square Root");
            Console.WriteLine("\tp - Taking the Power");
        }
        static string ProcessInput(string input)
        {
            if (input == "h" && Helpers.calcs.Count != 0)
            {
                Helpers.DisplayHistory();
                input = Helpers.GetPastResult();
                if (input == "c")
                {
                    Console.Write("Type a number or press h to retrieve a previous result, and then press Enter: ");
                    input = Console.ReadLine();
                }
            }
            else if (input == "c")
            {
                Helpers.ClearHistory();
                Console.WriteLine("History Cleared. Please input an integer: ");
                input = Console.ReadLine();
            }
            return input;
        }
    }
}