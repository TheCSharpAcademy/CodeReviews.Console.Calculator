using Newtonsoft.Json;

namespace CalculatorLibrary;

public class OperationRecord
{
    public double Operand1;
    public double Operand2;
    public string Operation;
    public double Result;

    public OperationRecord(double num, double result, string operation)
    {
        Operand1 = num;
        Result = result;
        Operation = operation;
    }

    [JsonConstructor]
    public OperationRecord(double num1, double num2, double result, string operation) : this(num1, result, operation)
    {
        Operand2 = num2;
    }

    public override string ToString()
    {
        switch (Operation)
        {
            case "Exponent":
                return $"{Operand1} to the power of {Operand2} = {Result}";
            case "Square Root":
            case "Sine":
            case "Cosine":
            case "Tangent":
                return $"{Operation} of {Operand1} = {Result}";
            default:
                return $"{Operand1} {GetOperationSymbol()} {Operand2} = {Result}";
        }
    }

    private string GetOperationSymbol()
    {
        switch(Operation)
        {
            case "Add":
                return "+";
            case "Subtract":
                return "-";
            case "Multiply":
                return "*";
            case "Divide":
                return "/";
            case "Square Root":
                return "√";
            default:
                return "";
        }
    }
}

