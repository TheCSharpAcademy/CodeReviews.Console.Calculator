namespace Calculator.S1m0n32002
{
    internal static class CalculatorController
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

        public static readonly Dictionary<string, Operations> strOperations = new()
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

        public static List<OperationResult> ResultLog { get; } = [];

        public static OperationResult? DoOperation(Operations operation, params double[] numbers)
        {
            double value = double.NaN; // Default value is "not-a-number" if an operation, such as division, could value in an error.

            switch (operation)
            {
                case Operations.Sum:
                    value = numbers[0] + numbers[1];
                    break;

                case Operations.Subtract:
                    value = numbers[0] - numbers[1];
                    break;

                case Operations.Multiply:
                    value = numbers[0] * numbers[1];
                    break;

                case Operations.Divide:
                    // Should already be not zero but just in case
                    if (numbers[1] != 0)
                    {
                        value = numbers[0] / numbers[1];
                    }
                    else
                        return null;
                    break;

                case Operations.SquareRoot:
                    value = Math.Sqrt(numbers[0]);
                    break;

                case Operations.TenPower:
                case Operations.Power:
                    value = Math.Pow(numbers[0], numbers[1]);
                    break;
                case Operations.Sin:
                    value = Math.Sin(numbers[0]);
                    break;

                case Operations.Cos:
                    value = Math.Cos(numbers[0]);
                    break;

                case Operations.Tan:
                    value = Math.Tan(numbers[0]);
                    break;

                case Operations.Log:
                    value = Math.Log(numbers[0]);
                    break;

                case Operations.Exp:
                    value = Math.Exp(numbers[0]);
                    break;
            }

            var result = new OperationResult()
            {
                Value = value,
                Numbers = numbers,
                Operation = operation,
            };

            ResultLog.Add(result);

            return result;
        }
    }
}
