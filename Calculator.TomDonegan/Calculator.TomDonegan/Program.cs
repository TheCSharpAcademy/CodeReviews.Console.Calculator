using CalculatorLibrary;

namespace CalculatorProgram.TomDonegan
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            int numberOfUses = 0;

            History history = new History();
            List<Calculation> calculationsList = new List<Calculation>();
            Calculator calculator = new Calculator();

            while (!endApp)
            {
                Console.WriteLine("Console Calculator in C#\r");
                Console.WriteLine("------------------------\n");
                Console.WriteLine("Please make a selection:");
                Console.WriteLine("\tc - Calculator");
                Console.WriteLine("\th - Show Calculation History");
                Console.WriteLine("\td - Delete Calculation History");
                Console.WriteLine("\tx - Exit");
                Console.Write("Your option? ");
                string initialChoice = Console.ReadLine();
                Console.Clear();

                switch (initialChoice)
                {
                    case "c":
                        break;
                    case "h":
                        history.ShowHistory(calculationsList);
                        continue;
                    case "d":
                        calculationsList.Clear();
                        Console.WriteLine("If there was any history... it's gone now!! Bye Bye");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                    case "x":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine(
                            $"{initialChoice} is not a valid selection, try again.\n"
                        );
                        continue;
                }

                string numInput1 = "";
                string numInput2 = "";
                double result = 0;

                Console.WriteLine($"Calculations this run: {numberOfUses}\n");

                Console.Write(
                    "First Number - To use a value from a previous calculation, press 'h' or\ntype a number, and then press Enter: "
                );
                numInput1 = Console.ReadLine();
                Console.WriteLine("\n"); // Maintain console readability

                if (numInput1.ToLower() == "h")
                {
                    numInput1 = history.UsePreviousResult(calculationsList).ToString();
                }

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write(
                        "This is not valid input. Please enter 'h' or an integer value: "
                    );
                    numInput1 = Console.ReadLine();
                    Console.WriteLine("\n"); // Maintain console readability
                }

                Console.Write(
                    "Second Number - To use a value from a previous calculation, press 'h' or\ntype another number, and then press Enter: "
                );
                numInput2 = Console.ReadLine();

                if (numInput2.ToLower() == "h")
                {
                    numInput2 = history.UsePreviousResult(calculationsList).ToString();
                }

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }

                Console.Clear();
                Console.WriteLine($"Number 1: {cleanNum1}\nNumber 2: {cleanNum2}\n");

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
                    Dictionary<string, string> operatorConversion = new Dictionary<string, string>
                    {
                        { "a", "+" },
                        { "s", "-" },
                        { "m", "x" },
                        { "d", "/" }
                    };

                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"{cleanNum1} {operatorConversion[op]} {cleanNum2}");
                        Console.WriteLine("= {0:0.##}\n", result);
                        calculationsList.Add(
                            new Calculation
                            {
                                FirstNumber = cleanNum1,
                                SecondNumber = cleanNum2,
                                Operator = op,
                                Result = result
                            }
                        );
                        numberOfUses++;
                        history.ManageHistory(calculationsList);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        "Oh no! An exception occurred trying to do the math.\n - Details: "
                            + e.Message
                    );
                }

                Console.WriteLine("------------------------\n");

                Console.Write(
                    "Press 'n' and Enter to close the app, or press any other key and Enter to continue: "
                );
                if (Console.ReadLine() == "n")
                    endApp = true;

                Console.Clear();
            }
            calculator.Finish();
            return;
        }
    }
}
