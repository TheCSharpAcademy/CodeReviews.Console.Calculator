using CalculatorLibrary;
public class CalculatorMain
{
    private static int previousResultIndex = -1;
    private static int usageCounter = 1;
    private static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Calculator app!\n");
        bool closeApp = false;
        Calculator calculator = new Calculator();

        while (!closeApp)
        {
            ShowMenu();
            string menuChoice = Validator.ValidateMenuChoice();
            (double number1, double number2) = GetUserInput(menuChoice);
            try
            {
                double result = calculator.DoOperation(menuChoice, number1, number2);
                DisplayResult(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong! An exception occurred.\nDetails: " + e.Message);
            }
            ChooseToDeleteOrShowHistory();
            closeApp = ChooseToClose();

            Console.Clear();
        }
        calculator.FinishWriter();
    }
    private static (double, double) GetUserInput(string menuChoice)
    {
        double number1 = 0.0d;
        double number2 = 0.0d;
        if (previousResultIndex >= 0 && previousResultIndex <= (Calculator.previousResults.Count - 1))
        {
            number1 = Calculator.previousResults[previousResultIndex];
            Console.WriteLine($"\nYour first number is: {number1}");
            previousResultIndex = -1;
        }
        else
        {
            Console.WriteLine("Please provide the first number: ");
            number1 = Validator.ValidateDoubleInput();
        }
        if (!menuChoice.Equals("sin") && !menuChoice.Equals("cos"))
        {
            Console.WriteLine("Please provide the second number: ");
            number2 = Validator.ValidateDoubleInput();
        }
        return (number1, number2);
    }
    private static void ShowMenu()
    {
        Console.WriteLine("Choose an option from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.WriteLine("\tp - Power (n1 to the power of n2)");
        Console.WriteLine("\tr - Root (root n2 of n1)");
        Console.WriteLine("\tsin - Sinus (sinus of n1)");
        Console.WriteLine("\tcos - Cosinus (cosinus of n1)");
        Console.Write("Your option? \n");
    }
    private static void DisplayResult(double result)
    {
        if (Validator.ValidateResultFinite(result))
            Console.WriteLine("\nThe result is: " + result);
        else
            Console.WriteLine("\nThis operation will result in a mathematical error.");
    }
    private static bool ChooseToClose()
    {
        Console.WriteLine("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
        if (Console.ReadLine()?.ToLower() == "n")
        {
            Console.WriteLine($"\nCalculator has been used {usageCounter} times in total. Press any key to close.");
            Console.ReadLine();
            return true;
        }
        else
        {
            usageCounter++;
            return false;
        }
    }
    private static void ChooseToDeleteOrShowHistory()
    {
        Console.WriteLine("\nType 'delete' and Enter to delete history of operations,\n" +
            "Type 'show' to display previous operations,\n" +
            "Press any other key and Enter to continue: ");

        string? choiceHistory = Console.ReadLine();
        if (choiceHistory is not null)
        {
            switch (choiceHistory)
            {
                case "delete":
                    Calculator.previousOperations.Clear();
                    Calculator.previousResults.Clear();
                    Console.WriteLine("\nYou have deleted the history of operations.");
                    break;
                case "show":
                    foreach (string operation in Calculator.previousOperations)
                    {
                        Console.WriteLine(operation);
                    }
                    previousResultIndex = ChoosePreviousResult();
                    break;
            }
        }
        else return;
    }
    private static int ChoosePreviousResult()
    {
        Console.WriteLine($"\nDo you want to use a previous result? If so, choose corresponding number: (0 - {Calculator.previousResults.Count - 1})");
        if (int.TryParse(Console.ReadLine(), out previousResultIndex))
            return previousResultIndex;
        else
            return -1;
    }
}