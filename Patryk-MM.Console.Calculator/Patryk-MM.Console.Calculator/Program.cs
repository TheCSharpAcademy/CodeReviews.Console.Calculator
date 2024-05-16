using CalculatorLibrary;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

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
Console.WriteLine("Console Calculator in C#\r");
Console.WriteLine("------------------------\n");

while (true) {
    PrintMenu();
    char input = GetValidInput("\nChoose the option (press Q for displaying menu): ", validMenuInputs);
    
    switch (input) {
        case 'P':
            PrintOperations();
            char operationChoice = GetValidInput("Choose the operation you want to perform: ", validTwoParamInputs.Concat(validSingleParamInputs).ToArray());

            if (validTwoParamInputs.Contains(operationChoice)) {
                double num1 = GetNumber("Enter the first number: ");
                double num2 = GetNumber("Enter the second number: ");
                //Call below both handles the calculation and adds it to the start of the list.
                calculator.Calculations.Insert(0, calculator.HandleTwoParameterOperation(new Calculation(num1, num2, operationChoice)));
            } else if (validSingleParamInputs.Contains(operationChoice)) {
                double num = GetNumber("Enter the number: ");
                //Call below both handles the calculation and adds it to the list.
                calculator.Calculations.Insert(0, calculator.HandleSingleParameterOperation(new Calculation(num, operationChoice)));
            }

            break;
        case 'L':

            Console.WriteLine("Calculation history:");
            foreach (var calc in calculator.Calculations) {
                if (validTwoParamInputs.Contains(calc.Operation)) {
                    Console.WriteLine($"\t{calculator.Calculations.IndexOf(calc) + 1, -2} " +
                        $"| Operation: {operations[calc.Operation], -20} | Numbers: {calc.Num1}, {calc.Num2} | Result: {calc.Result}");
                } else {
                    Console.WriteLine($"\t{calculator.Calculations.IndexOf(calc) + 1, -2} " +
                        $"| Operation: {operations[calc.Operation], -20} | Number: {calc.Num1} | Result: {calc.Result:0.##}");
                }
            }
            break;
        case 'Q':
            PrintMenu();
            break;
    }

    //What happens after each calculation
}

void PrintMenu() {
    Console.WriteLine("\nMenu:");
    Console.WriteLine("\tP - Perform an mathematical operation.");
    Console.WriteLine("\tL - View a list of recent operations.");
    Console.WriteLine("\tN - Exit the game");
}
void PrintOperations() {
    Console.WriteLine("\nOperations:");
    foreach (var operation in operations) {
        Console.WriteLine($"\t{operation.Key} - {operation.Value}");
    }
}


static char GetValidInput(string prompt, char[] validInputs) {
    char input;
    do {
        Console.Write(prompt);
        input = Char.ToUpper(Console.ReadKey().KeyChar);
        Console.WriteLine();
        if (!validInputs.Contains(input)) {
            Console.WriteLine("\nPlease provide a valid option from the menu.\n");
        }
    } while (!validInputs.Contains(input));
    return input;
}

static double GetNumber(string prompt) {
    double number;
    while (true) {
        Console.Write(prompt);
        if (double.TryParse(Console.ReadLine(), out number)) {
            return number;
        }
        Console.WriteLine("Invalid input. Please enter a valid number.");
    }
}