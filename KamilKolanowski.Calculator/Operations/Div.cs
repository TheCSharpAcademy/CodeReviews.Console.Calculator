namespace KamilKolanowski.Calculator;

public class Div : ICalculator
{
    public decimal Operation(decimal num1, decimal num2)
    {
        while (num2 == 0)
        {
            Console.WriteLine("Please, enter a non-zero divisor.");
            Console.Write("Enter a divisor: ");
            num2 = decimal.Parse(Console.ReadLine());
        }
        
        return num1 / num2;
    }
}