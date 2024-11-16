namespace CalculatorLibrary.UI.OperandSource;

public static class OperandSourceSelectionRenderer
{
    public static void Render(OperandSourceSelection operandSourceSelection)
    {
        Console.WriteLine("How do you want to provide the operand?");
        Console.Write(operandSourceSelection.ToString());
    }
}