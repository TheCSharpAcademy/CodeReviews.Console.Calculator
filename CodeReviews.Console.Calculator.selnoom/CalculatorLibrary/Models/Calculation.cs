namespace CalculatorLibrary.Models;

public class Calculation
{
    private static int _nextId;
    public int Id { get; }
    public string Operation { get; set; }
    public string Num1 { get; set; }
    public string Num2 { get; set; }
    public string Result { get; set; }
    public Calculation()
    {
        _nextId++;
        Id = _nextId;
    }
}