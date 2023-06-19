using CalculatorProgram;

namespace CalculatorProgram;

public class Program
{
    static void Main(string[] args)
    {
        Calculator calculator = new();
        bool endApp = false;
        int attempts = 0;


        while (!endApp)
        {
            int userDigit;
            double numInput = 0;
            double result;
            string? userCommand;
            bool loopList = false;
            List<double> userListDigit = new();

            Console.Clear();
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("\n------------------------\n");

            while (!loopList && calculator.CountResultsList() > 0)
            {
                Console.WriteLine($"\nYour list of past results: [ {calculator.ResultsList}]");

                Console.Write("Do you want to clear it? (y/n): ");

                userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case "y":
                        calculator.ClearResult();
                        loopList = true;
                        break;
                    case "n":
                        Console.Write("\nDo you want to use previous result from the list\n" +
                            "It will be your first digit (y/n): ");

                        userCommand = Console.ReadLine();

                        switch (userCommand)
                        {
                            case "y":
                                userListDigit.Add(calculator.Results[0]);
                                loopList = true;
                                break;
                            case "n":
                                loopList = true;
                                break;
                            default:
                                Console.Write("Invalid input. Please try again.\n ");
                                break;
                        }
                        break;
                    default:
                        Console.Write("Invalid input. Please try again.\n");
                        break;
                }
            }
            Console.Write("\nHow many digit do you want to enter: ");

            userCommand = Console.ReadLine();

            userDigit = calculator.CheckUserDigitInputINT(userCommand);

            while (userDigit <= 0)
            {
                Console.Write("\nCannot be a negative number or ZERO!.");
                userCommand = Console.ReadLine();
                userDigit = calculator.CheckUserDigitInputINT(userCommand);

            }
            if (userDigit == 1 && userListDigit.Count() < 1)
            {
                Console.Write("\nYour 1 digit: ");
                userCommand = Console.ReadLine();
                numInput = calculator.CheckUserDigitInputDOUBLE(userCommand);
                Console.WriteLine("------------------------");
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\t1 - Square root");
                Console.WriteLine("\t2 - Power of two");
                Console.WriteLine("\t3 - Trigonometry Sin()");
                Console.WriteLine("\t4 - Trigonometry Cos()");
                Console.WriteLine("\t5 - Trigonometry Tan()");
            }
            else
            {
                for (int i = 0; i < userDigit; i++)
                {
                    Console.WriteLine();
                    if (userListDigit.Count() > 0)
                    {
                        i = userListDigit.Count();
                        Console.WriteLine($"Your 1 digit is: {userListDigit[0]}");
                    }
                    Console.Write($"Your {i + 1} digit: ");
                    userCommand = Console.ReadLine();
                    numInput = calculator.CheckUserDigitInputDOUBLE(userCommand);
                    userListDigit.Add(numInput);
                }
                Console.WriteLine("------------------------");
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\t1 - Sum");
                Console.WriteLine("\t2 - Difference");
                Console.WriteLine("\t3 - Product");
                Console.WriteLine("\t4 - To the power of (only for 2 digits)");
                Console.WriteLine("\t5 - Quotient");
            }
            Console.Write("Your option: ");
            try
            {
                //while (double.IsNaN(result))
                if (userListDigit.Count > 1)
                {
                    result = calculator.DoOperationWithTwoOrMany(userListDigit);
                }
                else
                {
                    result = calculator.DoOperationWithOne(numInput);
                }
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error. TERMINATED\n");
                }
                else
                {
                    Console.WriteLine("------------------------\n");
                    Console.WriteLine("Your result: {0:0.##}", result);

                    calculator.StoreResult(result);
                    attempts += 1;

                    if (calculator.CountResultsList() > 0)
                    {
                        Console.WriteLine($"\nYour list of past results: [ {calculator.ResultsList}]\n");
                    }
                    Console.WriteLine($"Amount of times calculator was used: {attempts.ToString()}.\n");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            Console.WriteLine("------------------------");
            Console.Write("Press '0' and Enter to close the app, or press Enter continue. . .");
            if (Console.ReadLine() == "0") endApp = true;
            Console.WriteLine("\n");
        }
        return;
    }
}