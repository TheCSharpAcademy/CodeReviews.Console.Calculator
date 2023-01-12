
namespace Calculator.Lonchanick
{
    public class Program
    {
        static void Execute()
        {
            LogClass logClass = new LogClass();
            bool flag = false;
            Calculator calc = new Calculator();
            while (!flag)
            {
                Console.Clear();
                // Declare variables and then initialize to zero.
                double num1 = 0; double num2 = 0;
                // Display title as the C# console calculator app.
                Console.WriteLine("Console Calculator in C#\r");
                Console.WriteLine("------------------------\n");
                // Ask the user to choose an option.
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tl - See log of operations");
                Console.WriteLine("\tx - Exit");
                Console.Write("Your option? ");
                string op = ToolBox.GetValidOption();
                if(op=="x")
                    System.Environment.Exit(1);
                if (op!="l")
                {
                    Console.Clear();
                    // Ask the user to type the first number.
                    Console.WriteLine("Type a number, and then press Enter");
                    num1 = ToolBox.GetInputDouble();

                    // Ask the user to type the second number.
                    Console.WriteLine("Type another number, and then press Enter");
                    num2 = ToolBox.GetInputDouble();
                }
                
                calc.DoOperation(num1, num2, op);
                Console.WriteLine("Press q to close app, or press any other key to continue.. ");
                if (Console.ReadLine() == "q") flag = true;
            }
            calc.CloseLog();
        }

        static void Main(string[] args)
        {
            //JsonSerializer();
            Execute();
        }
    }
}
