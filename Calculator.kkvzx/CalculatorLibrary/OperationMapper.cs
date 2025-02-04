namespace CalculatorLibrary;

public static class OperationMapper
{
    public static Operation StringToOperation(string option)
    {
        switch (option)
        {
            case "a":
                return Operation.Addition;
            case "s":
                return Operation.Subtraction;
            case "m":
                return Operation.Multiplication;
            case "d":
                return Operation.Division;
            default:
                return Operation.Addition;
        }
    }
}