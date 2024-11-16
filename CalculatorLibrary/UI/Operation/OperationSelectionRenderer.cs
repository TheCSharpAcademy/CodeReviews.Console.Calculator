namespace CalculatorLibrary.UI.Operation;

public static class OperationSelectionRenderer
{
    public static void Render(OperationSelection operationSelection)
    {
        Console.WriteLine("What operation do you want to perform?");
        Console.Write(operationSelection.ToString());
    }
}