using System;
namespace CalculatorLibrary
{
	public class BaseOperations
	{
		public BaseOperations()
		{

		}
        public double Addition(double num1, double num2)
        {
            return num1 + num2;
        }
        public double Substraction(double num1, double num2)
        {
            return num1 - num2;
        }
        public double Multiplication(double num1, double num2)
        {
            return num1 * num2;
        }
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
            switch (oper.ToLower())
            {
                case "m":
                    return "*";
                case "s":
                    return "-";
                case "d":
                    return "/";
                case "a":
                    return "+";
                default:
                    throw new InvalidOperationException("\nInvalid operation. Try again, please");
            }
        }
        protected virtual string GetOperationName(string oper)
        {
            switch (oper)
            {
                case "M":
                    return "Multiplation";
                case "S":
                    return "Substraction";
                case "D":
                    return "Division";
                case "A":
                    return "Addition";
                default:
                    throw new InvalidOperationException("\nInvalid operation");
            }
        }


    }
}

