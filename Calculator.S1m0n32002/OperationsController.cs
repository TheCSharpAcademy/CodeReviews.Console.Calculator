namespace Calculator.S1m0n32002
{
    public static class CalculatorController
    {
        public enum Operations
        {
            Sum,
            Subtract,
            Multiply,
            Divide,
            SquareRoot,
            Power,
            TenPower,
            Sin,
            Cos,
            Tan,
            Log,
            Exp,
        }

        public readonly static Dictionary<string, Operations> strOperations = new()
        {
            { "Sum"                 ,Operations.Sum},
            { "Subtract"            ,Operations.Subtract},
            { "Multiply"            ,Operations.Multiply},
            { "Divide"              ,Operations.Divide},
            { "Square Root"         ,Operations.SquareRoot},
            { "X to the power of Y" ,Operations.Power},
            { "10^x"                ,Operations.TenPower},
            { "Sine"                ,Operations.Sin},
            { "Cosine"              ,Operations.Cos},
            { "Tangent"             ,Operations.Tan},
            { "Logaritmic"          ,Operations.Log},
            { "Exponential"         ,Operations.Exp},
        };

        public static double DoOperation(Operations operation, params double[] numbers)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.

            switch (operation)
            {
                case Operations.Sum:
                    result = numbers[0] + numbers[1];
                    break;

                case Operations.Subtract:
                    result = numbers[0] - numbers[1];
                    break;

                case Operations.Multiply:
                    result = numbers[0] * numbers[1];
                    break;

                case Operations.Divide:
                    // Should already be not zero but just in case
                    if (numbers[1] != 0)
                    {
                        result = numbers[0] / numbers[1];
                    }
                    break;

                case Operations.SquareRoot:
                    result = Math.Sqrt(numbers[0]);
                    break;

                case Operations.Power:
                    result = Math.Pow(numbers[0], numbers[1]);
                    break;

                case Operations.TenPower:
                    result = Math.Pow(10, numbers[0]);
                    break;

                case Operations.Sin:
                    result = Math.Sin(numbers[0]);
                    break;

                case Operations.Cos:
                    result = Math.Cos(numbers[0]);
                    break;

                case Operations.Tan:
                    result = Math.Tan(numbers[0]);
                    break;

                case Operations.Log:
                    result = Math.Log(numbers[0]);
                    break;

                case Operations.Exp:
                    result = Math.Exp(numbers[0]);
                    break;
            }

            return result;
        }
    }
}
