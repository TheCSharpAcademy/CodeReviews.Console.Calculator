namespace CalculatorLibrary;

public class HelpersLibrary
{
    public static string ValidateYesOrNoInput(string userInput, string message)
    {
        while (userInput.ToLower().Trim() != "y" && userInput.ToLower().Trim() != "n")
        {
            Console.WriteLine(message);
            userInput = Console.ReadLine();
        }
        return userInput;
    }

    public static int ValidateIndex(string numIndex, List<double> history)
    {
        int cleanIndex = 0;
        while (!int.TryParse(numIndex, out cleanIndex) || cleanIndex <= 0 || cleanIndex > history.Count)
        {
            Console.Write("This is not a valid input. Please enter an integer value within the list range: ");
            numIndex = Console.ReadLine();
        }
        return cleanIndex;
    }

    public static double ValidateNumberInput(string numInput)
    {
        double cleanNum = 0;
        while (!double.TryParse(numInput, out cleanNum))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            numInput = Console.ReadLine();
        }
        return cleanNum;
    }
}

