using System;
namespace CalculatorLibrary
{
    public class BaseOperations
    {
        public BaseOperations()
        {

        }
        public double Addition(double num1, double num2) => num1 + num2;

        public double Substraction(double num1, double num2) => num1 - num2;

        public double Multiplication(double num1, double num2) => num1 * num2;

        public double Division(double num1, double num2)
        {
            try
            {
                return num1 / num2;
            }
            catch
            {
                Console.Error.WriteLine("Cannot divide by zero!");
                return 0;
            }

        }
        public virtual string GetOperation(string oper)
        {
            return oper.ToLower() switch
            {
                "m" => "*",
                "s" => "-",
                "d" => "/",
                "a" => "+",
                _ => throw new InvalidOperationException("\nInvalid operation. Try again, please")
            };
        }
        protected virtual string GetOperationName(string oper)
        {
            return oper switch
            {
                "M" => "Multiplation",
                "S" => "Substraction",
                "D" => "Division",
                "A" => "Addition",
                _ => throw new InvalidOperationException("\nInvalid operation"),
            };
        }
    }
}
