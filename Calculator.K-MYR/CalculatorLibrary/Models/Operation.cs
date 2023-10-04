namespace CalculatorProgram.K_MYR.Models;

public class Operation
{
    public double Operand1 { get; set; }

    public double Operand2 { get; set; }

    public OperationType Type { get; set; }

    public double Result { get; set; }
}

public enum OperationType
{
    Addition = '+',
    Subtraction = '-',
    Multiplication = '*',
    Division = '/',
    Power = '^'
}