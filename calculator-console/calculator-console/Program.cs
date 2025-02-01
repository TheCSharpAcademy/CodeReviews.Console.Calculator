using CalculatorLibrary;
using CalculatorHistory;

double result;
List<double> operands = [];
int operation;
int typeOfOperation;
List<PossibleOperations> operationTypeMenuChoices = new List<PossibleOperations>((PossibleOperations[]) Enum.GetValues(typeof(PossibleOperations)));
List<PossibleArithmeticOperations> arithmeticOperationMenuChoices = new List<PossibleArithmeticOperations>((PossibleArithmeticOperations[])Enum.GetValues(typeof(PossibleArithmeticOperations)));
List<PossibleTrigonometricOperations> trigonometricOperationMenuChoices = new List<PossibleTrigonometricOperations>((PossibleTrigonometricOperations[])Enum.GetValues(typeof(PossibleTrigonometricOperations)));

Calculator calculator = new Calculator();
int useCount = 0;

Console.Write("Press Any Key to start your calculations, or press ESC to exit\t");
ConsoleKeyInfo startKey = Console.ReadKey();
if (startKey.Key == ConsoleKey.Escape)
{
    Environment.Exit(0);
}
Console.Clear();

while(true)
{
    typeOfOperation = NumberSelectionMenu(menuTitle: "Select the type of operation you want to perform:", menuChoices: operationTypeMenuChoices);
    operation = OperatorSelectionMenu(menuTitle: "Select the operator you want to use:", typeOfOperation: typeOfOperation);

    int numberOfOperands = GiveNumberOfOperands(operation, typeOfOperation);

    for (int i = 0; i < numberOfOperands; i++)
    {
        Console.WriteLine($"Enter operand {i+1}: " + (History.GetPastResultCount() == 0 ? "" : "(Press UP arrow key to use past results)"));
        operands.Add(ReadInputAndOnlyReturnDouble());
    }
    
    
    
    result = calculator.DoOperation(operands, operation, typeOfOperation);
    
    History.SaveHistory(operands, typeOfOperation, operation, result, DateTime.Now);
    Console.WriteLine($"\n\nThe Result of your operation is: {Math.Round(result, 4)}");
    operands.Clear();
    
    useCount++;
    RecurringMainChoices();  
}


/* --------------------------------------------------------------------------------------------------------------------------
 *  FUNCTIONS START BELOW HERE
 *  -------------------------------------------------------------------------------------------------------------------------*/

int GiveNumberOfOperands(int operation, int typeOfOperation)
{
    if (typeOfOperation == (int)PossibleOperations.Trigonometric)
    {
        return 1;
    }
    else if (typeOfOperation == (int)PossibleOperations.Arithmetic && (operation == (int)PossibleArithmeticOperations.SquareRoot))
    {
        return 1;
    }
    return 2;
}
double ReturnDoubleFromInput(string? operandInput)
{
    
    bool successfullyParsed = double.TryParse(operandInput, out double parseOutput);
    while (!successfullyParsed)
    {
        successfullyParsed = double.TryParse(Console.ReadLine(), out parseOutput);
    }
    return parseOutput;
}

double ReadInputAndOnlyReturnDouble()
{
    while(true)
    {   
        ConsoleKeyInfo pressedKey = Console.ReadKey(intercept: true);
        if (pressedKey.Key != ConsoleKey.UpArrow)
        {
            while (!char.IsDigit(pressedKey.KeyChar))
            {
                Console.WriteLine("Enter only digits.");
                pressedKey = Console.ReadKey(intercept: true);
            }
            char initialPress = pressedKey.KeyChar;
            Console.Write(initialPress);
            string? readLineString = Console.ReadLine();
            string wholeOperand = initialPress + readLineString;
            
            return ReturnDoubleFromInput(wholeOperand);
        }
        else
        {
            if (History.GetPastResultCount() == 0)
            {
                Console.WriteLine("There is no past calculation yet.");
                string? wholeOperand = Console.ReadLine();
                
                return ReturnDoubleFromInput(wholeOperand);
            }
            else
            {
                int i;
                for (i = History.GetPastResultCount(); i > 0; i--)
                {
                    Console.WriteLine($"{History.GetPastResult(i)} (ENTER for selection and UP arrow to see more past result)");
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        break;
                    }


                }
                if (i == 0)
                {
                    Console.Write("There are no more past results. Enter another value for operand:");
                    string? wholeOperand = Console.ReadLine();
                    
                    return ReturnDoubleFromInput(wholeOperand);

                }
                return History.GetPastResult(i);

            }
        }
    }
    
    


}

int NumberSelectionMenu(string menuTitle, List<PossibleOperations> menuChoices)
{
    int parseOutput;
    while (true)
    { 
        Console.WriteLine(menuTitle);
        foreach (PossibleOperations choice in menuChoices)
        {
            Console.WriteLine($"[{(int)choice}] {choice}");
        }
        bool successfullyParsed = int.TryParse(Console.ReadLine(), out parseOutput);
        if (successfullyParsed && parseOutput < menuChoices.Count)
        {
            break;
        }
    }
    return parseOutput;
    
}

int OperatorSelectionMenu(string menuTitle, int typeOfOperation)
{
    int parseOutput;
    switch (typeOfOperation) 
    {
        case 0:
            while (true)
            {
                Console.WriteLine(menuTitle);
                foreach (PossibleArithmeticOperations choice in arithmeticOperationMenuChoices)
                {
                    Console.WriteLine($"[{(int)choice}] {choice}");
                }
                bool successfullyParsed = int.TryParse(Console.ReadLine(), out parseOutput);
                if (successfullyParsed && parseOutput < arithmeticOperationMenuChoices.Count)
                {
                    break;
                }
            }
            return parseOutput;
        case 1:
            while (true)
            {
                Console.WriteLine(menuTitle);
                foreach (PossibleTrigonometricOperations choice in trigonometricOperationMenuChoices)
                {
                    Console.WriteLine($"[{(int)choice}] {choice}");
                }
                bool successfullyParsed = int.TryParse(Console.ReadLine(), out parseOutput);
                if (successfullyParsed && parseOutput < trigonometricOperationMenuChoices.Count)
                {
                    break;
                }
            }
            return parseOutput;
        default:
            throw new NotImplementedException("This is not implemented yet");
    }
    
    

}


void RecurringMainChoices()
{
    while(true)
    {
        Console.WriteLine($"\n\nYou have used the calculator {useCount} times.");
        Console.WriteLine("\nPress Esc to close the environment.\nPress Enter to continue.\nPress H to view history\nPress DELETE to wipe history.\nPress D to delete selected history.\n");
        ConsoleKeyInfo key = Console.ReadKey();
        if (key.Key == ConsoleKey.Escape)
        {
            calculator.Finish();
            Environment.Exit(0);
        }
        else if (key.Key == ConsoleKey.Enter)
        {
            break;
        }
        else if (key.Key == ConsoleKey.H)
        {
            History.DisplayHistory();
            continue;
        }
        else if (key.Key == ConsoleKey.Delete)
        {
            History.DeleteHistory();
            continue;
        }
        else if(key.Key == ConsoleKey.D)
        {
            History.DisplayHistory();
            Console.WriteLine("\nEnter the calculation number you want to delete:");
            bool successfulParse = int.TryParse(Console.ReadLine(), out int deleteIndex);
            while (!successfulParse)
            {
                successfulParse = int.TryParse(Console.ReadLine(), out deleteIndex);
            }
            History.DeleteHistory(deleteIndex);
            continue;
        }
    }
    
    return;
}