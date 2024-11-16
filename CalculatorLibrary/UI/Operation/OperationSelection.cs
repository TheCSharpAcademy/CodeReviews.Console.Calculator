namespace CalculatorLibrary.UI.Operation;

public class OperationSelection
{
    public override string ToString()
    {
        return $@"{Convert.ToChar(OperationChoice.Addition)}: Addition
{Convert.ToChar(OperationChoice.Subtraction)}: Subtraction
{Convert.ToChar(OperationChoice.Multiplication)}: Multiplication
{Convert.ToChar(OperationChoice.Division)}: Division
{Convert.ToChar(OperationChoice.SquareRoot)}: Square root
{Convert.ToChar(OperationChoice.Power)}: Power
{Convert.ToChar(OperationChoice.X10)}: x10
{Convert.ToChar(OperationChoice.Sine)} Sine
{Convert.ToChar(OperationChoice.Cosine)}: Cosine
{Convert.ToChar(OperationChoice.Tangent)}: Tangent
{Convert.ToChar(OperationChoice.Cotangent)}: Cotangent
";
    }
}