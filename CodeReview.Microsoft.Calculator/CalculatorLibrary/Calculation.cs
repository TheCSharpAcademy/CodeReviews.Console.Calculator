namespace CalculatorLibrary
{
    public class Calculation
    {
        public double Operand1 { get; set; }
        public double Operand2 { get; set; }
        public string Operation { get; set; } = "empty";
        public double Result { get; set; }
        public int Counter { get; set; }    

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




