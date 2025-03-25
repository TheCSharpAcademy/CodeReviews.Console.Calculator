Console.WriteLine("--- WELCOME TO C# CALCULATOR ---");

// Initialize the two operands
int num1 = 0; int num2 = 0;

// Prompt the user to enter the first number
Console.WriteLine("Enter the first number!");
num1 = Convert.ToInt32(Console.ReadLine());

// Prompt the user to enter the second number
Console.WriteLine("Enter the second number!");
num2 = Convert.ToInt32(Console.ReadLine());

// Prompt the user to choose a operator
Console.WriteLine("Choose an operator from the list!");
Console.WriteLine("\ta - Addition");
Console.WriteLine("\ts - Subtraction");
Console.WriteLine("\tm - Multiplication");
Console.WriteLine("\td - Division");
Console.WriteLine("Your Operator: ");

// Perform operations according to the user's choice

string? operand = Console.ReadLine();

switch (operand.ToLower())
{
    case "a":
        Console.WriteLine($"Result: {num1} + {num2} = {num1 + num2}");
        break;
    case "s":
        Console.WriteLine($"Result: {num1} - {num2} = {num1 - num2}");
        break;
    case "m":
        Console.WriteLine($"Result: {num1} * {num2} = {num1 * num2}");
        break;
    case "d":
        Console.WriteLine($"Result: {num1} / {num2} = {num1 / num2}");
        break;
}

Console.ReadLine();