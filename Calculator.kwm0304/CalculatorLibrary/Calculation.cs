namespace CalculatorLibrary;

public class Calculation(double numberOne, double numberTwo, double answer, string opertor)
{
    public double NumberOne { get; set; } = numberOne;
    public double NumberTwo { get; set; } = numberTwo;
    public double Answer { get; set; } = answer;
    public string Operator { get; set; } = opertor;
}
