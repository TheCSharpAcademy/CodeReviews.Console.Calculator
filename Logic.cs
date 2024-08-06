using System.Text.RegularExpressions;

class Logic{

    static double result = 0;
    static int count = 1; 
    static double cleanNum1 = 0; 
  


    internal static double SetFirstNum(){
        Console.Clear();  
        Console.WriteLine("Enter a number, then type any key");
        var num1 = Console.ReadLine();

        double cleanNum1;
        while (string.IsNullOrWhiteSpace(num1) || !double.TryParse(num1, out cleanNum1))
        {
            Console.WriteLine("Please, enter a valid number");
            num1 = Console.ReadLine();
        }
        return cleanNum1; 
    } 

    internal static (string operation, int count) SetOp(){
        Console.Clear(); 
        Console.WriteLine(@$"Select one of the following operations:

+ - Add
- - Substract
* - Multiply
/ - Division
R - Square Root
P - Taking the Power
Sin - Sinus
Cos - Cosinus
Tan - Tangent
");

        var operation = Console.ReadLine();
        while (operation == null || !Regex.IsMatch(operation, @"^\+|\-|\*|/|r|p|sin|cos|tan$"))
        {
            Console.WriteLine("Error, unrecognized input, try again!");
            operation = Console.ReadLine(); 
        }

        if (operation == "r" || operation == "sin" || operation == "cos" || operation == "tan")
        {
            result = Calculator.DoOperation(cleanNum1, 0, operation); ;
            Console.WriteLine("Your result: {0:0.##}\n", result);
            count += 1;
        }
    
        return (operation, count); 

    }

    internal static double SetSecondNum(){
        Console.Clear(); 
        Console.WriteLine("Enter another number, then press any key");
        var num2 = Console.ReadLine();

        double cleanNum2 = 0;
        while (string.IsNullOrWhiteSpace(num2) || !double.TryParse(num2, out cleanNum2))
        {
            Console.WriteLine("Please, enter a valid number");
            num2 = Console.ReadLine();
        }
        return cleanNum2; 
    }

    internal static double TotalOperation(double cleanNum1, double cleanNum2, string operation){
        Console.Clear(); 
        result = Calculator.DoOperation(cleanNum1, cleanNum2, operation); 
        if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");

            }
            else
            {
                Console.WriteLine("Your result: {0:0.##}\n", result);
                count += 1;

            }

        return result; 
    }

}