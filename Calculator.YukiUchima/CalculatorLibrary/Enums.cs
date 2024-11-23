
namespace CalculatorLibrary;

public class Enums
{
    public static Dictionary<OperationType, string> OpDescription = new Dictionary<OperationType, string> {
            { Enums.OperationType.Add, "Addition" },
            { Enums.OperationType.Subtract, "Subtract" },
            { Enums.OperationType.Multiply, "Multiply" },
            { Enums.OperationType.Divide, "Divide" },
            { Enums.OperationType.ShowCalculations, "Show Previous Calculations" },
            { Enums.OperationType.DeleteCalculations, "Delete Previous Calculations Memory" },
            { Enums.OperationType.SquareRoot, "Square Root" },
            { Enums.OperationType.Power, "Power" },
            { Enums.OperationType.TenX, "Mutipliter (10x)" },
        };
    public enum OperationType
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        SquareRoot,
        Power,
        TenX,
        Cosine,
        Sine,
        ShowCalculations,
        DeleteCalculations,
    }
}
