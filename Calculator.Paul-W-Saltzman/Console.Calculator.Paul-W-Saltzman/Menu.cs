namespace CalculatorProgram;

using CalculatorLibrary;
    internal class Menu
    {
        public static void ShowMenu(Calculator calculator)
        {
            bool valid = false;
            while (!valid)
            {
                Console.Clear();
                Console.WriteLine("Console Calculator in C#\r");
                Console.Write($@"This Calculator has been used ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($@"{calculator.NumberOfUses} ");
                Console.WriteLine($@"times.");
                Console.ResetColor();
                Console.WriteLine("------------------------\n");
               
                
                
                Console.WriteLine("Please Choose An Option");

                Console.WriteLine("\t1-Simple Math");
                Console.WriteLine("\t2-Advanced Functions");
                Console.WriteLine("\t3-List of Past Operations");
                Console.WriteLine("\t4-Exit Program");

            String MathType = Console.ReadLine();

                switch (MathType)
                {
                    case "1":
                        SimpleMath(calculator);
                        valid = true;
                    break;
                    case "2":
                        AdvancedOperations(calculator);
                        valid = true;
                    break;
                    case "3":
                        calculator.ListPastCalculations(calculator);
                    valid = true;
                    break;
                    case "4":
                        valid = true;
                        break;
                    default:
                        //keep the user in the loop till they enter a valid option
                        break;
                }
            }

        }
        public static void SimpleMath(Calculator calculator)
        {

        double cleanNum1 = Helpers.GetNumber(1);
        double cleanNum2 = Helpers.GetNumber(2);

        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.Write("Your option? ");

        string op = Console.ReadLine();

        Helpers.Result(cleanNum1, cleanNum2, op, calculator);
        

    }

    public static void AdvancedOperations(Calculator calculator)
        {
            bool valid = false;
            while (!valid)
            {
                Console.Clear();
                Console.WriteLine("What operation would you like to do?");
                Console.WriteLine("\tf-Factorial");
                Console.WriteLine("\tr-Find the Square Root");
                Console.WriteLine("\te-Exponent Calculator");
                string op = Console.ReadLine();

                switch (op)
                {
                    case "r":
                        double cleanNum = Helpers.GetNumber(1);
                        Helpers.Result (cleanNum,op, calculator);
                        valid = true;
                        break;
                    case "f":
                        cleanNum = Helpers.GetNumber(1);
                        Helpers.Result(cleanNum, op, calculator);
                        valid = true;
                    break;
                    case "e":
                        cleanNum = Helpers.GetNumber(1);
                        double cleanNum2 = Helpers.GetNumber(2);
                        Helpers.Result(cleanNum, cleanNum2, op, calculator);
                        valid = true;
                        break;
                default:
                        //keep the user in the loop till they enter a valid option
                        break;
                }
            }
        }
    
}