
List<char> operations = new List<char>();
string data = "asmd";
operations.AddRange(data);
// History, list of results and how many are stored currently
List<float> history = new List<float>();
int history_count = 0;
// Operands for the operations, separate ones for floating point numbers and user inputs
float operand_a = 0.0f;
float operand_b = 0.0f;
float result = 0.0f;
string user_input1 = "";
string user_input2 = "";
string user_input_menu = "";
string operation = "";
bool running = true;
bool checking_a = true;
bool running_history = true;


void Menu()
{
    Console.WriteLine(@"
[a]dd
[s]ubtract
[m]ultiply
or [d]ivide");

    // Validation of operations, only single characters in operations list will pass
    operation = Console.ReadLine();
    operation = operation.ToLower();

    while (!(operation.Length == 1 && operations.Contains(operation.First())))
    {
        if (operation.Length != 1)
        {
            Console.WriteLine("Please enter a single letter");
        }
        else
        {
            Console.WriteLine("Please enter a valid operation from the list");
        }
        operation = Console.ReadLine();
    }
}


// Validating the user input and assigning them to the correct operands

float ValidateInput(string str)
{
    if (str.Contains(","))
    {
        str = str.Replace(",", ".");
    }
        if (float.TryParse(str, out float tmp))
        {
            return tmp;
        }
        else
        {
            Console.WriteLine("The number you entered is invalid");
            return 0.0f;
        }
}

while (running)
{
    Menu();
    if (checking_a)
    {
        // Validating the first user input
        while (operand_a == 0)
        {
            Console.WriteLine("Enter the first number");
            user_input1 = Console.ReadLine();
            if (user_input1 == null)
            {
                continue;
            }
            operand_a = ValidateInput(user_input1);
        }
    }

    checking_a = true;
    // Validating the second user input
    while (operand_b == 0)
    {
        Console.WriteLine("Enter the second number");
        user_input2 = Console.ReadLine();

        // Check if the user wants to continue with 0
        if (user_input2 == "0")
        {
            Console.WriteLine("Are you sure you want to continue with 0?\n y/n");
            string check = Console.ReadLine();
            if (check.ToLower() == "n")
            {
                continue;
            }
        }

        // Validate the user input
        operand_b = ValidateInput(user_input2);
    }


    // Getting the result and displaying it, ADD THE RESULT TO HISTORY
    switch (operation)
    {
        case "a":   
            result = operand_a + operand_b;
            break;
        case "s":
            result = operand_a - operand_b;
            break;
        case "m":
            result = operand_a * operand_b;
            break;
        case "d":
            result = operand_a / operand_b;
            break;
    }

    Console.WriteLine(result);

    history.Add(result);
    operand_a = 0;
    operand_b = 0;
    history_count++;
    result = 0;

    while (user_input_menu != "y" && user_input_menu != "h" && user_input_menu != "q")
    {
        Console.WriteLine("If you want to continue enter y\n if you want to acces history enter h\n if you want to quit enter q");
        user_input_menu = Console.ReadLine();
    }
    if (user_input_menu == "q")
    {
        running = false;
    }
    else if (user_input_menu == "h")
    {
        Console.WriteLine("Chose the number you want to operate on by it's index,\n if you want to return to the menu enter c");
        {
            for (int i = 0; i < history.Count; i++)
            {
                Console.WriteLine($"{i}. {history[i]}");
            }
        }
        while (running_history)
        {
            user_input_menu = Console.ReadLine();
            if (user_input_menu.ToLower() == "c")
            {
                break;
            }
            else
            {
                try
                {
                    int i = Convert.ToInt32(user_input_menu);
                    Console.WriteLine($"You chose {history[i]}");
                    operand_a = history[i];
                    checking_a = false;
                    break;
                }
                catch (Exception ex) when (ex is FormatException || ex is ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Enter a valid entry");
                }
            }
        }
    }
}