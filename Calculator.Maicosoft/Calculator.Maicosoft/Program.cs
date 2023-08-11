using Calculator.Maicosoft;

Console.WriteLine("--------------------------");
Console.WriteLine("\tCalculator");
Console.WriteLine("--------------------------\n");

Console.WriteLine("Please enter your first number: ");
double num1 = Convert.ToDouble(Console.ReadLine());

Console.WriteLine("Please enter your second number: ");
double num2 = Convert.ToDouble(Console.ReadLine());

Console.WriteLine("Please enter an operation:");
Console.WriteLine("\ta - Adittion");
Console.WriteLine("\ts - Subtraction");
Console.WriteLine("\tm - Multiply");
Console.WriteLine("\td - Division");
Console.WriteLine("Make you choice: ");

var operation = Console.ReadLine().Trim().ToLower();

switch (operation)
{
    case "a":
        var resultA = Calculate.Addition(num1, num2);
        Console.WriteLine($"{num1} + {num2} = {resultA}");
        break;
    case "s":
        var resultS =  Calculate.Subtraction(num1, num2);
        Console.WriteLine($"{num1} - {num2} = {resultS}");
        break;
    case "m":
        var resultM =  Calculate.Multiply(num1, num2);
        Console.WriteLine($"{num1} * {num2} = {resultM}");
        break;
    case "d":
        var resultD = Calculate.Division(num1, num2);
        Console.WriteLine($"{num1} / {num2} = {resultD}");
        break;
}
    
