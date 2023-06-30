using System;

namespace Calculator.Mo3ses
{
    public static class MathOperations
    {
        public static void Sum(double value1, double value2)
        {
           double awnser = value1 + value2;
            Console.WriteLine($"The Sum of {value1} and {value2} is {awnser}");
        }
        public static void Subtract(double value1, double value2)
        {
           double awnser = value1 - value2;
            Console.WriteLine($"The Subtraction of {value1} and {value2} is {awnser}");
        }
        public static void Multiply(double value1, double value2)
        {
           double awnser = value1* value2;
            Console.WriteLine($"The Multiplication of {value1} and {value2} is {awnser}");
        }
        public static void Divide(double value1, double value2)
        {
           double awnser = value1 / value2;
            Console.WriteLine($"The Division of {value1} and {value2} is {awnser}");
        }

        
    }
}
