namespace CalculatorLibrary;

public class Calculation
{
    public double Num1 { get; set; }
    public double Num2 { get; set; }
    public string Op { get; set; }
    public double Result { get; set; } 
      
    public Calculation(double num1, double num2, string op, double result)
    {
        Num1 = num1;
        Num2 = num2;
        Op = op;
        Result = result;
    }
}