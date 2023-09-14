using CalculatorLibrary;

Console.WriteLine("Console Calculator in C#\n");
Console.WriteLine("------------------------\n");

Calculator calculator = new();

bool endApp = false;

double result;
double operand1;
double operand2;

while (!endApp)
{
    Console.Clear();
    Console.WriteLine(@"Choose an option from the following list:
a - Add
s - Subtract
m - Multiply
d - Divide
p - Power
r - Square Root
l - sinus
k - cosinus
h - Calculation History
u - Delete Calculation History
n - Exit
");
    Console.WriteLine();
    Console.Write("Your option? ");

    var operation = Console.ReadLine()?.Trim()?.ToLower();

    try
    {
        switch (operation)
        {
            case "u":
                Console.Clear();
                Console.WriteLine(@"Are you sure you want to delete your calculations history?
y - yes
n - no
");
                var confirmation = Console.ReadLine()?.ToLower();

                while (string.IsNullOrEmpty(confirmation) && confirmation != "y" && confirmation != "n")
                {
                    Console.Write("\nPlease choose a valid option! ");
                    confirmation = Console.ReadLine()?.ToLower();
                }

                if (confirmation == "y")
                {
                    Console.WriteLine("\nYou chose to delete your history. ");
                    calculator.ClearHistory();
                }
                else
                {
                    Console.WriteLine("\nYou chose to not delete your history. ");
                }

                Console.WriteLine("\nPress any key to return to main menu.");

                Console.ReadLine();

                continue;
            case "n":
                endApp = true;
                continue;
            case "h":
                Console.Clear();
                Console.WriteLine("History\n");
                Console.WriteLine("------------------------\n");

                if (calculator.HasHistory()) calculator.PrintHistory();
                else Console.WriteLine("No history found. Please make some calculations and come back!");

                Console.WriteLine("\n------------------------\n");

                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");

                if (Console.ReadLine()?.ToLower() == "n") endApp = true;

                continue;
            case "a":
            case "s":
            case "m":
            case "d":
            case "p":
                Console.Write("Type a number, and then press Enter: ");
                operand1 = Helpers.GetNumber();

                Console.Write("Type another number, and then press Enter: ");
                operand2 = Helpers.GetNumber();

                result = calculator.BinaryOperation(operand1, operand2, operation);
                break;
            case "l":
            case "k":
            case "r":
                Console.Write("Type your number, and then press Enter: ");

                double num = Helpers.GetNumber();

                result = calculator.UnaryOperation(num, operation);
                break;
            default:
                Console.WriteLine("Invalid Operation. Press any key to try again.");
                Console.ReadLine();
                continue;
        }

        if (double.IsNaN(result))
        {
            Console.WriteLine("This operation will result in a mathematical error.\n");
        }
        else Console.WriteLine("Your result: {0:0.##}\n", result);
    }
    catch (Exception e)
    {
        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
    }

    Console.WriteLine("------------------------\n");

    Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");

    if (Console.ReadLine()?.ToLower() == "n") endApp = true;

    Console.WriteLine("\n"); // Friendly linespacing.
}

Console.WriteLine($"Congratulations! You used the calculator app {calculator.NumberOfCalculations} times.");
calculator.Finish();