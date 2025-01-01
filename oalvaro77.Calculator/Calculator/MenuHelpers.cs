using System.Text.RegularExpressions;

namespace Calculator
{

    internal class MenuHelpers
    {
        Input input = new Input();
        Calculator calculator = new Calculator();
        internal double ExecuteCalculation(double num1, double num2, string op)
        {
            try
            {
                double result = Calculator.DoOperation(num1, num2, op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error");
                }
                return result;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Oh no! An exception ocurred to do math.\n -Details:" + ex.Message);
                return double.NaN;
            }

        }

        internal double PerfomOp(string op)
        {
            double num1 = 0, num2 = 0;

            //For sqrt, only one input is need
            if (Regex.IsMatch(op, "^[5|7|8|9]$"))
            {
                num1 = input.Tinput1();
                double result = ExecuteCalculation(num1, 0, op);
                return result;
            }

            // for other operation

            num1 = input.Tinput1();
            num2 = input.Tinput2();

            return ExecuteCalculation(num1, num2, op);
        }
    }
}
