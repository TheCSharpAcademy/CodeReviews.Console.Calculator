// My Calculator followed by Microsoft tutorial. Added some additional functionalities from challenges list. Created more user-friendly menu system to manage the lists and history.
//
// After the first calculation user has an option to enter into the menu by pressing 'm' and Enter.
// In the menu user can:
// - View and manage history of previous calculations
// - Exit menu
// - Exit application
//
// Implemented challenges:
// - Track number of calculations (doesn't matter if it is successful or not)
// - Calculation history stored in the list and ability to show that for user
// - Ability for user to use values from history with suitable input
// - Power

using CalculatorLibrary.HopelessCoding;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            int calculationsCount = 1;
            Calculator calculator = new Calculator();

            // Displays title
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            while (!endApp)
            {
                // Variable declaration and initialization
                double num1 = 0;
                double num2 = 0;
                double result = 0;
                
                // Ask user input for the first number
                Console.WriteLine($"This is calculation number: {calculationsCount}");

                if (calculationsCount > 1)
                {
                    Console.WriteLine("If you want to use result values from the history, write 'h' and index of the value, for example 'h1'");
                }

                num1 = GetUserInput(calculator, "first");
                num2 = GetUserInput(calculator, "second");

                // Ask user to choose an option
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tp - Power");
                Console.Write("Your option?");

                string op = Console.ReadLine();

                try
                {
                    result = calculator.DoOperation(num1, num2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    calculationsCount++;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");
                Console.Write("Press 'm' and Enter to enter menu, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "m")
                {
                    showMenu(calculator);
                }
                Console.WriteLine();
            }

            // Call to close the JSON writer before return
            calculator.Finish();
        }

        private static double GetUserInput(Calculator calculator, string position)
        {
            int historyIndex = 0;
            double cleanNum = 0;
            string input = "";

            while (true)
            {
                Console.Write($"Type {position} number, and then press Enter: ");
                input = Console.ReadLine();

                if (input.StartsWith('h'))
                {
                    if (int.TryParse(input.Substring(1), out historyIndex))
                    {
                        input = calculator.GetHistoryValue(historyIndex);
                    }
                }

                if (double.TryParse(input, out cleanNum))
                {
                    return cleanNum;
                }
                else
                {
                    Console.Write("Index out of range, please try again\n");
                }
            }
        }

        private static void showMenu(Calculator calculator)
        {
            bool stayMenu = true;
            while (stayMenu)
            {
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\tl - Show history");
                Console.WriteLine("\tc - Clear history");
                Console.WriteLine("\te - Exit menu");
                Console.WriteLine("\tq - Quit application");
                Console.Write("Your option?");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "l":
                        calculator.showHistory();
                        break;
                    case "c":
                        calculator.clearHistory();
                        break;
                    case "e":
                        stayMenu = false;
                        break;
                    case "q":
                        stayMenu = false;
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}