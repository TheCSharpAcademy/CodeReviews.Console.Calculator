namespace CalculatorLibrary
{
    public class Calculation
    {
        public double Operand1 { get; set; } = 0;
        public double Operand2 { get; set; } = 0;
        public string Operation { get; set; } = "empty";
        public double Result { get; set; } = 0;
        public int Counter { get; set; } = 0;        

    }
    public class CalculationsList
    {
        public List<Calculation> Operations { get; set; } = new List<Calculation>();

        public void AddOperation(Calculation operation)
        {
            Operations.Add(operation);
        }       
    }
}




