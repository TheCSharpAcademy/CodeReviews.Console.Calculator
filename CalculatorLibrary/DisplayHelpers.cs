using Spectre.Console;

internal static class DisplayHelpers
{
    public static void ShowAdvancedCalculatorInfo()
    {
        var table = new Table();
        table.AddColumn("[yellow]Mathematical Functions[/]");
        table.AddColumn("[blue]Arithmetic Operations[/]");
        table.AddColumn("[green]Trigonometric Functions[/]");
        table.AddRow("Absolute value: Abs(a)\nExponential: Exp(a)\nNatural logarithm: Log(a)\nBase 10 logarithm: Log10(a)\nCeiling: Ceil(a)\nFloor: Floor(a)\nMaximum: Max(a,b)\nMinimum: Min(a,b)\nSquare root: Sqrt(a)\nPower: Pow(a,b)\nRound to nearest integer or specified number\n of decimal places: Round(a,b)"
        , "Addition: (a+b)\nSubtraction: (a-b)\nMultiplication: (a*b)\nDivision: (a/b)\nModulus (remainder): (a%b)\nExponentiation: (a^b)"
        ,"Sine: Sin(a)\nCosine: Cos(a)\nTangent: Tan(a)\nArcsine: Asin(a)\nArccosine: Acos(a)\nArctangent: Atan(a)");
        AnsiConsole.Write(table);

        var help = new Table();
        help.AddColumn("[yellow]To use calculator just type expression, like:\n[/]" +
            "5632565*858+Sin(7564)/44532+(6354+Cos(546))/46");
        AnsiConsole.Write(help);
    }
}
