using System;
using System.Buffers;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

string response1;
string response2;
double num1;
double num2;
double result;
int numGames = 0;
string selection;

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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("ADDITION");
            Console.ResetColor();
            Console.WriteLine("--------");
            Addition();
            break;
        case "S":
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("SUBTRACTION");
            Console.ResetColor();
            Console.WriteLine("--------");
            Subtraction();
            break;
        case "D":
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("DIVISION");
            Console.ResetColor();
            Console.WriteLine("--------");
            Division();
            break;
        case "M":
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("MULTIPLICATION");
            Console.ResetColor();
            Console.WriteLine("--------");
            Multiplication();
            break;
        case "R":
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("SQUARE ROOT");
            Console.ResetColor();
            Console.WriteLine("--------");
            SquareRoot();
            break;
        case "P":
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("POWER");
            Console.ResetColor();
            Console.WriteLine("--------");
            Power();
            break;
        default:
            break;
    }
}

void Power()
{
    Console.Write("Base: ");
    response1 = Console.ReadLine();
    Console.Write("Power: ");
    response2 = Console.ReadLine();
    if (Double.TryParse(response1, out num1) && Double.TryParse(response2, out num2))
    {
        result = Math.Pow(num1, num2);
        Console.WriteLine($"{num1} ^ {num2} = {result}");
        numGames++;
        MainMenu();
    }
    else
    {
        Console.WriteLine("ERROR!: Please input NUMBERS only");
        Power();
    }    
}

void SquareRoot()
{
    Console.Write("Enter a number: ");
    response1 = Console.ReadLine();
    if (Double.TryParse(response1, out num1))
    {
        result = Math.Sqrt(num1);
        Console.WriteLine($"√{num1} = {result}");
        numGames++;
        MainMenu();
    }
    else
    {
        Console.WriteLine("ERROR!: Please input a NUMBER");
        SquareRoot();
    }
}

void Multiplication()
{
    Console.Write("First number: ");
    response1 = Console.ReadLine();
    Console.Write("Second number: ");
    response2 = Console.ReadLine();
    if (Double.TryParse(response1, out num1) && Double.TryParse(response2, out num2))
    {
        result = num1 * num2;
        Console.WriteLine($"{num1} × {num2} = {result}");
        numGames++;
        MainMenu();
    }
    else
    {
        Console.WriteLine("ERROR!: Please input NUMBERS only");
        Multiplication();
    }
}

void Division()
{    
    Console.Write("First number: ");
    response1 = Console.ReadLine();
    Console.Write("Second number: ");
    response2 = Console.ReadLine();
    if (Double.TryParse(response1, out num1) && Double.TryParse(response2, out num2))
    {
        result = num1 / num2;
        Console.WriteLine($"{num1} ÷ {num2} = {result}");
        numGames++;
        MainMenu();
    }
    else
    {
        Console.WriteLine("ERROR!: Please input NUMBERS only");
        Division();
    }
}

void Subtraction()
{
    Console.Write("First number: ");
    response1 = Console.ReadLine();
    Console.Write("Second number: ");
    response2 = Console.ReadLine();
    if (Double.TryParse(response1, out num1) && Double.TryParse(response2, out num2))
    {
        result = num1 - num2;
        Console.WriteLine($"{num1} - {num2} = {result}");
        numGames++;
        MainMenu();
    }
    else
    {
        Console.WriteLine("ERROR!: Please input NUMBERS only");
        Subtraction();
    }
}

void Addition()
{
    Console.Write("First number: ");
    response1 = Console.ReadLine();
    Console.Write("Second number: ");
    response2 = Console.ReadLine();
    if(Double.TryParse(response1, out num1) && Double.TryParse(response2, out num2))
    {
        result = num1 + num2;
        Console.WriteLine($"{num1} + {num2} = {result}");
        numGames++;
        MainMenu();
    }
    else
    {
        Console.WriteLine("ERROR!: Please input NUMBERS only");
        Addition();
    }
}