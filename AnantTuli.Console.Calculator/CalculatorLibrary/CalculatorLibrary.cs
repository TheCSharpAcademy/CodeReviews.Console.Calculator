namespace CalculatorLibrary
{
    public class Calculator
    {
        public CalculationData calculationData;

        public Calculator(CalculationData calculationData)
        {
            this.calculationData = calculationData;
        }

        public double DoUnaryOperation(double num, string op)
        {
            double result = double.NaN;
            string prettyMathOperator = op;

            switch (op)
            {
                case "r":
                    result = Math.Sqrt(num);
                    prettyMathOperator = "Sqrt";
                    break;
                case "x":
                    result = num * 10;
                    prettyMathOperator = "10x";
                    break;
                case "n":
                    result = Math.Sin(num);
                    prettyMathOperator = "Sin(x)";
                    break;
                case "c":
                    result = Math.Cos(num);
                    prettyMathOperator = "Cos(x)";
                    break;
                default:
                    break;
            }

            this.calculationData.AddCalculation(new Calculation(op, [num], result, prettyMathOperator));
            return result;
        }

        public double DoBinaryOperation(double num1, double num2, string op)
        {
            double result = double.NaN;
            string prettyMathOperator = op;

            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    prettyMathOperator = "+";
                    break;
                case "s":
                    result = num1 - num2;
                    prettyMathOperator = "-";
                    break;
                case "m":
                    result = num1 * num2;
                    prettyMathOperator = "*";
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        prettyMathOperator = "/";
                    }
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    prettyMathOperator = "^";
                    break;
                default:
                    break;
            }

            this.calculationData.AddCalculation(new Calculation(op, [num1, num2], result, prettyMathOperator));
            return result;
        }

        public void Finish()
        {
            this.calculationData.WriteToFile(this.calculationData);
        }
    }
}