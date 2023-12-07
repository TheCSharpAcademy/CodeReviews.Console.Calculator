using CalculatorLibrary;
namespace CalculatorProgram
{
    class Program
    {
        internal static void Main(string[] args)
        {
            int timesUsed = 0;
            bool endApp = false;

            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("--------------------------");

            Calculator calculator = new Calculator();
            while (!endApp)
            {

                string numInput1 = "";
                string numInput2 = "";
                double result = 0;


                Console.WriteLine("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();
                
                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.WriteLine("This is not a valid input. Please enter an integer: ");
                    numInput1 = Console.ReadLine();
                }

                Console.WriteLine("Choose an operator from the following list: ");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tr - Square Root");
                Console.WriteLine("\tp - Power");
                Console.WriteLine("\t10x - 10x");
                Console.WriteLine("\tsin - Sin");
                Console.WriteLine("\ttan - Tan");
                Console.WriteLine("\tcos - Cos");
                Console.WriteLine("Your option?: ");

                string op = Console.ReadLine();

                if (op == "a" || op == "s" || op == "m" || op == "d" || op == "p")
                {
                    Console.WriteLine("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();

                    double cleanNum2 = 0;
                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.WriteLine("This is not a valid input. Please enter an integer: ");
                        numInput2 = Console.ReadLine();
                    }
                    try
                    {
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error. \n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the maths.\n - Details: " + e.Message);
                    }
                } else if (op == "r" || op == "10x" || op == "sin" || op == "tan" || op == "cos")
                {
                    try
                    {
                    result = calculator.SingleDigitOp(cleanNum1, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error. \n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    catch (Exception e)
                    {
                    Console.WriteLine("Oh no! An exception occurred trying to do the maths.\n - Details: " + e.Message);
                    } 
                } else
                {
                    Console.WriteLine("NO!");
                }

                Console.WriteLine("------------------------");

                timesUsed++;
                Console.WriteLine($"Calculator has been used {timesUsed} times in this session.");
                Console.WriteLine("History: ");
                foreach (var sum in calculator.sums)
                {
                    Console.WriteLine(sum);
                }

                Console.WriteLine("------------------------");

                Console.WriteLine("Would you like to clear your recent sums? (y/n): ");
                if (Console.ReadLine().ToLower() == "y") calculator.sums.Clear();

                Console.WriteLine("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n");
            }
            calculator.Finish();
            return;
        }
    }
}