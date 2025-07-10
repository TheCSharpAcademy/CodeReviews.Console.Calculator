using System.Linq;

namespace CalculatorLibrary.TheNigerianNerd
{
    public enum OperationType
    {
        None,
        Addition,
        Subtraction,
        Multiplication,
        Divison
    }
    internal class Operation
    {
        // Properties to hold the operands and the operation type.
        public double Operand1 { get; set; }
        public double Operand2 { get; set; }
        public OperationType OperationType { get; set; }
        public double Result { get; set; }

        // Constructor to initialize the properties.
        public Operation(double num1, double num2, OperationType operation, double result)
        {
            Operand1 = num1;
            Operand2 = num2;
            OperationType = operation;
            Result = result;
        }
    }
}
