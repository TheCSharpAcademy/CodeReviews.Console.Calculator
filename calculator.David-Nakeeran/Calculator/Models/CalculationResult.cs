namespace CalculatorProgram
{
    class CalculationResult
    {
        internal double NumOne { get; set; }
        internal double NumTwo { get; set; }
        internal double Result { get; set; }
        internal string? OperationSymbol { get; set; }
        internal OperationType Operation { get; set; }
    }

    internal enum OperationType
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }
}