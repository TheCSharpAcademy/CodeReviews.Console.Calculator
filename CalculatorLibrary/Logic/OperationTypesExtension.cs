namespace CalculatorLibrary.Logic;

public static class OperationTypesExtension
{
    public static bool RequiresTwoOperands(this OperationType operationType)
    {
        if (operationType == OperationType.Addition || operationType == OperationType.Subtraction
                                                    || operationType == OperationType.Multiplication ||
                                                    operationType == OperationType.Division
                                                    || operationType == OperationType.Power)
            return true;

        return false;
    }
}