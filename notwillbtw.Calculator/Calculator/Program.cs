using System.Text.RegularExpressions;
using CalculatorLibrary;
using Helpers;
using LoggingHandlers;
using Objects;


class Program
{
    public static LoggingHandling loggingHandling = new LoggingHandling();

    static void Main(string[] args)
    {
        loggingHandling.Start();

        int timesUsed = 0;

        bool endApp = false;

        while (!endApp)
        {
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Console.WriteLine("Use calculator (c) or view past uses (v) or quit the program (q)");
            string? choice = Console.ReadLine();

            if (choice == "v")
            {
                Helpers.UsesListFunctions.PrintUses();
                Console.WriteLine("\nDelete list (D), Use past use for calculation (P) or Enter any key and press enter to return to calculator.");
                string? input = Console.ReadLine();

                if (input.ToLower() == "d")
                {
                    Helpers.UsesListFunctions.DeleteUses();
                    Console.ReadLine();
                }
                else if (input.ToLower() == "p")
                {
                    if (Objects.Uses.uses.Count() != 0)
                    {
                        UsePastCalculation();

                        timesUsed++;

                        Console.Write($"You have used the calculator {timesUsed} time(s). Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                        if (Console.ReadLine() == "n") endApp = true;
                    }
                    else 
                    {
                        Console.WriteLine("No calculations to use. Press enter to return to calculator.");
                        Console.ReadLine();
                    
                    }

                }
                
                Console.Clear();
                continue;
            }
            else if (choice == "c")
            {
                timesUsed++;

                Calculator();

                Console.Write($"You have used the calculator {timesUsed} time(s). Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;
            }
            else if (choice == "q")
            {
                loggingHandling.Finish();
                return;
            }
            else
            {
                Console.WriteLine("invalid input.");
            }

            

            Console.Clear();

        }
            
    }

    internal static void Calculator()
    {
        CalculatorMethods calculator = new CalculatorMethods();

        string? numInput1 = "";
        string? numInput2 = "";
        double result = 0;

        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.WriteLine("\tr - Square root");
        Console.WriteLine("\tp - power");
        Console.Write("Your option? ");

        string? op = Console.ReadLine();

        while (String.IsNullOrEmpty(op) || !Regex.IsMatch(op, "[a|s|m|d|r||p]"))
        {
            Console.WriteLine("invalid input. enter a given option: ");
            op = Console.ReadLine();
        }

        Console.Write("Type a number, and then press Enter: ");
        numInput1 = Console.ReadLine();

        double cleanNum1 = 0;
        while (!double.TryParse(numInput1, out cleanNum1))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput1 = Console.ReadLine();
        }

        double cleanNum2 = 0;
        if (op != "r")
        {
            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();

            while (numInput2 != "n" && !double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput2 = Console.ReadLine();
            }
        }

        try
        {
                
            result = calculator.DoOperation(cleanNum1, cleanNum2, op);
            loggingHandling.LogCalculation(op, cleanNum1, cleanNum2, result);

            if (op == "r" && cleanNum1 < 0)
            {
                Console.WriteLine("Negatives can't have a square root.\n");
            }
            else if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else
            {
                Console.WriteLine("Your result: {0:0.##}\n", result);

                UsesListFunctions.AddToHistory(cleanNum1, cleanNum2, result, op);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
        }

        Console.WriteLine("------------------------\n");

        

    }

    public static void UsePastCalculation()
    {
        CalculatorMethods calculatorMethods = new CalculatorMethods();

        double result = 0;

        Console.WriteLine("Enter Index of calculation you want to use: ");
        string? calcIndexInput = Console.ReadLine();

        int calcIndex = 0;
        while (!int.TryParse(calcIndexInput, out calcIndex))
        {
            Console.WriteLine("Input invalid, please only input numeric values");
            calcIndexInput = Console.ReadLine();
        }

        while (!int.TryParse(calcIndexInput, out calcIndex) || calcIndex > Objects.Uses.uses.Count() || calcIndex < 0)
        {
            Console.WriteLine("Input is not a valid index, please only input an index in the list.\n");
            Console.WriteLine("Enter Index of calculation you want to use: ");
            calcIndexInput = Console.ReadLine();

            calcIndex = 0;
            while (!int.TryParse(calcIndexInput, out calcIndex))
            {
                Console.WriteLine("Input invalid, please only input numeric values");
                calcIndexInput = Console.ReadLine();
            }
        }
        calcIndex = Objects.Uses.uses.Count() - calcIndex;

        Console.WriteLine($"selected calculation: {Objects.Uses.uses[calcIndex].Operand1} {Objects.Uses.uses[calcIndex].Op} {Objects.Uses.uses[calcIndex].Operand2} = {Objects.Uses.uses[calcIndex].Result}");
        Console.WriteLine("Would you  like to use the first operand (1), second operand (2), or result (3)?");
        string? usedComponentInput = Console.ReadLine();

        while (String.IsNullOrEmpty(usedComponentInput) || !Regex.IsMatch(usedComponentInput, "[1|2|3]"))
        {
            Console.WriteLine("enter valid input.");
            usedComponentInput = Console.ReadLine();
        }

        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.WriteLine("\tr - Square root");
        Console.WriteLine("\tp - power");
        Console.Write("Your option? ");

        string? op = Console.ReadLine();

        while (String.IsNullOrEmpty(op) || !Regex.IsMatch(op, "[a|s|m|d|r||p|t]"))
        {
            Console.WriteLine("invalid input. enter a given option: ");
            op = Console.ReadLine();
        }

        string? componentLocation = "1";
        if (op != "r")
        {
            Console.WriteLine("Would you like to use it as the first operand (1) or second operand (2)?");
            componentLocation = Console.ReadLine();
        }
        double num1 = 0;
        double num2 = 0;

        switch (componentLocation)
        {
            case "1":

                switch (usedComponentInput)
                {
                    case "1":
                        num1 = Convert.ToDouble(Uses.uses[calcIndex].Operand1);
                        break;
                    case "2":
                        num1 = Convert.ToDouble(Uses.uses[calcIndex].Operand2);
                        break;
                    case "3":
                        num1 = Convert.ToDouble(Uses.uses[calcIndex].Result);
                        break;
                }

                if (op != "r")
                {
                    Console.WriteLine("Enter second number: ");
                    string? num2Input = Console.ReadLine();

                    num2 = 0;
                    while (!double.TryParse(num2Input, out num2))
                    {
                        Console.WriteLine("Input invalid, please input a numeric value.");
                        num2Input = Console.ReadLine();
                    }
                }

                try
                {
                    result = calculatorMethods.DoOperation(num1, num2, op);
                    loggingHandling.LogCalculation(op, num1, num2, result);

                    if (op == "r" && num1 < 0)
                    {
                        Console.WriteLine("Negatives can't have a square root.\n");
                    }
                    else if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                        UsesListFunctions.AddToHistory(num1, num2, result, op);

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("\n------------------------\n");


                Console.WriteLine("");
                break;

            case "2":
                Console.WriteLine("Enter first number: ");
                string? num1Input = Console.ReadLine();

                num1 = 0;
                while (!double.TryParse(num1Input, out num1))
                {
                    Console.WriteLine("Input invalid, please provide a numeric value.");
                    num1Input = Console.ReadLine();
                }

                switch (usedComponentInput)
                {
                    case "1":
                        num2 = Convert.ToDouble(Uses.uses[calcIndex].Operand1);
                        break;
                    case "2":
                        num2 = Convert.ToDouble(Uses.uses[calcIndex].Operand2);
                        break;
                    case "3":
                        num2 = Convert.ToDouble(Uses.uses[calcIndex].Result);
                        break;
                }
                try
                {
                    result = calculatorMethods.DoOperation(num1, num2, op);
                    loggingHandling.LogCalculation(op, num1, num2, result);

                    if (op == "r" && num1 < 0)
                    {
                        Console.WriteLine("Negatives can't have a square root.\n");
                    }
                    else if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                        UsesListFunctions.AddToHistory(num1, num2, result, op);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("\n------------------------\n");

                

                Console.WriteLine("");

                break;

            default:
                Console.WriteLine("please enter a given option.");
                break;
        }
    }
}
