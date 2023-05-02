namespace CalculatorLibrary;
public class Calculation
{
    public double Operand1 { get; set; }
    public double Operand2 { get; set; }
    public char Operator { get; set; }
    public double Result
    {
        get
        {
            return Operator switch
            {
                '-' => Operand1 - Operand2,
                '*' => Operand1 * Operand2,
                '/' => Operand1 / Operand2,
                '^' => Math.Pow(Operand1, Operand2),
                'r' => Math.Pow(Operand1, 1 / Operand2),
                _ => Operand1 + Operand2,
            };
        }
    }

    public override string ToString()
    {
        return $"{Operand1} {Operator} {Operand2} = {Result}";
    }
}
