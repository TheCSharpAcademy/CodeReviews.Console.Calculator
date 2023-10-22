using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        Calculator calculator = new Calculator();
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        while (!endApp)
        {
            printMenu();
            string op = Console.ReadLine();

            switch (op)
            {
                case Constants.CALCULATION_TIMES:
                    calculator.printCalculationTimes();
                    break;
                case Constants.CALCULATION_HISTORY:
                    calculator.PrintHistory();
                    break;
                case Constants.CLEAR_HISTORY:
                    calculator.ClearHistory();
                    break;
                case Constants.USE_HISTORY:
                    ReuseCalculation(calculator);
                    break;
                default:
                    double firstNumber = GetFirstNumber();
                    double secondNumber = GetSecondNumber();
                    DoCalculation(calculator, op, firstNumber, secondNumber);
                    break;
            }

            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }

        // Write the calculation history before return
        calculator.writeHistory();

        return;
    }

    private static double GetFirstNumber()
    {
        Console.Write("Type the first number, and then press Enter: ");
        string numInput1 = Console.ReadLine();

        double cleanNum1 = 0;
        while (!double.TryParse(numInput1, out cleanNum1))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            numInput1 = Console.ReadLine();
        }
        return cleanNum1;
    }

    private static double GetSecondNumber()
    {
        Console.Write("Type the second number, and then press Enter: ");
        string numInput2 = Console.ReadLine();

        double cleanNum2 = 0;
        while (!double.TryParse(numInput2, out cleanNum2))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            numInput2 = Console.ReadLine();
        }

        return cleanNum2;
    }

    private static void DoCalculation(Calculator calculator, string op, double cleanNum1, double cleanNum2)
    {
        try
        {
            double result = calculator.DoOperation(cleanNum1, cleanNum2, op);
            if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else Console.WriteLine("Your result: {0:0.##}\n", result);
        }
        catch (Exception e)
        {
            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
        }
    }

    private static void ReuseCalculation(Calculator calculator)
    {
        var cnt = calculator.getTotalCalculationTimes();
        if (cnt < 1)
        {
            Console.WriteLine("There is no calculation can be resued.");
            return;
        }
        calculator.PrintHistory();
        Console.WriteLine($"Please choose the calculation id (1-{cnt}) you want to resue:");
        string id = Console.ReadLine();
        int cleanId = 0;
        while (!int.TryParse(id, out cleanId) || cleanId < 0 || cleanId > cnt)
        {
            Console.Write($"This is not valid input. Please enter a valid id(1-{cnt}): ");
            id = Console.ReadLine();
        }
        double cleanNum1 = calculator.GetCalculationResultById(cleanId - 1);
        Console.WriteLine($"The fist number is {cleanNum1}");
        double cleanNum2 = GetSecondNumber();
        Console.WriteLine("Please choose a operation:");
        PrintCalculationOperation();
        string operation = Console.ReadLine();
        DoCalculation(calculator, operation, cleanNum1, cleanNum2);
    }

    private static void PrintCalculationOperation()
    {
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
    }

    private static void printMenu()
    {
        Console.WriteLine("Choose an operator from the following list:");
        PrintCalculationOperation();
        Console.WriteLine("\tt - Show the times of your history calculation");
        Console.WriteLine("\th - Show the calculation history");
        Console.WriteLine("\tc - Clear the calculation history");
        Console.WriteLine("\tu - Use the result of the calculation history");
        Console.Write("Your option? ");
    }
}