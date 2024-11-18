using System.Diagnostics;
using Calculator.BNAndras.CalculatorLibrary.Models;

namespace Calculator.BNAndras.CalculatorLibrary;

public static class CalculatorController
{
    
    private static Dictionary<MathOperation,MathFunction> AllowedOperations { get; } = new()
    {
        {MathOperation.Add, new( (operand1, operand2) =>  operand1 + operand2 )},
        {MathOperation.Subtract, new ((operand1, operand2) => operand1 - operand2)},
        {MathOperation.Multiply, new ((operand1, operand2) => operand1 * operand2)},
        {MathOperation.Divide, new ((operand1, operand2) => operand1 != 0 ? operand1 / operand2 : double.NaN)},
        {MathOperation.LogX, new ((operand1, operand2) => Math.Log(operand1, operand2))},
        {MathOperation.Power, new (Math.Pow)},
        {MathOperation.Cosine, new (Math.Cos)},
        {MathOperation.Cotangent, new((operand) => 1 / Math.Tan(operand))},
        {MathOperation.Sine, new ( Math.Sin)},
        {MathOperation.SquareRoot, new(Math.Sqrt)},
        {MathOperation.Tangent, new(Math.Tan)},
    };

    private static List<MathOperation> TwoOperands { get; } =
        AllowedOperations.Where(kvp => kvp.Value.TakesTwoOperands())
                         .Select(kvp => kvp.Key)
                         .ToList();

    public static bool HasTwoOperands(MathOperation operation) => TwoOperands.Contains(operation);

    public static CalculationResult Do(MathOperation operation, List<double> operands)
    {
        MathFunction f = AllowedOperations[operation];

        double result = HasTwoOperands(operation) switch
        {
            true => f.Call(operands[0], operands[1]),
            false => f.Call(operands[0])
        };

        return new(operation, operands, result);
    }
}