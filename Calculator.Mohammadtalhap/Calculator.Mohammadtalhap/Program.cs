using CalculatorLibrary;
using System.Text.RegularExpressions;

class Program
{
    public static void Main(String[] args)
    {

        bool endapp = false;
        Calculator cal = new Calculator();
        while (!endapp)
        {

            Console.Clear();

            Console.WriteLine("Console Calculator in C#");
            Console.WriteLine("-----------------------------------------------------");

            string? num1 = "";
            string? num2 = "";
            double result = 0;

            Console.Write("Enter first number :");
            num1 = Console.ReadLine();

            double cleannum1 = 0;
            while (!double.TryParse(num1, out cleannum1))
            {
                Console.Write("Invalid Input. Please enter a numerical value:");
                num1 = Console.ReadLine();
            }

            Console.Write("Enter second number :");
            num2 = Console.ReadLine();

            double cleannum2 = 0;
            while (!double.TryParse(num2, out cleannum2))
            {
                Console.Write("Invalid Input. Please enter a numerical value:");
                num2 = Console.ReadLine();
            }

            Console.WriteLine("Choose an option from the following list:");
            Console.Write("\ta - Add\n\ts - Subtraction\n\tm - Multiplication\n\td - Division\nYour Option:");
            string? op = Console.ReadLine();

            if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
            {
                Console.WriteLine("Error: Unrecognized Input.");
            }
            else
            {
                try
                {
                    result = cal.DoOperation(cleannum1, cleannum2, op);
                    if (double.IsNaN(result))
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    else
                        //Console.WriteLine("Your result: {0:0.##}", result);
                        Console.WriteLine($"Your result: {result:0.##}");

                }
                catch (Exception e)
                {
                    Console.Write("Oh no! An exception occured trying to do math.\n Details:", e.Message);
                }
            }
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Enter 'n' to end the app or any key to continue the app:");
            if (Console.ReadLine() == "n")
                endapp = true;
        }

        cal.Finish();
    }
}