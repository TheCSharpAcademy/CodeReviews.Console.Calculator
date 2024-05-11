namespace CalculatorLibrary;

public class Symbol
{
    internal string OperationToSymbol(string op)
    {
        return op switch
        {
            "a" => "+",
            "s" => "-",
            "d" => "/",
            "m" => "*",
            "p" => "^",
            "sr" => "root",
            _ => op
        };
    }
}