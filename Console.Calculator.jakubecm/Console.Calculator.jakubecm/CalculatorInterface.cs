using CalculatorLibrary;

public class CalculatorInterface
{
    /// <summary>
    /// Method for displaying the calc menu.
    /// </summary>
    public static void DisplayMenu()
    {
        Console.WriteLine("Choose an operation from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.WriteLine("\troot - Nth root (y root of x)");
        Console.WriteLine("\tpow - Power (x to the power of y)");
        Console.WriteLine("\tx10 - Times 10");
        Console.WriteLine("\tsin - Sinus");
        Console.WriteLine("\tcos - Cosinus");
        Console.WriteLine("\ttg - Tangens");
        Console.WriteLine("\tcotg - Cotangens\n");
    }

    /// <summary>
    /// Method for parsing either one or both calculation operands depending on the parameter requiresTwo.
    /// </summary>
    /// <param name="memoryEmpty">Boolean that tells the function whether the recent calculations memory is empty</param>
    /// <param name="calcInstance">Instance of the Calculator class</param>
    /// <param name="requiresTwo">Boolean that tells the function whether two operands are required</param>
    /// <returns>An array of one or two operands</returns>
    public static double[] ParseOperands(bool memoryEmpty, Calculator calcInstance, bool requiresTwo)
    {
        double[] array = new double[2];
        int neededOperands = requiresTwo ? 2 : 1;

        for (int i = 0; i < neededOperands; i++)
        {
            double cleanNum = 0;
            Console.Write("Type a number, or M to select result from memory (if not empty), and then press Enter: ");
            string? numInput = Console.ReadLine();

            if (numInput!.ToLower() == "m" && !memoryEmpty)
            {
                cleanNum = calcInstance.SelectResultFromMemory();
            }
            else
            {
                while (!double.TryParse(numInput, out cleanNum))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput = Console.ReadLine();
                }
            }

            array[i] = cleanNum;
        }

        return array;
    }
}

