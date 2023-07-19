namespace Calculator_lib.Model;

internal enum TypeOfCalculation
{
    Addition,
    Subraction,
    Multiplication,
    Divison,
    TimesTen,
    TakingToPower,
    SquareRoot,
    Sinus,
    Cosinus,
    Tangens,
    Cotangens
}

internal class Calculator
{
    internal static int NumberOfTimesUsed { get; set; }
    internal DateTime DateTime { get; set; }
    internal TypeOfCalculation TypeOfCalculation { get; set; }
    internal double Result { get; set; }
}
