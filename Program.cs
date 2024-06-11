Welcome();

while (true)
{
    try
    {
        char currentOperator = ' ';
        string input = Console.ReadLine() ?? "quit";
        input = input.Replace(" ", "");

        if (input == "quit") break;
        else if (input == "clear")
        {
            Console.Clear();
            Welcome();
            continue;
        }
        else if (checkBadStr(input))
        {
            throw new ArgumentException("The operation specified was invalid. Please try again.");
        }
        else
        {
            int opCount = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '-' || input[i] == '+' || input[i] == '/' || input[i] == '*')
                {
                    currentOperator = input[i];
                    opCount++;
                }
            }

            if (opCount > 1)
            {
                throw new ArgumentException("Expected one operator, recieved more than one. Please try again.");
            }
        }

        string[] operation = input.Split(currentOperator);

        if (currentOperator == '+' && !(operation[0].Contains('.') || operation[1].Contains('.')))
        {
            Console.WriteLine("Answer: " + intAddition(operation[0], operation[1]));
        }
        else if (currentOperator == '+' && (operation[0].Contains('.') || operation[1].Contains('.')))
        {
            Console.WriteLine("Answer: " + decAddition(operation[0], operation[1]));
        }
        else if (currentOperator == '-' && !(operation[0].Contains('.') || operation[1].Contains('.')))
        {
            Console.WriteLine("Answer: " + intSubtraction(operation[0], operation[1]));
        }
        else if (currentOperator == '-' && (operation[0].Contains('.') || operation[1].Contains('.')))
        {
            Console.WriteLine("Answer: " + decSubtraction(operation[0], operation[1]));
        }
        else if (currentOperator == '*' && !(operation[0].Contains('.') || operation[1].Contains('.')))
        {
            Console.WriteLine("Answer: " + intMulti(operation[0], operation[1]));
        }
        else if (currentOperator == '*' && (operation[0].Contains('.') || operation[1].Contains('.')))
        {
            Console.WriteLine("Answer: " + decMulti(operation[0], operation[1]));
        }
        else if (currentOperator == '/')
        {
            Console.WriteLine("Answer: " + division(operation[0], operation[1]));
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        continue;
    }

}

static void Welcome()
{
    Console.WriteLine("Welcome to my calculator app!");
    Console.WriteLine("Usage: Input a complete mathematical equation using two operands and one of the four basic math operators");
    Console.WriteLine("Type 'clear' to reset the log and type 'quit' to exit.");
}

static bool checkBadStr(string str)
{
    switch (true)
    {
        case true when str.Any(c => char.IsLetter(c)):
        case true when str.Any(c => char.IsSymbol(c) && !(c == '+' || c == '-' || c == '/' || c == '*')):
        case true when !(str.Contains('+') || str.Contains('-') || str.Contains('/') || str.Contains('*')):
        case true when str.Split('+').Length > 2:
        case true when str.Split('-').Length > 2:
        case true when str.Split('/').Length > 2:
        case true when str.Split('*').Length > 2:
            return true;
        default:
            return false;
    }
}

static int intAddition(string a, string b)
{
    if (int.TryParse(a, out int arg1) && int.TryParse(b, out int arg2))
    {
        return arg1 + arg2;
    }
    throw new ArgumentException("Integer addition failed, validate input and try again.");
}

static decimal decAddition(string a, string b)
{
    if (decimal.TryParse(a, out decimal arg1) && decimal.TryParse(b, out decimal arg2))
    {
        return arg1 + arg2;
    }
    throw new ArgumentException("Decimal addition failed, validate input and try again.");
}

static int intSubtraction(string a, string b)
{
    if (int.TryParse(a, out int arg1) && int.TryParse(b, out int arg2))
    {
        return arg1 - arg2;
    }
    throw new ArgumentException("Integer subtraction failed, validate input and try again.");
}

static decimal decSubtraction(string a, string b)
{
    if (decimal.TryParse(a, out decimal arg1) && decimal.TryParse(b, out decimal arg2))
    {
        return arg1 - arg2;
    }
    throw new ArgumentException("Decimal subtraction failed, validate input and try again.");
}

static int intMulti(string a, string b)
{
    if (int.TryParse(a, out int arg1) && int.TryParse(b, out int arg2))
    {
        return arg1 * arg2;
    }
    throw new ArgumentException("Integer multiplication failed, validate input and try again.");
}

static decimal decMulti(string a, string b)
{
    if (decimal.TryParse(a, out decimal arg1) && decimal.TryParse(b, out decimal arg2))
    {
        return arg1 * arg2;
    }
    throw new ArgumentException("Decimal multiplication failed, validate input and try again.");
}

static decimal division(string a, string b)
{
    if (decimal.TryParse(a, out decimal arg1) && decimal.TryParse(b, out decimal arg2))
    {
        if (arg2 == 0) throw new DivideByZeroException("You tried to divide by zero! You silly billy!");
        return arg1 / arg2;
    }
    throw new ArgumentException("Division failed, validate input and try again.");
}