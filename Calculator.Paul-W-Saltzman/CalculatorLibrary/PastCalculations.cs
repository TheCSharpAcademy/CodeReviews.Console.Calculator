
namespace CalculatorLibrary
{
    public class PastCalculations
    {
        public Double Number1 { get; set; }
        public Double Number2 { get; set; }
        public String Operation { get; set; }
        public Double Result { get; set; }

        

        public PastCalculations(double number1, double number2, string operation, double result)
        {
            Number1 = number1;
            Number2 = number2;
            Operation = operation;
            Result = result;
        }

        public PastCalculations(double number1, string operation, double result)
        {
            Number1 = number1;
            Operation = operation;
            Result = result;
        }

        
    }
}
