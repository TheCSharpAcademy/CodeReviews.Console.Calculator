

namespace CalculatorLibrary
{
    public class History
    {
        public double Operand1 { get; set; }
        public double? Operand2 { get; set; }
        public  OperationType Type { get; set; }
        public double Result { get; set; }
    }

    public enum OperationType
    {
        Broken,
        Add,
        Subtract,
        Multiply,
        Divide,
        SquareRoot,
        PowerOf,
        X10,
        Sin,
        Cos,
        Tan
    }
}
