namespace ohshie.calculator;

public class Calculator
{
    public decimal Result { get; private set; }
    public decimal NumberA { get; private set; }
    public decimal NumberB { get; private set; }
    public string MathOperator { get; private set; }
    public int OperationIndex { get; }
    
    public void CalculatorApp()
    {
    bool doneCalculating;
    do
    {
        Console.Clear();

        Console.WriteLine($"Together we calculated {Program.NumberOfCalculations} equations");

        NumberA = GetNumber("first");

        MathOperator = GetOperator();

        NumberB = GetNumber("second");

        Program.NumberOfCalculations++;

        CalculateResult();

        Console.WriteLine($"Result of equation: {BuildEquation()}");

        doneCalculating = ContinueCheck();
        
    } while (doneCalculating == false);
    }

    decimal GetNumber(string index)
    {
        if (Program.NumberOfCalculations > 0)
        {
            Console.WriteLine($"Do you want to select previous results as {index} number?");
            if (!UserChoice("to enter number manually"))
            {
                return GetNumberFromPreviousResults();
            }
        }
        return GetUserInputNumber(index);
    }

    decimal GetUserInputNumber(string index)
    {
        Console.Write($"Enter {index} number: ");
        string userInput = Console.ReadLine();
        decimal approvedNumber;
    
        while (!decimal.TryParse(userInput, out approvedNumber))
        {
            Console.WriteLine("You entered invalid number or not a number at all!\n" +
                              "Enter new number: ");
            userInput = Console.ReadLine();
        }

        return approvedNumber;
    }

    decimal GetNumberFromPreviousResults()
    {
        Console.WriteLine("Previous calculations:");
        Log.ReadLog();
    
        uint userInput = 0;
        Console.WriteLine("Enter index of a result you want to use as a number:");
        userInput = Convert.ToUInt32(Console.ReadLine());
        
        return Log.ExtractResult(userInput);
    }

    string GetOperator()
    {
        string mathOperator;
        do 
        {
            Console.Write("Choose operation:\n" +
                          "1. +\n" +
                          "2. -\n" +
                          "3. *\n" +
                          "4. /\n" +
                          "press corresponding number: ");
            mathOperator = Console.ReadLine() switch
            {
                "1" => "+",
                "2" => "-",
                "3" => "*",
                "4" => "/",
                _ => ""
            };
        } while (mathOperator == "");
        return mathOperator;
    }

    bool ContinueCheck()
    {
        Console.WriteLine("Do you want to do another calculation?");
        return UserChoice("to exit");
    }

    bool UserChoice(string choice)
    {
        Console.WriteLine($"Press Y to continue, press anything else {choice}.");
        ConsoleKey consoleKey = Console.ReadKey(true).Key;
        return consoleKey != ConsoleKey.Y;
    }

    private void CalculateResult()
    {
        switch (MathOperator)
        {
            case "+":
                Result = NumberA + NumberB;
                break;
            case "-":
                Result = NumberA - NumberB;
                break;
            case "*":
                Result = NumberA * NumberB;
                break;
            case "/":
            {
                if (MathOperator == "/")
                {
                    while (NumberB == 0)
                    {
                        Console.WriteLine("Cant divide by 0! Please choose another number");
                        NumberB = Convert.ToDecimal(Console.ReadLine());
                    }
                }
                Result = NumberA / NumberB;
                break;
            }
        }
    }

    private string BuildEquation()
    {
        string equation = $"{NumberA}{MathOperator}{NumberB}={Result}";
        Log.WriteToLog(this);
        return equation;
    }
}