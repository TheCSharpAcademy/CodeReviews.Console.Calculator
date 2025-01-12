namespace KamilKolanowski.Calculator;

public class Div : ICalculator
{
    public decimal Operation(decimal num1, decimal num2)
    {
        if (num2 == 0)
        {
            return 0;
        }

        return num1 / num2;
    }
}