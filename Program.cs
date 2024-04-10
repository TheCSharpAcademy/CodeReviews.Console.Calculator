int numberOfUse = 0;

bool isDone = false;
bool useAgain = true;
bool validInput = false;

int num = 0;
char operation = ' ';
string expression;

bool firstTermFound = false;

string? readInput;

do
{
    Console.Clear();
    Console.WriteLine($"Calculator - Number of times used: {numberOfUse}");
    Console.WriteLine("Enter what type of mathematical functions you want to do. Type 'exit' to close the program:");
    Console.WriteLine("1. Basic Calculations ( x / + - )");
    Console.WriteLine("2. Square Root");
    Console.WriteLine("3. Power");
    Console.WriteLine("4. 10x");
    Console.WriteLine("5. Trigonometry");
    Console.WriteLine();

    readInput = Console.ReadLine();

    if (readInput != null)
    {
        if (readInput.ToLower() == "exit")
        {
            isDone = true;
        }

        validInput = int.TryParse(readInput, out int chosenFunction);

        if (validInput)
        {
            switch (chosenFunction)
            {
                case 1:
                    BasicCalculations();
                    break;
                case 2:
                    SquareRoot();
                    break;
                case 3:
                    Power();
                    break;
                case 4:
                    TenX();
                    break;
                case 5:
                    Trigonometry();
                    break;
            }
        }


    }



    numberOfUse++;
} while (isDone == false);

void BasicCalculations()
{
    do
    {
        Console.Clear();
        Console.WriteLine("Basic Calculations");
        Console.WriteLine("Enter an expression(ex. 1 x 1):");

        readInput = Console.ReadLine();

        if (readInput != null)
        {
            expression = readInput;

            string[] expressionArray = expression.Split(' ');
            int expressionLength = expressionArray.Length;
            expression = "";

            for (int i = 0; i < expressionLength; i++)
            {
                char[] terms = expressionArray[i].ToCharArray();

                foreach (char term in terms)
                {
                    if (term == 'x' || term == '/' || term == '+' || term == '-')
                    {
                        operation = term;
                        firstTermFound = true;
                        num = int.Parse(expression);
                        expression = "";
                    }
                    else
                    {
                        if (firstTermFound == false)
                        {
                            expression += term;
                        }
                        else
                        {
                            expression += term;
                        }
                    }
                }
            }

            switch (operation)
            {
                case 'x':
                    num *= int.Parse(expression);
                    break;

                case '/':
                    num /= int.Parse(expression);
                    break;

                case '+':
                    num += int.Parse(expression);
                    break;

                case '-':
                    num -= int.Parse(expression);
                    break;
            }

            Console.WriteLine(num);
        }

        Console.WriteLine();
        Console.WriteLine("Do you want to use this again? Type 'yes' to use again. Type 'no' to return to the main menu.");
        readInput = Console.ReadLine();

        if (readInput != null)
        {
            string choice = readInput.ToLower();

            if (choice == "yes" || choice == "y")
            {
                useAgain = true;
            }
            else if (choice == "no" || choice == "n")
            {
                useAgain = false;
            }
        }

    } while (useAgain != false);

}

void SquareRoot()
{
    do
    {
        Console.Clear();
        Console.WriteLine("Square Root");
        Console.WriteLine("Enter a number to square root:");

        readInput = Console.ReadLine();

        if (readInput != null)
        {
            validInput = int.TryParse(readInput, out num);

            if (validInput)
            {
                Console.WriteLine(Math.Sqrt(num));
            }

        }

        Console.WriteLine();
        Console.WriteLine("Do you want to use this again? Type 'yes' to use again. Type 'no' to return to the main menu.");
        readInput = Console.ReadLine();

        if (readInput != null)
        {
            string choice = readInput.ToLower();

            if (choice == "yes" || choice == "y")
            {
                useAgain = true;
            }
            else if (choice == "no" || choice == "n")
            {
                useAgain = false;
            }
        }

    } while (useAgain != false);
}

void Power()
{
    double result = 0;

    do
    {
        Console.Clear();
        Console.WriteLine("Power");
        Console.WriteLine("Enter the exponential expression(ex. 2 ^ 4): ");

        readInput = Console.ReadLine();

        if (readInput != null)
        {
            expression = readInput;

            string[] expressionArray = expression.Split(' ');
            int expressionLength = expressionArray.Length;
            expression = "";

            for (int i = 0; i < expressionLength; i++)
            {
                char[] terms = expressionArray[i].ToCharArray();

                foreach (char term in terms)
                {
                    if (term == '^')
                    {
                        operation = term;
                        firstTermFound = true;
                        num = int.Parse(expression);
                        expression = "";
                    }
                    else
                    {
                        if (firstTermFound == false)
                        {
                            expression += term;
                        }
                        else
                        {
                            expression += term;
                        }
                    }
                }
            }

            switch (operation)
            {
                case '^':
                    result = Math.Pow(num, double.Parse(expression));
                    break;
                default:
                    Console.WriteLine("Enter a valid expression!");
                    break;
            }

            Console.WriteLine(result);
        }

        Console.WriteLine();
        Console.WriteLine("Do you want to use this again? Type 'yes' to use again. Type 'no' to return to the main menu.");
        readInput = Console.ReadLine();

        if (readInput != null)
        {
            string choice = readInput.ToLower();

            if (choice == "yes" || choice == "y")
            {
                useAgain = true;
            }
            else if (choice == "no" || choice == "n")
            {
                useAgain = false;
            }
        }

    } while (useAgain != false);

}

void TenX()
{
    do
    {
        Console.Clear();
        Console.WriteLine("10x");
        Console.WriteLine("Enter a number to 10x:");

        readInput = Console.ReadLine();

        if (readInput != null)
        {
            validInput = long.TryParse(readInput, out long result);

            if (validInput)
            {
                Console.WriteLine($"{result * 10}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Do you want to use this again? Type 'yes' to use again. Type 'no' to return to the main menu.");
        readInput = Console.ReadLine();

        if (readInput != null)
        {
            string choice = readInput.ToLower();

            if (choice == "yes" || choice == "y")
            {
                useAgain = true;
            }
            else if (choice == "no" || choice == "n")
            {
                useAgain = false;
            }
        }

    } while (useAgain != false);
}

void Trigonometry()
{
    int a, b, c;

    do
    {
        Console.Clear();
        Console.WriteLine("Trigonometry");
        Console.WriteLine("Enter the sides of the right triangle, respectively as a, b, c (ex. 3, 4, 5)");

        readInput = Console.ReadLine();

        if (readInput != null)
        {
            expression = readInput;

            string[] tempArray = expression.Split(",");
            int[] array = new int[tempArray.Length];

            for (int i = 0; i < tempArray.Length; i++)
            {
                string temp = tempArray[i].Trim();

                if (int.TryParse(temp, out array[i]))
                {

                }
                else
                {
                    Console.WriteLine("Failed to parse.");
                }

            }

            Array.Sort(array);

            if (array.Length == 3)
            {
                a = array[0];
                b = array[1];
                c = array[2];


                if (a + b > c && a + c > b && b + c > a)
                {
                    Console.WriteLine($"sin: {(double)a / c}");
                    Console.WriteLine($"cos: {(double)b / c}");
                    Console.WriteLine($"tan: {(double)a / b}");
                }
                else
                {
                    Console.WriteLine("Error: The length of any side must be less than the sum of the lengths of the other two sides.");
                }
            }

        }

        Console.WriteLine();
        Console.WriteLine("Do you want to use this again? Type 'yes' to use again. Type 'no' to return to the main menu.");
        readInput = Console.ReadLine();

        if (readInput != null)
        {
            string choice = readInput.ToLower();

            if (choice == "yes" || choice == "y")
            {
                useAgain = true;
            }
            else if (choice == "no" || choice == "n")
            {
                useAgain = false;
            }
        }

    } while (useAgain != false);
}