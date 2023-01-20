namespace didntreally_ConsoleCalculator;
class Program
{
    static void Main(string[] args)
    {
        // Declare variables and then initialize to zero.
        string num1;
        string num2;
        int usageNum = 0;
        List<double> calculations = new List<double>();

        Console.WriteLine("Welcome to the console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
        bool runCalculator = true;

        while (runCalculator)
        {
            Console.WriteLine("Type a number and then press Enter! ");
            num1 = Console.ReadLine();
            double numInput1 = 0;
            while (!double.TryParse(num1, out numInput1))
            {
                Console.WriteLine("This is not a valid number, please enter a number! ");
                num1 = Console.ReadLine();
            }

            Console.WriteLine("Type a second number, and then press Enter! ");
            num2 = Console.ReadLine();
            double numInput2 = 0;
            while (!double.TryParse(num2, out numInput2))
            {
                Console.WriteLine("This is not a valid number, please enter a number! ");
                num2 = Console.ReadLine();
            }

            Console.WriteLine("Choose an option from the list and type in the alphabet ");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - divide");
            Console.WriteLine("What is your option: ");
            string userInput = Console.ReadLine().ToUpper();
            double ifInputIsInt = 0;
            String[] calculatorOperations = { "A", "S", "M", "D" };
            String[] yesOrNo = { "Y", "N" };

            while (String.IsNullOrWhiteSpace(userInput) || double.TryParse(userInput, out ifInputIsInt) || (!calculatorOperations.Contains(userInput)))
            {
                Console.WriteLine("Please enter a valid alphabet!");
                userInput = Console.ReadLine().ToUpper();
            }

            double result = Calculator.Operation(numInput1, numInput2, userInput);

            Console.WriteLine($"The result is: {result}");
            usageNum += 1;
            calculations.Add(result);
            for (int x = 0; x < calculations.Count(); x++)
            {
                Console.WriteLine($"Your past results are Number {x + 1} : {calculations[x]}");
            }

            // Wait for the user to respond before closing.
            Console.WriteLine("To exit the Calculator, type exit");
            Console.WriteLine("To edit the list of past calculations, type edit");
            Console.WriteLine("Or press any other key to start again!");
            string calculatorSwitch = Console.ReadLine();
            if (calculatorSwitch == "exit")
            {
                runCalculator = false;
                Console.WriteLine("Thanks for using our Calculator!");
                Console.WriteLine($"Number of times calculator was used: {usageNum}");
            }
            else if (calculatorSwitch == "edit")
            {
                Console.WriteLine("Type in the roman numeral of the calculation you want to delete, eg. 1 or 2 ");
                string userString = Console.ReadLine();
                int userNum = 0;
                while (!int.TryParse(userString, out userNum))
                {
                    Console.WriteLine("This is not a valid number, please enter a number! ");
                    userString = Console.ReadLine();
                }

                calculations.RemoveAt(userNum - 1);

                if (calculations.Count() != 0)
                {
                    Console.WriteLine($"{userNum} has been removed, the new list is ");
                    for (int x = 0; x < calculations.Count(); x++)
                    {
                        Console.WriteLine($"Your past results are Number {x + 1} : {calculations[x]}");
                    }

                    Console.WriteLine("Do you still want to continue using calculator? Type y for yes / n for no");
                    string userContinue = Console.ReadLine().ToUpper();

                    while (String.IsNullOrWhiteSpace(userContinue) || double.TryParse(userContinue, out ifInputIsInt) || (!yesOrNo.Contains(userContinue)))
                    {

                        Console.WriteLine("Please enter a correct alphabet y/n");
                        userContinue = Console.ReadLine().ToUpper();
                    }

                    if (userContinue == "Y")
                    {
                        runCalculator = true;
                    }
                    else if (userContinue == "N")
                    {
                        runCalculator = false;
                        Console.WriteLine("Thanks for using our Calculator!");
                    }
                }
                else
                {
                    Console.WriteLine("The list is empty now!");
                    Console.WriteLine("Do you still want to continue using calculator? Type y for yes / n for no");
                    string userContinue2 = Console.ReadLine().ToUpper();

                    while (String.IsNullOrWhiteSpace(userContinue2) || double.TryParse(userContinue2, out ifInputIsInt) || !yesOrNo.Contains(userContinue2))
                    {
                        Console.WriteLine("Please enter a correct alphabet y/n");
                        userContinue2 = Console.ReadLine().ToUpper();
                    }

                    if (userContinue2.ToUpper() == "Y")
                    {
                        runCalculator = true;
                    }
                    else if (userContinue2.ToUpper() == "N")
                    {
                        runCalculator = false;
                        Console.WriteLine("Thanks for using our Calculator!");
                    }
                }
            }
            else
            {
                runCalculator = true;
            }
        }

    }
}