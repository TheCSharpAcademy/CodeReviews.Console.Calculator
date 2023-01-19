// See https://aka.ms/new-console-template for more information
global using System;

bool endApp = false;
Console.WriteLine("Console Calculator in c#\r");
Console.WriteLine("------------------------\n");
var calculator = new Calculator.yashsachdev.Calculator(); 
while (!endApp)
{
    string numInput1 = "";
    string numInput2 = "";
    double result = 0;
    Console.Write("Type a number, and then press Enter: ");
    numInput1 = Console.ReadLine();
    double cleanNum1 = 0;
    while (!double.TryParse(numInput1, out cleanNum1))
    {
        Console.Write("This is not valid input. Please enter an integer value: ");
        numInput1 = Console.ReadLine();
    }
    if (calculator.cnt == 0) 
    {
        Console.WriteLine("Type Another Number");
    }
    else
    {
        Console.Write("Type another number OR type 'list' to use results from previous calculations , and then press Enter: ");
    }
    numInput2 = Console.ReadLine();
    if (numInput2 == "list" && calculator.store.Count != 0)
    {
        calculator.DisplayResult();
        Console.Write("Enter result index: ");
        int index = int.Parse(Console.ReadLine());
        double Input2 = 0;
        if (index >= 0 && index < calculator.store.Count) //validating list.
        {
            Input2 = calculator.store[index]; //enter the list value as input 
        }
        else
        {
            Console.WriteLine("oops the index was out of bound lets try again.");
            continue;
        }
        Console.WriteLine("\t1 - Display Result");
        Console.WriteLine("\t2 - Clear Result");
        Console.WriteLine("\t3 - Perform operations");
        string str = Console.ReadLine();
        switch(str)
        {
            case "1": calculator.DisplayResult();
                break;
            case "2":calculator.RemoveList();
                break;
            case "3":
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("type 'more' to perform 10x, ^, sin, cos, tan  operations ");
                Console.Write("Your option? ");
                string oper = Console.ReadLine(); 
                try
                {
                    result = calculator.DoOperation(cleanNum1, Input2, oper);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else if (result == 0)
                    {
                        Console.WriteLine("\t");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to the list .\n - Details: " + ex.Message);
                }
                Console.WriteLine("------------------------\n");
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                 if (Console.ReadLine() == "n") endApp = true;
                Console.WriteLine("\n");
                break;
            default: 
                break;
        }
    }
    else
    {
        double cleanNum2 = 0;
        while (!double.TryParse(numInput2, out cleanNum2))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            numInput2 = Console.ReadLine();
        }
        Console.WriteLine("\t1 - Display Result");
        Console.WriteLine("\t2 - Clear Result");
        Console.WriteLine("\t3 - Perform operations");
        string str = Console.ReadLine();
        switch (str)
        {
            case "1":
                calculator.DisplayResult();
                break;
            case "2":
                calculator.RemoveList();
                break;
            case "3":
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("type 'more' to perform 10x, ^, sin, cos, tan  operations ");
                Console.Write("Your option? ");
                string op = Console.ReadLine();
                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n"); 
                    }
                    else if (result == 0)
                    {
                        Console.WriteLine(" \t");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
                Console.WriteLine("------------------------\n");
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;
                Console.WriteLine("\n");
                break;
            default:
                break;
        }
    }
}
