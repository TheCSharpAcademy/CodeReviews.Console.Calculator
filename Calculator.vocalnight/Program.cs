using CalculatorLibrary;

double num1 = 0;
double num2 = 0;
var calculations = new List<double>();

bool endApp = false;

Console.WriteLine("Console Calculator in C#\r");
Console.WriteLine("-----------------------\n");

Calculator calculator = new Calculator();

while (!endApp) {

    double result = 0;

    Console.WriteLine("Type a number, and then press Enter");

    num1 = CheckValidity(Console.ReadLine());

    Console.WriteLine("Type another number and then press Enter");
    num2 = CheckValidity(Console.ReadLine());

    Console.WriteLine("Choose an option from the following list:");
    Console.WriteLine($"So far, there have been {calculations.Count} calculations made in this session.");
    Console.WriteLine
    ($@"a - Add
s - Subtract
m - Multiply
d - Divide
del - delete past calculations
rep - replace the second number with the last calculation
Your option?");


    string op = Console.ReadLine();

    while (op == "del" || op == "rep") {

        if (op == "rep") {
            num2 = ReplaceNumber(calculations, num2);
        } else {
            ClearCalculationList(calculations);
        }

        op = Console.ReadLine();
    }

    PerformOperation(num1, num2, calculations, calculator, result, op);

    Console.WriteLine("--------------\n");

    Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
    if (Console.ReadLine() == "n") endApp = true;

    Console.WriteLine("\n");
}
calculator.Finish();
return;


double CheckValidity( string? v ) {

    while (!double.TryParse(v, out _)) {
        Console.WriteLine("Enter a valid number");
        v = Console.ReadLine();
    }

    return Convert.ToDouble(v);
}

static double PerformOperation( double num1, double num2, List<double> calculations, Calculator calculator, double result, string op ) {
    try {
        result = calculator.DoOperation(num1, num2, op);
        if (double.IsNaN(result)) {
            Console.WriteLine("This operation will result in error\n");
        } else {
            Console.WriteLine("Your result: {0:0.##}\n", result);
            calculations.Add(result);
        }
    } catch (Exception ex) {
        Console.WriteLine("An error ocurred");
    }

    return result;
}

static double ReplaceNumber( List<double> calculations, double secondNumber ) {
    if (calculations.Count == 0) {
        Console.WriteLine("There are no calculations saved. Perform a calculation first");
        return secondNumber;
    } else {
        Console.WriteLine($"Num 2 changed to {calculations.Last()}! Please make a selection for operation now");
        return calculations.Last();
    }
}

static void ClearCalculationList( List<double> calculations ) {
    calculations.Clear();
    Console.WriteLine("List of calculations cleared");
}