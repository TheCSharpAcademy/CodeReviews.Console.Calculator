using CalculatorLibrary;
using ConsoleTables;

char[] validMenuInputs = ['P', 'L', 'N', 'Q'];
char[] validTwoParamInputs = { '+', '-', '*', '/', 'E' };
char[] validSingleParamInputs = { 'R', 'P', 'S', 'C', 'T' };

Dictionary<char, string> operations = new Dictionary<char, string>() {
    {'+', "Addition"},
    {'-', "Subtraction"},
    {'*', "Multiplication"},
    {'/', "Division"},
    {'E', "Exponentiation (x^y)"},
    {'R', "Square Root"},
    {'P', "10^x"},
    {'S', "Sine"},
    {'C', "Cosine"},
    {'T', "Tangent"}
};

Calculator calculator = new Calculator();


//Display title as the C# console calculator app.
TextFormat.WriteLine("Console Calculator in C#", ConsoleColor.Magenta);
TextFormat.WriteLine("------------------------", ConsoleColor.Magenta);
PrintMenu();

while (true) {
    char input = GetValidInput("Choose the option (press Q for displaying menu): ", validMenuInputs);

    switch (input) {
        case 'P':
            PrintOperations();
            char operationChoice = GetValidInput("Choose the operation you want to perform: ", validTwoParamInputs.Concat(validSingleParamInputs).ToArray());

            if (validTwoParamInputs.Contains(operationChoice)) {
                double num1 = GetNumber("Enter the first number: ");
                double num2 = GetNumber("Enter the second number: ");
                //Call below both handles the calculation and adds it to the start of the list.
                calculator.Calculations.Add(calculator.HandleTwoParameterOperation(new Calculation(num1, num2, operationChoice)));
            } else if (validSingleParamInputs.Contains(operationChoice)) {
                double num = GetNumber("Enter the number: ");
                //Call below both handles the calculation and adds it to the list.
                calculator.Calculations.Add(calculator.HandleSingleParameterOperation(new Calculation(num, operationChoice)));
            }
            PrintMenu();
            break;
        case 'L':
            GetCalculationHistory();
            PrintMenu();
            break;
        case 'Q':
            PrintMenu();
            break;
        case 'N':
            TextFormat.WriteLine("Thank you for using my calculator! :)", ConsoleColor.Magenta);
            return;
    }
    calculator.WriteToJson();
}

void PrintMenu() {
    TextFormat.WriteLine("\nMenu:", ConsoleColor.Yellow);
    TextFormat.WriteLine("\tP - Perform a mathematical operation.", ConsoleColor.Yellow);
    TextFormat.WriteLine("\tL - View a list of recent operations.", ConsoleColor.Yellow);
    TextFormat.WriteLine("\tN - Exit the game", ConsoleColor.Yellow);
}
void PrintOperations() {
    TextFormat.WriteLine("\nOperations:", ConsoleColor.Yellow);
    foreach (var operation in operations) {
        TextFormat.WriteLine($"\t{operation.Key} - {operation.Value}", ConsoleColor.Yellow);
    }
}


static char GetValidInput(string prompt, char[] validInputs) {
    char input;
    do {
        TextFormat.Write(prompt, ConsoleColor.Green);
        input = Char.ToUpper(Console.ReadKey().KeyChar);
        Console.WriteLine();
        if (!validInputs.Contains(input)) {
            TextFormat.WriteLine("Please provide a valid option from the menu.\n", ConsoleColor.Red);
        }
    } while (!validInputs.Contains(input));
    return input;
}

static double GetNumber(string prompt) {
    double number;
    while (true) {
        TextFormat.Write(prompt, ConsoleColor.Green);
        if (double.TryParse(Console.ReadLine(), out number)) {
            return number;
        }
        TextFormat.WriteLine("Invalid input. Please enter a valid number.", ConsoleColor.Red);
    }
}

void GetCalculationHistory() {
    if (calculator.Calculations.Count == 0) {
        TextFormat.WriteLine("There are no calculations in the history.", ConsoleColor.Red);
        return;
    }

    Console.WriteLine("\nCalculation history:\n");
    ConsoleTable table = new ConsoleTable("ID", "Operation", "Number(s)", "Result");
    foreach (var calc in calculator.Calculations) {
        if (validTwoParamInputs.Contains(calc.Operation)) {
            table.AddRow($"{calculator.Calculations.IndexOf(calc) + 1}", $"{operations[calc.Operation]}", $"{calc.Num1}, {calc.Num2}", $"{calc.Result}");
        } else {
            table.AddRow($"{calculator.Calculations.IndexOf(calc) + 1}", $"{operations[calc.Operation]}", $"{calc.Num1}", $"{calc.Result}");
        }
    }
    table.Write();
    Console.WriteLine("\n");

    switch (GetValidInput("Do you want to use any of the results to perform another calculation? (Y for yes, N for no.) ", ['Y', 'N'])) {
        
        case 'Y':
            Calculation historyCalc = new Calculation();
            bool isValidInput = false;
            while (!isValidInput) {
                try {
                    historyCalc = calculator.Calculations[Convert.ToInt32(GetNumber("Please provide the number of calculation the result you want to use: ")) - 1];
                    isValidInput = true;
                } catch {
                    TextFormat.WriteLine("Please provide a number that corresponds to a list entry.", ConsoleColor.Red);
                } 
            }
            TextFormat.WriteLine($"Chosen number: {historyCalc.Result}", ConsoleColor.Cyan);
            PrintOperations();

            char operationChoice = GetValidInput("Choose the operation you want to perform: ", validTwoParamInputs.Concat(validSingleParamInputs).ToArray());

            if (validTwoParamInputs.Contains(operationChoice)) {
                double num2 = GetNumber("Enter the second number: ");
                //Call below both handles the calculation and adds it to the start of the list.
                calculator.Calculations.Add(calculator.HandleTwoParameterOperation(new Calculation(historyCalc.Result, num2, operationChoice)));
            } else if (validSingleParamInputs.Contains(operationChoice)) {
                //Call below both handles the calculation and adds it to the list.
                calculator.Calculations.Add(calculator.HandleSingleParameterOperation(new Calculation(historyCalc.Result, operationChoice)));
            }
            break;
        case 'N': return;
    }
}