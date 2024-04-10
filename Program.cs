int numberOfUse = 0;

bool isDone = false;
bool goBack = false;
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

    goBack = false;
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
    Console.Clear();
    Console.WriteLine("Basic Calculations");
    Console.WriteLine("Enter an expression(ex. 1 x 1). Type 'back' to go back to the main menu:");

    do
    {
        readInput = Console.ReadLine();

        if (readInput != null)
        {
            if (readInput.ToLower() != "back")
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
            else
            {
                goBack = true;
            }
        }

    } while (goBack == false);

}

void SquareRoot()
{
    Console.Clear();
    Console.WriteLine("Square Root");
    Console.WriteLine("Enter a number to square root. Type 'back' to go back to the main menu:");

    do
    {
        readInput = Console.ReadLine();

        if (readInput != null)
        {
            if (readInput.ToLower() != "back")
            {
                validInput = int.TryParse(readInput, out num);

                if (validInput)
                {
                    Console.WriteLine(Math.Sqrt(num));
                }
            }
            else
            {
                goBack = true;
            }
        }

    } while (goBack == false);
}

void Power()
{
    double result = 0;

    Console.Clear();
    Console.WriteLine("Power");
    Console.WriteLine("Enter the exponential expression(ex. 2 ^ 4). Type 'back' to go back to the main menu: ");

    do
    {
        readInput = Console.ReadLine();

        if (readInput != null)
        {
            if (readInput.ToLower() != "back")
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
            else
            {
                goBack = true;
            }
        }

    } while (goBack == false);

}

void TenX()
{
    Console.Clear();
    Console.WriteLine("10x");
    Console.WriteLine("Enter a number to 10x. Type 'back' to go back to the main menu:");

    do
    {
        readInput = Console.ReadLine();

        if (readInput != null)
        {
            if (readInput.ToLower() != "back")
            {
                validInput = long.TryParse(readInput, out long result);

                if (validInput)
                {
                    Console.WriteLine($"{result * 10}");
                }
            }
            else
            {
                goBack = true;
            }
        }

    } while (goBack == false);
}

void Trigonometry()
{
    int a, b, c;
    Console.Clear();
    Console.WriteLine("Trigonometry");
    Console.WriteLine("Enter the sides of the right triangle, respectively as a, b, c (ex. 3, 4, 5). Type 'back' to go back to the main menu:");

    do
    {
        readInput = Console.ReadLine();

        if (readInput != null)
        {
            if (readInput.ToLower() != "back")
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
                        Console.WriteLine("Please enter a number.");
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
            else
            {
                goBack = true;
            }
        }

    } while (goBack == false);
}