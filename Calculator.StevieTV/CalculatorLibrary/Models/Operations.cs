namespace CalculatorLibrary.Models
{
    internal class Operation
    {
        public int Sequence { get; set; }
        public double Operand1 { get; set; }
        public double Operand2 { get; set; }
        public double Result { get; set; }
        public string OperationType { get; set; }
    }
}
