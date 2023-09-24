namespace CalculatorProgram
{
    internal static class Helpers
    {
        internal static List<Calculations> latestCalculations = new();
        internal static void ShowTimesUsed(int timesUsed)
        {
            if (timesUsed == 1)
            {
                Console.WriteLine("Calculator has been used 1 time.");
            }
            else
            {
                Console.WriteLine($"Calculator has been used {timesUsed} times");
            }
        }
        
        internal static void AddToHistory(double first, double second, string operation)
        {
            string convertedOperation = ""; 

            switch (operation)
            {
                case "a":
                    convertedOperation = "+";
                    break;
                case "s":
                    convertedOperation = "-";
                    break;
                case "m":
                    convertedOperation = "*";
                    break;
                case "d":
                    convertedOperation = "/";
                    break;
                default:
                    Console.WriteLine("Something went wrong while converting operation");
                    break;
            }
            latestCalculations.Add(new Calculations
            {
                FirstOperand = first,
                SecondOperand = second,
                Operation = convertedOperation
            });
        }

        internal static void ShowHistory ()
        {
            Console.Clear();
            Console.WriteLine("Previous Calculations");
            Console.WriteLine("------------------------\n");

            int calculationCounter = 1;

            foreach (Calculations calculations in latestCalculations)
            {
                Console.WriteLine($"Calculation {calculationCounter }: {calculations.FirstOperand} {calculations.Operation} {calculations.SecondOperand} = {calculations.Result}");
                calculationCounter++;
            }
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
            Console.Clear();
        }

        internal static void DeleteHistory()
        {
            latestCalculations.Clear();
        }
    }
}
