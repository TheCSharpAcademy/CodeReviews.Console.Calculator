
namespace CalculatorLibrary;

public class CalculationsRecord
{
    public class OperationRecord //class that holds the list structure
    {
        public int Id { get; set; }
        public double NumA1 { get; set; }
        public string Operation { get; set; }
        public double NumA2 { get; set; }
        public double ResultA { get; set; }
    }
}
