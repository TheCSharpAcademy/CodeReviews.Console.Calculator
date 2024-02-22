using CalculatorLibrary;

var endApp = false;
var calculator = new Calculator();
while (!endApp)
{
    Calculator.CalculatorHeader();
    Console.WriteLine(@"Choose an operator from the following list: 
A - Add
S - Subtract
M - Multiply
D - Divide
SQRT - Square Root
P - Power of
P10 - Power of 10");
    Console.WriteLine("--------------------------------------------");
    var op = Console.ReadLine()?.Trim().ToLower();
    calculator.DoOperation(op);
    Console.Write("Press 'n' to close the app, or press any other key to continue: ");
    if(Console.ReadKey().Key == ConsoleKey.N) endApp = true;

    Console.WriteLine("\n");
}
calculator.Finish();