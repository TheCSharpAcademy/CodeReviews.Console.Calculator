using CalculatorLibrary;

namespace CalculatorProgram;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        int usageCounter = 0;

        Calculator calculator = new Calculator();

        while (!endApp)
        {
            Console.Clear();
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------");

            string numInput1 = "";
            string numInput2 = "";
            double result = 0;

            string userInput = "";
            double cleanNum1 = 0;
            double cleanNum2 = 0;

            if (calculator.history.Any())
            {
                while (userInput.ToLower().Trim() != "y" && userInput.ToLower().Trim() != "n")
                {
                    Console.WriteLine("Would you like to delete results history (y/n)?");
                    userInput = Console.ReadLine();
                }
                if (userInput.ToLower().Trim() == "y")
                {
                    calculator.history.Clear();
                    Console.WriteLine("Results history was cleared! Press any key to continue...");
                    Console.ReadKey();
                }
            }

            if (calculator.history.Any())
            {
                userInput = "";
                while (userInput.ToLower().Trim() != "y" && userInput.ToLower().Trim() != "n")
                {
                    Console.WriteLine("Would you like to use a previous result for a new calculation (y/n)?: ");
                    userInput = Console.ReadLine();
                }

                if (userInput.ToLower().Trim() == "y")
                {
                    foreach (var item in calculator.history)
                    {
                        Console.WriteLine($"{calculator.history.IndexOf(item) + 1}. result -> {item}");
                    }
                    Console.WriteLine("Select a number from the list to use in a new calculation");
                    string numIndex = Console.ReadLine();

                    int cleanIndex = 0;
                    while (!int.TryParse(numIndex, out cleanIndex) || cleanIndex <= 0 || cleanIndex > calculator.history.Count)
                    {
                        Console.Write("This is not a valid input. Please enter an integer value within the list range: ");
                        numIndex = Console.ReadLine();
                    }
                    cleanNum1 = calculator.history[cleanIndex - 1];
                    Console.WriteLine($"First calculation number is: {cleanNum1}\n");

                    Console.WriteLine("Select another number from the list to use in a new calculation");
                    numIndex = Console.ReadLine();

                    while (!int.TryParse(numIndex, out cleanIndex) || cleanIndex <= 0 || cleanIndex > calculator.history.Count)
                    {
                        Console.Write("This is not a valid input. Please enter an integer value within the list range: ");
                        numIndex = Console.ReadLine();
                    }
                    cleanNum2 = calculator.history[cleanIndex - 1];
                    Console.WriteLine($"First calculation number is: {cleanNum2}");
                }
                else
                {
                    Console.Write("Type a first number, and then press Enter: ");
                    numInput1 = Console.ReadLine();

                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput1 = Console.ReadLine();
                    }

                    Console.Write("Type a second number, and then press Enter: ");
                    numInput2 = Console.ReadLine();

                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput2 = Console.ReadLine();
                    }
                }
            }
            else
            {
                Console.Write("Type a first number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }
                Console.Write("Type a second number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }
            }

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
                    usageCounter++;
                    Console.WriteLine("You used the calculator {0} times", usageCounter);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine("------------------------\n");

            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n")
            {
                endApp = true;
            }

            Console.WriteLine("\n");
        }
        calculator.Finish();
        return;
    }
}