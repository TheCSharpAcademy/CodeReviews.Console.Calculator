int num = 0;
char operation = ' ';
string expression;

bool firstTermFound = false;

string? readInput;

Console.WriteLine("Calculator");
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
