using Calculator.saroua;

class Program
{
    static void Main(string[] args)
    {
        int runAmount = 0;
        double reuseResult = 0;
        bool reuseResultBool = false;
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        while (!endApp)
        {
            // Diplay the title at every new calculation
            if (runAmount != 0)
            {
                Console.WriteLine("Console Calculator in C#\r");
                Console.WriteLine("------------------------\n");
            }
            // Declare variables and set to empty.
            string numInput1 = "";
            string numInput2 = "";
            double result = 0;

            // Ask the user to type the first number.
            if(reuseResultBool == false)
            {
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();
            }
            else
            {
                numInput1 = $"{reuseResult}";
            }

            double cleanNum1 = 0;
            if (reuseResultBool == false)
            {
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }
            }
            else
            {
                cleanNum1 = reuseResult;
                reuseResultBool = false;
            }

            // Ask the user to type the second number.
            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput2 = Console.ReadLine();
            }

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tp - Power");
            Console.Write("Your option? ");

            string op = Console.ReadLine();
            try
            {
                result = Calculatore.DoOperation(cleanNum1, cleanNum2, op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else
                {
                    //Increments the total of use of the calculator
                    helpers.AddToCalculation(cleanNum1, cleanNum2, op, result);
                    runAmount = helpers.IncrementTotalUse(runAmount);
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }


            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'v' to see your calcultation history\nPress 'T' to see how much you've used the calculator\nPress 'n' to close the app\nPress any other key and Enter to continue: ");
            string userAnswer = Console.ReadLine().ToLower();
            switch (userAnswer)
            {
                case "n":
                    endApp = true;
                    break;
                case "v":
                    Console.Clear();
                    Console.WriteLine("Previous calculations:");
                    helpers.showPastCalculation();
                    Console.WriteLine("\nPress enter to continue or enter the number of the calculation to reuse it's result:");
                    string calculationReused = Console.ReadLine();
                    if (string.IsNullOrEmpty(calculationReused))
                    {
                        break;
                    }
                    else
                    {
                        int calculationReused2 = Int32.Parse(calculationReused);
                        reuseResult = helpers.calculation[calculationReused2 - 1].Result;
                        reuseResultBool = true;
                    }
                    Console.Clear();
                    break;
                case "t":
                    Console.Clear();
                    Console.WriteLine("Amount of time you've used the calculator:");
                    Console.WriteLine(runAmount);
                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    break;
            }

            //Console.WriteLine("\n"); // Friendly linespacing.
        }
        return;
    }
}