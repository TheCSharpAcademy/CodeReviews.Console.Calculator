namespace Calculator_kilozdazolik;

public class CalculatorEngine
{
    public double AddOperand(double operand1, double operand2)
    {
        return operand1 + operand2;
    }
    
    public double SubtractOperand(double operand1, double operand2)
    {
        return operand1 - operand2;
    }
    
    public double MultiplyOperand(double operand1, double operand2)
    {
        return operand1 * operand2;
    }

    public double DivideOperand(double operand1, double operand2)
    {
        if (operand2 == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero");
        }
        return operand1 / operand2;
    }

    public double SquareRootOperand(double operand)
    {
        return Math.Sqrt(operand);
    }

    public double SinOperand(double operand)
    {
        return Math.Sin(operand); 
    }

    public double CosOperand(double operand)
    {
        return Math.Cos(operand); 
    }

    public double TanOperand(double operand)
    {
        return Math.Tan(operand); 
    }

    public double PowerOfTen(double operand)
    {
        return Math.Pow(10, operand); 
    }
}