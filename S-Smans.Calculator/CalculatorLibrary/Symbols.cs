namespace CalculatorLibrary;

internal class Symbols
{
    internal string OperatorToSymbol(string op)
    {
        switch (op)
        {
            case "a":
                return "+";
            case "s":
                return "-";
            case "m":
                return "*";
            case "d":
                return "/";
            default:
                return op;
        }
    }
}