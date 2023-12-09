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
            string numInput1 = "";
            string numInput2 = "";
            double result = 0;

            string userInput = "";
            double cleanNum1 = 0;
            double cleanNum2 = 0;
            bool hasHistory = calculator.history.Any();

            Console.Clear();
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------");

            if (hasHistory)
            {
                userInput = HelpersLibrary.ValidateYesOrNoInput(userInput, "Would you like to delete results history (y/n)?");
                if (userInput == "y")
                {
                    calculator.DeleteResultsHistory(calculator.history);
                }
            }

            hasHistory = calculator.history.Any();
            if (hasHistory)
            {
                userInput = "";
                userInput = HelpersLibrary.ValidateYesOrNoInput(userInput, "Would you like to use a previous result for a new calculation (y/n)?: ");
                
                if (userInput == "y")
                {
                    calculator.PrintResultsHistory(calculator.history);
                    Console.Write("Select a number from the list to use in a new calculation: ");
                    string numIndex = Console.ReadLine();
                    int cleanIndex = 0;

                    cleanIndex = HelpersLibrary.ValidateIndex(numIndex, calculator.history);
                    cleanNum1 = calculator.history[cleanIndex - 1];
                    Console.WriteLine($"First calculation number is: {cleanNum1}\n");

                    Console.Write("Select another number from the list to use in a new calculation: ");
                    numIndex = Console.ReadLine();

                    cleanIndex = HelpersLibrary.ValidateIndex(numIndex, calculator.history);
                    cleanNum2 = calculator.history[cleanIndex - 1];
                    Console.WriteLine($"Second calculation number is: {cleanNum2}");
                }
                else
                {
                    Console.Write("Type a first number, and then press Enter: ");
                    numInput1 = Console.ReadLine();
                    cleanNum1 = HelpersLibrary.ValidateNumberInput(numInput1);

                    Console.Write("Type a second number, and then press Enter: ");
                    numInput2 = Console.ReadLine();
                    cleanNum2 = HelpersLibrary.ValidateNumberInput(numInput2);
                }
            }
            else
            {
                Console.Write("Type a first number, and then press Enter: ");
                numInput1 = Console.ReadLine();
                cleanNum1 = HelpersLibrary.ValidateNumberInput(numInput1);

                Console.Write("Type a second number, and then press Enter: ");
                numInput2 = Console.ReadLine();
                cleanNum2 = HelpersLibrary.ValidateNumberInput(numInput2);

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