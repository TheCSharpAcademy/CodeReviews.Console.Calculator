using CalculatorLibrary;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            bool endMenu = false;
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            CalculatorList counter = new CalculatorList();
            int numberOfUses = counter.ReturnCount();

            Console.WriteLine($"This calculator has been used {numberOfUses} times");
            while (!endApp)
            {
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;

                while (!endMenu)
                {
                    Console.WriteLine("Welcome to the calculator app");
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("What would you like to do?");
                    Console.WriteLine("\tc - Enter calculator");
                    Console.WriteLine("\th - View history");
                    Console.WriteLine("\td - Delete History");
                    Console.WriteLine("\tq - Quit the program");

                    string? option = Console.ReadLine();
                    if (option == "c")
                    {
                        endMenu = true;
                    }
                    if (option == "h")
                    {
                        counter.ViewHistory();
                        Console.WriteLine("Press any key to continue");
                        Console.ReadLine();
                    }
                    if (option == "d")
                    {
                        File.Delete("History.txt");
                    }
                    if (option == "q")
                    {
                        endApp = true;
                        Environment.Exit(0);
                    }
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tr - Square root");
                Console.Write("Your option? ");

                string? operation = Console.ReadLine();

                // Ask the user to type the first number.
                Console.Write("Type a number, or M to use memory, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                if (numInput1.ToUpper().StartsWith("M"))
                {
                    try
                    {
                        cleanNum1 = counter.GetNumber();
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("No history file to read from");
                        continue;
                    }
                        
                }
                else
                {
                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput1 = Console.ReadLine();
                    }
                }

                // Ask the user to type the second number.
                Console.Write("Type another number, or M to use memory, and then press Enter: ");
                if (operation != "r")
                {
                    numInput2 = Console.ReadLine();
                }
                else
                {
                    numInput2 = "0";
                }

                double cleanNum2 = 0;
                if (numInput2.ToUpper().StartsWith("M"))
                {
                    try
                    {
                        cleanNum2 = counter.GetNumber();
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("No history file to read from");
                        continue;
                    }

                }
                else
                {
                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput2 = Console.ReadLine();
                    }
                }

                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, operation);
                    counter.WriteHistory(cleanNum1, cleanNum2, operation, result);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                    counter.CountCalculations();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'm' and Enter to return to the menu, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "m") endMenu = false;
                else endMenu = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }
    }
}