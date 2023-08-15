class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        int calculationCounter = 0;
        List<string> calculationHistory = new List<string>();
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        while (!endApp)
        {
            // Declare variables and set to empty.
            string numInput1 = "";
            string numInput2 = "";
            double result = 0;

            // Ask the user to choose an operator.
            Console.WriteLine(
$@"Choose an operator from the following list:
    +   : Add
    -   : Subtract
    x   : Multiply
    /   : Divide
    r   : Square Root
    ^   : Exponent
    sin : sine (radians)
    cos : cosine (radians)
    tan : tangent (radians)
    log : logarithm (base 10)
    ln  : logarithm (base e)

    v - View History

    {calculationCounter} total calculations

Selection: ");
            string op = Console.ReadLine().ToLower();

            double cleanNum1 = 0;
            double cleanNum2 = 0;

            if (op == "v")
            {
                Console.WriteLine("------------------------\n");
                foreach (string calculation in calculationHistory)
                {
                    Console.WriteLine(calculation);
                }
            }
            else
            {
                if (op == "+" | op == "-" | op == "x" | op == "/" | op == "^")
                {
                    // Ask the user to type the first number.
                    Console.Write("(Select a stored result with %#)\nType a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();

                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        if (numInput1[0] == '%')
                        {
                            bool found = false;
                            numInput1 = numInput1.Substring(1);
                            foreach (string calculation in calculationHistory)
                            {
                                if (numInput1 == calculation.Split(':')[0])
                                {
                                    numInput1 = calculation.Split(':')[1].Split(' ')[5];
                                    Console.WriteLine($"Using stored result: {numInput1}");
                                    found = true;
                                }
                            }
                            if (!found)
                            {
                                Console.WriteLine("Record not found. Please try again or enter an integer value: ");
                                numInput1 = Console.ReadLine();
                            }
                        }
                        else
                        {
                            while (!double.TryParse(numInput1, out cleanNum1))
                            {
                                Console.Write("This is not valid input. Please enter an integer value: ");
                                numInput1 = Console.ReadLine();
                            }
                        }
                    }
                }

                // Ask the user to type the second number. (first if only 1 operand)
                Console.Write("(Select a stored result with %#)\nType a number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    if (numInput2[0] == '%')
                    {
                        bool found = false;
                        numInput2 = numInput2.Substring(1);
                        foreach (string calculation in calculationHistory)
                        {
                            if (numInput2 == calculation.Split(':')[0])
                            {
                                numInput2 = calculation.Split(':')[1].Split(' ')[5];
                                Console.WriteLine($"Using stored result: {numInput2}");
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            Console.WriteLine("Record not found. Please try again or enter an integer value: ");
                            numInput2 = Console.ReadLine();
                        }
                    }
                    else
                    {
                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter an integer value: ");
                            numInput2 = Console.ReadLine();
                        }
                    }
                }

                try
                {
                    result = Calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        calculationCounter++;
                        calculationHistory.Add($"{calculationCounter}: {cleanNum1} {op} {cleanNum2} = {result}");
                        Console.WriteLine($"Your result: {result:0.##}\n\nCalulations completed: {calculationCounter}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        return;
    }
}