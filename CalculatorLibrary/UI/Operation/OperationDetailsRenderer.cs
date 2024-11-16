using CalculatorLibrary.Logic;

namespace CalculatorLibrary.UI.Operation;

public static class OperationDetailsRenderer
{
    public static void Render(OperationDetails operationDetails)
    {
        if (operationDetails.OperationType.RequiresTwoOperands())
        {
            if (double.IsNaN(operationDetails.Result))
                Console.WriteLine("{0:0.##} {1} {2:0.##} - This operation will result in an error");
            else
                Console.WriteLine("Your result: {0:0.##} {1} {2:0.##} = {3:0.##}", operationDetails.LeftOperand,
                    OperationTypeToPresentationMapper.Map(operationDetails.OperationType),
                    operationDetails.RightOperand, operationDetails.Result);
        }
        else
        {
            if (double.IsNaN(operationDetails.Result))
                Console.WriteLine("{1} {0:0.##} - This operation will result in an error");
            else
                Console.WriteLine("Your result: {1} {0:0.##} = {2:0.##}", operationDetails.LeftOperand,
                    OperationTypeToPresentationMapper.Map(operationDetails.OperationType), operationDetails.Result);
        }
    }
}