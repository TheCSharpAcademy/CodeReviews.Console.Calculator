namespace CalculatorLibrary
{
    public class CalculatorUseStatistics
    {
        public List<Operation> Operations { get; set; } = null!; //never clear
        public long CountOfCalculations { get; set; }
        public List<Operation> LatestCalculations { get; set; } = null!; // user can clear that list
    }

    public class Operation
    {
        public double OperandLeft { get; set; }
        public double OperandRight { get; set; }
        public string Operator { get; set; } = null!;
        public double Result { get; set; }
    }
}
