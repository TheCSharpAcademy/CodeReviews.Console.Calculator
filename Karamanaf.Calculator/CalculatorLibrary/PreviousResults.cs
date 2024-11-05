namespace CalculatorLibrary
{
    public class PreviousResults
    {
        public int Id { get; private set; }
        private int _count = 1;
        public double Operand1 { get; set; }
        public double Operand2 { get; set; }
        public string Operation { get; set; }
        public OperationType OpType { get; set; }
        public double Result { get; set; }
        public PreviousResults() { }
        public PreviousResults(double operand1, double operand2, string operation, OperationType opType, double result)
        {
            Id = _count++;
            Operand1 = operand1;
            Operand2 = operand2;
            Operation = operation;
            OpType = opType;
            Result = result;
        }
    }
    public enum OperationType
    {
        Single,
        Double
    }
}
