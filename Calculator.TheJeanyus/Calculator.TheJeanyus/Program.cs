using CalculatorLibrary;

bool endApp = false;
Calculator calculator = new();

while (!endApp)
{
    double result;

    Console.Clear();
    Console.Write(CalculatorMenu.Title);
            
    //Print last n operations, 5 by default
    calculator.PrintHistory();
                        
    Console.Write(CalculatorMenu.OperatorList);
    CalculatorOperations.OperationData opData = Calculator.GetOperation(Console.ReadLine());
            
    if (calculator.NumberOfCalcs > 0) Console.Write("You can type 'ans#' where # is an item in the history to use the result.\n");
    Console.Write(opData.ExtraInfo);
            
    // Ask the user for the quanitity of numbers needed for the operation
    double[] cleanNumbers = new double[opData.ParamCount];
    for (int i = 0; i < opData.ParamCount; i++)
    {
        Console.Write("Type a number, and then press Enter: ");
        cleanNumbers[i] = Calculator.GetOperand(Console.ReadLine(), opData.Op, i);
    }
                        
    try
    {
        result = calculator.DoOperation(cleanNumbers, opData);
        if (double.IsNaN(result)) Console.Write("This operation will result in a mathematical error.\n\n");
        else Console.Write("Your result: {0:0.##}\n\n", result);
    }
    catch (Exception e)
    {
        Console.Write($"Oh no! An exception occurred trying to do the math.\n - Details: {e.Message}\n");
    }

    Console.Write(CalculatorMenu.EndMessage);
    if (Console.ReadLine() == "n") endApp = true;
}

calculator.Finish();