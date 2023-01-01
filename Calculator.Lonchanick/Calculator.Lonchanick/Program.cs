using System;

public class Program
{

    static double GetInputDouble()
    {
        double aux;
        string answer = Console.ReadLine();
        while (!double.TryParse(answer, out aux))
        {
            Console.WriteLine("Error, Type again:");
            answer = Console.ReadLine();
        }
        return aux;
    }

    static string GetValidOption()
    {
        //this mean option can't be more than one char and an option that is not allowed
        string ops = "asmd";
        string op = "";
        bool wh = true;
        while (wh)
        {
            op = Console.ReadLine();
            if (ops.IndexOf(op[0]) == -1 | op.Length > 1)
                Console.WriteLine("No es una opcion valida try again: ");
            else
                wh = false;
        }
        return op;
    }
    static void Main(string[] args)
    {

        // Declare variables and then initialize to zero.
        double num1 = 0; double num2 = 0;

        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        // Ask the user to type the first number.
        Console.WriteLine("Type a number, and then press Enter");
        num1 = GetInputDouble();

        // Ask the user to type the second number.
        Console.WriteLine("Type another number, and then press Enter");
        num2 = GetInputDouble();

        // Ask the user to choose an option.
        Console.WriteLine("Choose an option from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.Write("Your option? ");

        //pedimos los valores por teclado
        string op = GetValidOption();
        Calculator.DoOperation(num1,num2,op);
    }
}