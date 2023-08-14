using CalculatorLibrary;

bool endProgramm = false;
Calculate calculate = new();
while (!endProgramm)
{
    Console.WriteLine("--------------------------");
    Console.WriteLine("\tCalculator");
    Console.WriteLine("--------------------------\n");

    Console.WriteLine("Please enter your first number: ");
    var num1 = Console.ReadLine();
    double finalnum1;
    while (!double.TryParse(num1, out finalnum1))
    {
        Console.WriteLine("That's not a valid character, type a number: ");
        num1 = Console.ReadLine();
    }

    Console.WriteLine("Please enter your second number: ");
    var num2 = Console.ReadLine();
    double finalnum2;
    while (!double.TryParse(num2, out finalnum2))
    {
        Console.WriteLine("That's not a valid character, type a number: ");
        num2 = Console.ReadLine();
    }

    Console.WriteLine("Please enter an operation:");
    Console.WriteLine("\ta - Adittion");
    Console.WriteLine("\ts - Subtraction");
    Console.WriteLine("\tm - Multiply");
    Console.WriteLine("\td - Division");
    Console.WriteLine("Make you choice: ");

    string operation = Console.ReadLine().Trim().ToLower();

    string result = calculate.DoMath(finalnum1, finalnum2, operation);

    Console.WriteLine("\nThe result is:\n" + result);

    Console.WriteLine("Do you want to do another calculation? y/n");
    if (Console.ReadLine() == "n")
    {
        endProgramm = true;
    }
    calculate.Finish();
    Console.Clear();
}