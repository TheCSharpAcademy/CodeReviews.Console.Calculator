// See https://aka.ms/new-console-template for more information
using CalculatorLibrary;

bool endApp = false;
Calculator calculator = new Calculator();
int timesCalculatorUsed = 0;
string userOption;
bool sillyJoke = false;

while (endApp == false)
{

    double num1 = 0; double num2 = 0; double result = 0;

    if (timesCalculatorUsed == 2)
    {
        sillyJoke = true;
    }

    Console.WriteLine("Welcome! This is the console calculator App");
    Console.WriteLine("---------------------------------------------");

    Console.Write(@$"Select an Option
        p - Perform calculation.
        t - Check how many times calculator was used during this session.
        v - View the list of operations performed.
        b - Use a previous result from the list to perform new operations.
        d - Delete the list.
        n - Close the App.");

    userOption = Console.ReadLine();

    while (userOption != "p" && userOption != "t" && userOption != "v" && userOption != "d" && userOption != "n" && userOption != "b")
    {
        Console.WriteLine("Invalid input!");
        userOption = Console.ReadLine();
    }

    if(userOption == "n")
    {
        endApp = true;
    }
    else if(userOption == "t")
    {
        if(sillyJoke == true)
        {
            Console.Clear();
            Console.WriteLine($"The calculator was used {timesCalculatorUsed} times. Don´t you think you have used the calculator enough times already... ");
            Console.ReadLine();
            Console.WriteLine("Just kidding! Enjoy! Press any key to continue!");
            Console.ReadLine();
            sillyJoke = false;
            Console.Clear();
        }
        else
        {
            Console.Clear();
            Console.WriteLine($"The calculator was used {timesCalculatorUsed} times. Press any key to continue!");
            Console.ReadLine();
        }
    }
    else if (userOption == "v")
    {
        List<CalculationsRecord.OperationRecord> operationsList = calculator.GetOperationsList();
        Console.Clear();
        Console.WriteLine("List of operations performed:");
        foreach (var operationRecord in operationsList)
        {
            Console.WriteLine($"Id: {operationRecord.Id}, Operation: {operationRecord.NumA1} {operationRecord.Operation} {operationRecord.NumA2}, Result: {operationRecord.ResultA}");
        }
        Console.WriteLine("Press any key to continue!");
        Console.ReadLine();
        Console.Clear();
    }
    else if(userOption == "b")
    {
        Console.Clear();
        if(timesCalculatorUsed >= 1)
        {
            List<CalculationsRecord.OperationRecord> operationsList = calculator.GetOperationsList();
            Console.Clear();
            Console.WriteLine("List of operations performed:");
            foreach (var operationRecord in operationsList)
            {
                Console.WriteLine($"Id: {operationRecord.Id}, Operation: {operationRecord.NumA1} {operationRecord.Operation} {operationRecord.NumA2}, Result: {operationRecord.ResultA}");
            }

            Console.WriteLine("Choose an Id containing the result you want to use:");
            int idChosen = int.Parse(Console.ReadLine());

            num1 = calculator.GetResultFromList(idChosen);

            calculator.UIOperationHandler(num1, num2, result, endApp, userOption);
            timesCalculatorUsed++;
        }
        else
        {
            Console.WriteLine("No operations were performed yet! Press any key to return to main menu!");
            Console.ReadLine();
            Console.Clear();
        }

    }
    else if (userOption == "d")
    {
        calculator.ClearOperationsList();
        Console.Clear();
        Console.WriteLine("List of operations cleared!");
        Console.WriteLine("Press any key to continue!");
        Console.ReadLine();
        calculator.operationId = 1;
        Console.Clear();
    }
    else if(userOption == "p")
    {
        calculator.UIOperationHandler(num1, num2, result, endApp, userOption);
        timesCalculatorUsed++;
    }
}

calculator.Finish();