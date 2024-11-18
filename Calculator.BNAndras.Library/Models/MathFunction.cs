namespace Calculator.BNAndras.CalculatorLibrary.Models;

record MathFunction
{
    private readonly Func<double, double>? _single;
    private readonly Func<double, double, double>? _double;

    public MathFunction(Func<double, double> f) => _single = f;
    public MathFunction(Func<double, double, double> f) => _double = f;

    public double Call(double x) => _single!(x);
    public double Call(double x, double y) => _double!(x, y);

    public bool TakesTwoOperands() => _double is not null;
}
