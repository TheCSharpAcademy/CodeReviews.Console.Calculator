namespace CalculatorLibrary;

public class Calculation
{
    public double NumberOne { get; set; }
    public double NumberTwo { get; set; }
    public double Answer { get; set; }
    public string? Operator { get; set; }
    public Calculation(double numberOne, double numberTwo, double answer, string op)
    {
        NumberOne = numberOne;
        NumberTwo = numberTwo;
        Answer = answer;
        Operator = op;
    }
    public Calculation(double numberOne, double answer, string op)
    {
        NumberOne = numberOne;
        Answer = answer;
        Operator = op;
    }
}
