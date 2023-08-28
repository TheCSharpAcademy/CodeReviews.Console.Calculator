//Calculator app from https://learn.microsoft.com/en-us/visualstudio/get-started/csharp/tutorial-console?view=vs-2022

class Calculator
{
    double num1;
    double num2;
    string op;

    public void setNumbers()//here we are setting values of number1 and number2
    {
        string input;
        //get num1 value
        while (true)
        {
            Console.Write("Type a number, and then press Enter: ");
            input = Console.ReadLine()!;
            try
            {
                num1 = double.Parse(input);
                break;
            }
            catch
            {
                Console.WriteLine("This is no a valid input!");
            }
        }
        //get num2 value
        while (true)
        {
            Console.Write("Type another number, and then press Enter: ");
            input = Console.ReadLine()!;
            try
            {
                num2 = double.Parse(input);
                break;
            }
            catch
            {
                Console.WriteLine("This is no a valid input!");
            }
        }
    }

    public void chooseOperation()
    {
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine($"\ta - Add\n\ts - Substract\n\tm - Multiply\n\td - Divide");
        Console.Write("Your option: ");
        while (true)
        {
            op = Console.ReadLine()!;
            op.ToLower();
            if (op == "a" || op == "s" || op == "m" || op == "d")
            {
                break;
            }
            else
            {
                Console.WriteLine("Please choose correct option.");
            }
        }
    }

    public void printNumbers()//debug
    {
        Console.WriteLine($"num1 = {num1}, num2 = {num2}");
    }

    public double calculate()
    {
        switch (op)
        {
            case "a":
                return num1 + num2;
            case "s":
                return num1 - num2;
            case "m":
                return num1 * num2;
            case "d":
                return division();
        }
        return 0;
    }

    public double division()
    {
        if (num1 == 0)
        {
            return 0;
        } 
        else if (num2 == 0)
        {
            Console.WriteLine("Can't divide by 0, returning 0 instead.");
            return 0;
        }
        else
        { 
            return num1 / num2;
        }
    }

    public bool end()
    {
        Console.WriteLine($"\n-------------------\n");
        Console.Write("Press n and Enter to close the app, or press any other key and Enter to continue: ");
        string input = Console.ReadLine()!;
        if (input == "n")
        {
            return false;
        }
        else
        {
            Console.WriteLine($"\n");
            return true;
        }
    }

}

class Program
{
    static void Main(string[] args)
    {
        bool appLoop = true;
        Calculator calc = new Calculator();
        
        while (appLoop)
        {
            calc.setNumbers();
            calc.chooseOperation();
            Console.WriteLine($"Your result: {calc.calculate():0.##}");
            appLoop = calc.end();
        }
    }
}