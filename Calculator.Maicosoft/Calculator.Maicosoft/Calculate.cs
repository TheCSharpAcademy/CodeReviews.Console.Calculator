using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Maicosoft
{
    internal class Calculate
    {
        internal static double Addition(double num1, double num2)
        {
            return num1 + num2;
        }

        internal static double Division(double num1, double num2)
        {
            while (num2 == 0)
            {
                Console.WriteLine("You cannot divide by 0, please enter a valid number: ");
                num2 = Convert.ToDouble(Console.ReadLine());
            }
            return num1 / num2;
        }

        internal static double Multiply(double num1, double num2)
        {
            return num1 * num2;
        }

        internal static double Subtraction(double num1, double num2)
        {
            return num1 - num2;
        }
    }
}
