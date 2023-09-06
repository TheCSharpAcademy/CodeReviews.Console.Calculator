namespace CalculatorLibrary;

public abstract class Operation
{
    public string Shortcut { get; }
    public string Name { get; }
    public int ParamCount { get; }

    public Operation(string shortcut, string name, int paramCount)
    {
        Shortcut = shortcut;
        Name = name;
        ParamCount = paramCount;
    }

    public abstract double GetResult(double[] numbers);

    public abstract string Format(double[] numbers);
    
}