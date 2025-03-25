using System.Linq;

namespace Calculator.Models
{
    internal class Operation
    {
        internal double Number1 { get; set; }
        internal double Number2 { get; set; }
        internal string Operator { get; set; }
        internal double Result { get; set; }
        internal string? TrigFunction { get; set; }

        internal Operation(double num1, double num2, string op, double res, string? trigFunction)
        {
            Number1 = num1;
            Number2 = num2;
            Operator = op;
            Result = res;
            TrigFunction = trigFunction;
        }

        public void DisplayOperation(int i)
        {
            string formatted_operator = Operator switch
            {
                "a" => "+",
                "s" => "-",
                "m" => "*",
                "d" => "/",
                "p" => "^",
                "q" => "sqrt",
                "x" => "10x",
                "t" => TrigFunction
            };

            if(formatted_operator == "q")
            {
                Console.WriteLine($"{i}) sqrt({Number1}) = {Result}");
            }else if (formatted_operator == "x")
            {
                Console.WriteLine($"{i}) {Number1} x 10 = {Result}");
            }
            else if (Operator == "t" && TrigFunction != null)
            {
                Console.WriteLine($"{i}) {TrigFunction}({Number1}) = {Result}");
            }
            else
            {
                Console.WriteLine($"{i}) {Number1} {formatted_operator} {Number2} = {Result}");
            }
        }
    }
}
