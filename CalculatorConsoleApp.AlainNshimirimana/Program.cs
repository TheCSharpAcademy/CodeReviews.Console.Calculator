using System;
using System.Buffers;
using System.Collections.Generic;

double num1;
double num2;
double result;
int numGames = 0;
string selection;
string operationType;

Console.WriteLine(" CALCULATOR APP ");
Console.WriteLine(" ------------- ");
Console.WriteLine("Welcome to the console Calculator App. With this app you will be able to perform maths operations");
MainMenu();
Console.WriteLine($"The number of oparetions performed is {numGames}");
Console.WriteLine("\nGoodbye!");

void MainMenu()
{
    Console.WriteLine("\nWhat would you like to do?: ");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("A. Addition");
    Console.WriteLine("S. Subtraction");
    Console.WriteLine("D. Division");
    Console.WriteLine("M. Multiplication");
    Console.WriteLine("R. Square Root");
    Console.WriteLine("P. Raise to a power");
    Console.WriteLine("V. View History (not functional at the moment)");
    Console.WriteLine("E. Exit");
    Console.Write("> ");
    Console.ResetColor();
    selection = Console.ReadLine().Trim().ToUpper();
    Console.WriteLine();

    while (selection != "A" && selection != "S" &&  selection != "D" && selection != "M" && selection != "E" && selection != "R" && selection != "P")
    {
        Console.WriteLine("Invalid selection. Please enter A, S, D, M, or E to exit");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("> ");
        Console.ResetColor();
        selection = Console.ReadLine().ToUpper();
    }

    Console.Clear();

    switch (selection)
    {
        case "A":
            Addition();
            break;
        case "S":
            Subtraction();
            break;
        case "D":
            Division();
            break;
        case "M":
            Multiplication();
            break;
        case "R":
            SquareRoot();
            break;
        case "P":
            Power();
            break;
        default:
            break;
    }
}

void Power()
{
    operationType = "Power";
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("POWER");
    Console.ResetColor();
    Console.WriteLine("--------");
    Console.Write("Base: ");
    num1 = double.Parse(Console.ReadLine());
    Console.Write("Power: ");
    num2 = double.Parse(Console.ReadLine());
    result = Math.Pow(num1, num2);
    Console.WriteLine($"{num1} ^ {num2} = {result}");
    numGames++;
    MainMenu();
}

void SquareRoot()
{
    operationType = "Square Root";
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("SQUARE ROOT");
    Console.ResetColor();
    Console.WriteLine("--------");
    Console.Write("Enter a number: ");
    num1 = double.Parse(Console.ReadLine());
    result = Math.Sqrt(num1);
    Console.WriteLine($"√{num1} = {result}");
    numGames++;
    MainMenu();
}

void Multiplication()
{
    operationType = "Multiplication";
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("MULTIPLICATION");
    Console.ResetColor();
    Console.WriteLine("--------");
    Console.Write("First number: ");
    num1 = double.Parse(Console.ReadLine());
    Console.Write("Second number: ");
    num2 = double.Parse(Console.ReadLine());
    result = num1 * num2;
    Console.WriteLine($"{num1} × {num2} = {result:0.00}");
    numGames++;
    MainMenu();
}

void Division()
{
    operationType = "Division";
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("DIVISION");
    Console.ResetColor();
    Console.WriteLine("--------");
    Console.Write("First number: ");
    num1 = double.Parse(Console.ReadLine());
    Console.Write("Second number: ");
    num2 = double.Parse(Console.ReadLine());
    result = num1 / num2;
    Console.WriteLine($"{num1} ÷ {num2} = {result:0.00}");
    numGames++;
    MainMenu();
}

void Subtraction()
{
    operationType = "Subtraction";
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("SUBTRACTION");
    Console.ResetColor();
    Console.WriteLine("--------");
    Console.Write("First number: ");
    num1 = double.Parse(Console.ReadLine());
    Console.Write("Second number: ");
    num2 = double.Parse(Console.ReadLine());
    result = num1 - num2;
    Console.WriteLine($"{num1} - {num2} = {result:0.00}");
    numGames++;
    MainMenu();
}

void Addition()
{
    operationType = "Addition";
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("ADDITION");
    Console.ResetColor();
    Console.WriteLine("--------");
    Console.Write("First number: ");
    num1 = double.Parse(Console.ReadLine());
    Console.Write("Second number: ");
    num2 = double.Parse(Console.ReadLine());
    result = num1 + num2;
    Console.WriteLine($"{num1} + {num2} = {result:0.00}");
    numGames++;
    MainMenu();
}