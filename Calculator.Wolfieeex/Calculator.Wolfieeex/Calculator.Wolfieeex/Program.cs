double firstNumber = 0;
double secondNumber = 0;

Console.WriteLine("Welcome to Console Calculator in C#!\r");
Console.WriteLine($"{new string('-', Console.BufferWidth)}\n");

AskForNumber("first");
ReadNumericInput(ref firstNumber, "first");

AskForNumber("second");
ReadNumericInput(ref secondNumber, "second");

Console.WriteLine("\n\nChoose one of the options below: ");
Console.WriteLine($"\tA - Add");
Console.WriteLine($"\tS - Subtract");
Console.WriteLine($"\tM - Multiply");
Console.WriteLine($"\tD - Divide");
Console.Write($"Please select your option: ");

string? userOption = null;
ReadMatchingInput(ref userOption, new string[] { "a", "s", "m", "d" });

switch (userOption.ToLower())
{
    case "a":
        Console.WriteLine($"Your result: {firstNumber} + {secondNumber} = {firstNumber + secondNumber}");
        break;
    case "s":
        Console.WriteLine($"Your result: {firstNumber} - {secondNumber} = {firstNumber - secondNumber}");
        break;
    case "m":
        Console.WriteLine($"Your result: {firstNumber} * {secondNumber} = {firstNumber * secondNumber}");
        break;
    case "d":
        if (secondNumber == 0)
        {
            ReadNumericInput(ref secondNumber, "second", true);
        }
        Console.WriteLine($"Your result: {firstNumber} / {secondNumber} = {firstNumber / secondNumber}");
        break;
}

Console.Write("\nThank you for using this application! Press any key to exit: ");
Console.ReadKey();

void ReadNumericInput(ref double number, string ordinalString, bool divisor = false)
{
    bool checkNumber = true;
    while (checkNumber)
    {
        string? userInput = Console.ReadLine();
        ordinalString = ordinalString == "" ? ordinalString : ordinalString + " ";

        if (string.IsNullOrEmpty(userInput))
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write($"\r{new string(' ', Console.BufferWidth)}");
            Console.Write($"\rYour input was empty. Please try again- insert your {ordinalString}number and press ENTER: ");
            continue;
        }
        if (!double.TryParse(userInput, out number))
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write($"\r{new string(' ', Console.BufferWidth)}");
            Console.Write($"\rYour input must not include letters or symbols. Please try again- insert your {ordinalString}number and press ENTER: ");
            continue;
        }
        if (divisor && number == 0)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write($"\r{new string(' ', Console.BufferWidth)}");
            Console.Write($"\rYour divisor must not equal 0. Please try again- reinsert your {ordinalString}number and press ENTER: ");
            continue;
        }
        checkNumber = false;
    }
}
void ReadMatchingInput(ref string? letter, string[] options)
{
    bool checkNumber = true;
    while (checkNumber)
    {
        string? userInput = Console.ReadLine();

        if (string.IsNullOrEmpty(userInput))
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write($"\r{new string(' ', Console.BufferWidth)}");
            Console.Write($"\rYour input was empty. Please try to select your option again and press ENTER: ");
            continue;
        }
        foreach (string option in options)
        {
            if (option == userInput.ToLower())
            {
                checkNumber = false;
                letter = userInput;
                break;
            }
        }
        if (checkNumber)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write($"\r{new string(' ', Console.BufferWidth)}");
            Console.Write($"\rYour selection doesn't match any of the options above. Please try to select your option again and press ENTER: ");
        }
    }
}
void AskForNumber(string ordinalString)
{
    Console.Write($"Please type in your {ordinalString} number, and then press ENTER: ");
}