using CalculatorLibrary;

bool endApp = false;



// Display title as the C# console calculator app.
Console.WriteLine("Console Calculator in C#\r");
Console.WriteLine("------------------------\n");

Calculator calculator = new Calculator();



while (!endApp)
{
    // Declare variables and set to empty.
    string firstNumber = "";
    string secondNumber = "";
    double result = 0;

    // Ask the user to type the first number.
    if (!Calculator.useResult)
    {
        firstNumber = Calculator.GetNumber("Type a number, and then press Enter: ");
        bool firstValidationResult = Calculator.ValidateNumber(firstNumber);


        while (!firstValidationResult)
        {
            firstNumber = Calculator.GetNumber("This is not valid input. Please enter an integer value: ");
            firstValidationResult = Calculator.ValidateNumber(firstNumber);
        }

        Calculator.cleanFirstNumber = double.Parse(firstNumber);
    }
    // Ask the user to type the second number.
    secondNumber = Calculator.GetNumber("Type an another number, and then press Enter: ");
    bool secondValidationResult = Calculator.ValidateNumber(secondNumber);

    while (!secondValidationResult)
    {
        secondNumber = Calculator.GetNumber("This is not valid input. Please enter an integer value: ");
        secondValidationResult = Calculator.ValidateNumber(secondNumber);
    }

    Calculator.cleanSecondNumber = double.Parse(secondNumber);

    // Ask the user to choose an operator.
    string op = Calculator.GetCalculationOption();
    try
    {
        result = calculator.DoOperation(Calculator.cleanFirstNumber, Calculator.cleanSecondNumber, op);
        if (double.IsNaN(result))
        {
            Console.WriteLine("This operation will result in a mathematical error.\n");
        }
        else
        {
            Console.WriteLine("Your result: {0:0.##}\n", result);
            Calculator.AddOperation(Calculator.cleanFirstNumber, Calculator.cleanSecondNumber, result);
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
    }

    Console.WriteLine("------------------------\n");

    // Wait for the user to respond before closing.

    Console.Write("Press 'h' and Enter to show history of operations.\n");
    Console.Write("Press 'n' and Enter to close the app.\n");
    Console.Write("Press any other key and Enter to continue.\n");

    switch (Console.ReadLine())
    {
        case "n":
            endApp = true;
            break;
        case "h":
            Calculator.PrintHistory();
            Console.WriteLine("Press 'd' and Enter to delete history of operations.\n");

            Console.WriteLine("Press Enter to go to next calculation or press number calculation and Enter to use it in next calculation.\n");
            var userInput = Console.ReadLine();
            while (!Calculator.ValidateUserNumber(userInput))
            {

                userInput = Console.ReadLine();
            }

            var numbersCalculations = new List<int>(Calculator.historyResults.Keys);

            if (userInput == "d")
            {
                Calculator.historyResults.Clear();
                Console.Clear();
                Console.WriteLine("History is clear\n");
                Console.WriteLine("Press Enter to continue\n");
                Console.ReadLine();
                Console.Clear();
                Calculator.operationNumber = 0;
                Calculator.useResult = false;

            }
            else if (userInput == "")
            {
                Calculator.useResult = false;
                Console.Clear();
                break;

            }
            else if (numbersCalculations.Contains(int.Parse(userInput)))
            {
                var selectedResult = Calculator.historyResults[int.Parse(userInput)].result;
                Calculator.cleanFirstNumber = selectedResult;
                Calculator.useResult = true;
                Console.Clear();
                Console.WriteLine($"You have chosen a first number - {selectedResult}");
            }


            break;
        default:
            Calculator.useResult = false;
            break;
    }


    Console.WriteLine("\n"); // Friendly linespacing.
}
// Add call to close the JSON writer before return
calculator.Finish();