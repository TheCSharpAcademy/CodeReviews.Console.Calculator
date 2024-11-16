namespace CalculatorLibrary.UI.OperandSource;

public class OperandSourceSelection
{
    public override string ToString()
    {
        return $@"{Convert.ToChar(OperandSources.Console)}: Console
{Convert.ToChar(OperandSources.History)}: History
{Convert.ToChar(OperandSources.Speech)}: Speech
";
    }
}