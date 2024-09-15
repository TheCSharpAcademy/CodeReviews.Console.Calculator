namespace CalculatorLibrary;

public class OperationModel
{
    public double Operand1 { get; set; }
    public double Operand2 { get; set; }
    public string Operation { get; set; } = default!;
    public double Result { get; set; }
}