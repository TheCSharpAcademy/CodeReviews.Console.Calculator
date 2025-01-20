namespace CalculatorLibrary;

public static class CalculatorMenu
{
    public static string Title { get; } = 
@"Console Calculator in C#
------------------------

";
    
    public static string OperatorList { get; } =
@"Choose an operator from the following list:
    a - Add
    s - Subtract
    m - Multiply
    d - Divide
    r - Square Root
    p - Power (x^y)
    t - 10^x
    sin - Sine
    cos - Cosine
    tan - Tangent
Your option?";
    
    public static string EndMessage { get; } =
@"------------------------

Press 'n' and Enter to close the app, or press any other key and Enter to continue: ";
}