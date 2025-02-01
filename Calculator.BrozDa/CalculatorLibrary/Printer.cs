namespace CalculatorLibrary
{
    /// <summary>
    /// Represent object used for majority of print operations for <see cref="Calculator"/>
    /// </summary>
    internal class Printer
    {
        /// <summary>
        /// Prints operations available for the <see cref="Calculator"/>
        /// </summary>
        public void PrintOperationSelectionMenu()
        {
            Console.WriteLine("Choose an operation from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("\tsr - Square Root");
            Console.WriteLine("\t10x - Multiply by 10");
            Console.WriteLine("\tsin - Sinus");
            Console.WriteLine("\tcos - Cosinus");
            Console.WriteLine("\ttg - tangent ");
            Console.WriteLine();
            Console.Write("Your input:");
        }
        /// <summary>
        /// Prints history of all calculations which were performed in current session
        /// </summary>
        /// <param name="calculationHistory"></param>
        public void PrintHistory(List<string> calculationHistory)
        {
            Console.WriteLine("Calculation history:");
            if (calculationHistory == null || calculationHistory.Count == 0)
            {
                Console.WriteLine("No records in history");
            }
            else
            {
                for (int i = 0; i < calculationHistory.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {calculationHistory[i]}");
                }
            }
            Console.WriteLine("------------------------");
        }
        /// <summary>
        /// Prints title of the <see cref="Calculator"/> application
        /// </summary>
        /// <param name="numberOfUses"><see cref="int"/> representing number of calculations performed by the application</param>
        public void PrintTitle(int numberOfUses)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Welcome to Console Calculator in C#\r");
            Console.WriteLine($"Calculator was used to solve {numberOfUses} problems so far");
            Console.WriteLine("------------------------");
        }
        /// <summary>
        /// Prints menu containing options
        /// </summary>
        /// <param name="numberOfUses"><see cref="int"/> representing number of calculations performed by the application</param>
        public void PrintOptionsMenu(int numberOfUses)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            PrintTitle(numberOfUses);
            Console.WriteLine("Press coresponding number and [Enter] for selection: ");
            Console.WriteLine("1. Show History");
            Console.WriteLine("2. Delete History");
            Console.WriteLine("3. Return to calculations");
            Console.WriteLine("------------------------");
        }
    }
}
