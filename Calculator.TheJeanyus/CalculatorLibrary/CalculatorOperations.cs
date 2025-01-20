namespace CalculatorLibrary;

public static class CalculatorOperations
{
    static readonly string TrigInfo = "Provide numbers in radians. You can type '#pi' to mean #*Pi.\n";

    public enum Operation                                                                          //List of operations to support
    {
        Addition, Subtraction, Multiplication, Division, SquareRoot, Power, TenToPower, Sine, Cosine, Tangent
    }
    public struct OperationData                                                                    //Data structure to hold required operands and delegates for results of and operation
    {
        public Operation Op { get; set; }
        public int ParamCount { get; set; }
        public string ExtraInfo { get; set; }
        public delegate double OpCaclulation(double[] operands);
        public delegate string OpRecord(double[] operands);
        public OpCaclulation Calc { get; set; }
        public OpRecord Record { get; set; }

        public OperationData(Operation op, int paramCount, OpCaclulation calc, OpRecord record, string extraInfo = "")
        {
            Op = op;
            ParamCount = paramCount;
            Calc = calc;
            Record = record;
            ExtraInfo = extraInfo;
        }
    }

    public static readonly Dictionary<string, OperationData> OpList = new() //Link user input to the data for operations in the enum
    {
        { "a",      new OperationData(Operation.Addition, 2, (double[] operands) => operands[0] + operands[1], (double[] operands) => $"{operands[0]} + {operands[1]} = ") },
        { "s",      new OperationData(Operation.Subtraction, 2, (double[] operands) => operands[0] - operands[1], (double[] operands) => $"{operands[0]} - {operands[1]} = ") },
        { "m",      new OperationData(Operation.Multiplication, 2, (double[] operands) => operands[0] * operands[1], (double[] operands) => $"{operands[0]} * {operands[1]} = ") },
        { "d",      new OperationData(Operation.Division, 2, (double[] operands) => operands[0] / operands[1], (double[] operands) => $"{operands[0]} / {operands[1]} = ") },
        { "r",      new OperationData(Operation.SquareRoot, 1, (double[] operands) => Math.Sqrt(operands[0]), (double[] operands) => $"Sqrt({operands[0]}) = ") },
        { "p",      new OperationData(Operation.Power, 2, (double[] operands) => Math.Pow(operands[0],operands[1]), (double[] operands) => $"{operands[0]} ^ {operands[1]} = ") },
        { "t",      new OperationData(Operation.TenToPower, 1, (double[] operands) => Math.Pow(10,operands[0]), (double[] operands) => $"10 ^ {operands[0]} = ") },
        { "sin",    new OperationData(Operation.Sine, 1, (double[] operands) => Math.Sin(operands[0]), (double[] operands) => $"Sin({operands[0]}) = ", TrigInfo) },
        { "cos",    new OperationData(Operation.Cosine, 1, (double[] operands) => Math.Cos(operands[0]), (double[] operands) => $"Cos({operands[0]}) = ", TrigInfo) },
        { "tan",    new OperationData(Operation.Tangent, 1, (double[] operands) => Math.Tan(operands[0]), (double[] operands) => $"Tan({operands[0]}) = ", TrigInfo) }
    };
}