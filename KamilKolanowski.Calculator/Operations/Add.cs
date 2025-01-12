namespace KamilKolanowski.Calculator;

public class Add : ICalculator
{
    public decimal Operation(decimal num1, decimal num2) => num1 + num2;
}