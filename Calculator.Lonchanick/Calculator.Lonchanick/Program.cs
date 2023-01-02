using System;
using Calculator.Lonchanick;

namespace Calculator.Lonchanick
{
    public class Program
    {
        static void Main(string[] args)
        {
            bool flag=false;
            Calculator calc = new Calculator();
            while (!flag) 
            {
                // Declare variables and then initialize to zero.
                double num1 = 0; double num2 = 0;

                // Display title as the C# console calculator app.
                Console.WriteLine("Console Calculator in C#\r");
                Console.WriteLine("------------------------\n");

                // Ask the user to type the first number.
                Console.WriteLine("Type a number, and then press Enter");
                num1 = ToolBox.GetInputDouble();

                // Ask the user to type the second number.
                Console.WriteLine("Type another number, and then press Enter");
                num2 = ToolBox.GetInputDouble();

                // Ask the user to choose an option.
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.Write("Your option? ");

                //pedimos los valores por teclado
                string op = ToolBox.GetValidOption();
                //mandamos parametros a la funcion DoOperations para hacer el calculo
                calc.DoOperation(num1, num2, op);
                Console.WriteLine("Press q to close app, or press any other key to continue.. ");
                if(Console.ReadLine() == "q") flag= true;
            }
            calc.Finish();
        }
    }
}
