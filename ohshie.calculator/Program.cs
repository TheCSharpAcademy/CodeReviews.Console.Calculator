using ohshie.calculator;

class Program
{
    public static int NumberOfCalculations = 0;
    private static bool _closeCalculator;
    
    public static void Main(string[] args)
    {
        Calculator calculator = new Calculator();
        do
        {
            Console.Clear();
            Console.WriteLine("Calculator app!\n" +
                              "1. Press 1 to perform calculations\n" +
                              "2. Press 2 to view previous equations\n" +
                              "3. Press 3 to exit");
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                    calculator.CalculatorApp();
                    break;
                
                case ConsoleKey.D2:
                {
                    Log.ReadLog();
                    Console.WriteLine("Press enter to go back\n" +
                                      "If you want to reset previous equations press X");
                    var userChoice = Console.ReadLine().ToLowerInvariant();
                    if (userChoice == "x")
                    {
                        Log.FlushLog();
                        NumberOfCalculations = 0;
                    }
                    break;  
                }
                
                case ConsoleKey.D3:
                    _closeCalculator = true;
                    break;
                default:
                {
                    Console.WriteLine("You've chosen poorly. Try again!\n" +
                                      "Press enter to repeat.");
                    Console.ReadLine();
                    break;
                }
            }
        } while (_closeCalculator == false);
    }
}


