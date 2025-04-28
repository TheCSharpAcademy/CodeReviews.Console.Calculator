
namespace CalculatorLibrary
{
    public class Calculation
    {
        public double FirstOperand { get; set; }
        public double SecondOperand { get; set; }
        public string Operation { get; set; }
        public double Result { get; set; }

        public override string ToString()
        {
            if ("+-/*^".Contains(Operation))
                return $"{FirstOperand} {Operation} {SecondOperand} = {Result}";
            else
                return $"{Operation} {FirstOperand} = {Result}";
        }
    }
}
