namespace CalculatorLibrary
{
    public class CalculationModel
    {
        public double FirstOperand { get; set; }
        public double SecondOperand { get; set; }
        public enum CalculationType
        {
            Add,
            Subtract, 
            Multiply,
            Divide,
            FailDivide
        }
        public CalculationType Type { get; set; }
        public double Answer { get; set; }
        public CalculationModel(double firstOperand, double secondOperand, CalculationType type, double answer)
        { 
            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
            Type = type;
            Answer = answer;
        }
    }
}