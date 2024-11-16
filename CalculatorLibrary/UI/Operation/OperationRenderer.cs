using CalculatorLibrary.Logic;

namespace CalculatorLibrary.UI.Operation;

public static class OperationRenderer
{
    public static void Render(OperationDetails operationDetails)
    {
        if (operationDetails.OperationType.RequiresTwoOperands())
            Console.Write(
                $"{operationDetails.LeftOperand} {OperationTypeToPresentationMapper.Map(operationDetails.OperationType)} {operationDetails.RightOperand} = {operationDetails.Result}");
        else
            Console.Write(
                $"{OperationTypeToPresentationMapper.Map(operationDetails.OperationType)} {operationDetails.LeftOperand} = {operationDetails.Result}");
    }
}