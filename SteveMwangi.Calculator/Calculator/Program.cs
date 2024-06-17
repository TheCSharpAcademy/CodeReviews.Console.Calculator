using CalculatorLibrary;
using System.Text.RegularExpressions;
bool endProgram = false;

Console.Clear();
Console.WriteLine("Welcome!, lets do some computation\r");
Console.WriteLine(".....................................\n");

var calculator = new CalculatorLibrary.Calculator();

while (!endProgram)
{
    string? num1 = "";
    string? num2 = "";
    double result = 0;

   
    Console.Write("Please enter a number and press enter: ");
    num1 = Console.ReadLine();
    double correctNum1 = 0;

    while(!double.TryParse(num1, out correctNum1))
    {
        Console.Write("Invalid entry, please key in a numerical input");
        num1 = Console.ReadLine();
    }

    Console.Write("Please enter another number and press enter");
    num2 = Console.ReadLine();
    double correctNum2 = 0;
    while(!double.TryParse(num2, out correctNum2))
    {
        Console.Write("Inavalid entry, please key in a numerical input");
        num2 = Console.ReadLine();
    }

    Console.WriteLine("Please choose an operation");
    Console.WriteLine("\t a - Addition");
    Console.WriteLine("\t m - Multiplication");
    Console.WriteLine("\t d - Division");
    Console.WriteLine("\t s - Subtraction");
    Console.Write("Your option?");

    string? operation = Console.ReadLine().Trim().ToLower();
    if(operation == null || !Regex.IsMatch(operation, "[a|m|d|s]")){
        Console.WriteLine("Error! Invalid Input");
    }

    else
    {
        try
        {
            result = calculator.DoOperation(correctNum1, correctNum2, operation);
            if (double.IsNaN(result))
                {
                Console.WriteLine("This operation result in a mathematical error.\n");
            
            } else
            {
                Console.WriteLine("Your result: {0:0.##}\n", result);
            }

        } catch (Exception e)
        { 
            Console.WriteLine("An error occured while trying to execute the operation. Error Details:" + e.Message); 
        }
    Console.WriteLine("..........................\n");

    Console.WriteLine("Press the 'n' key and enter to close the app or press any other key to continue");
    if (Console.ReadLine() == "n") endProgram = true;
    Console.WriteLine("\n");
    }
 
}

calculator.Finish();






