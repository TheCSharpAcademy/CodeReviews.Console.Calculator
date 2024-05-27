public class UserInteractions
{
    public void Intro()
    {
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
    }

    public string? CalculationsOptions()
    {
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\td - Delete current calculations & start new one"); 
        Console.WriteLine("\tp - Use previous calculations"); 
        Console.WriteLine("\tAny other key - New calculation"); 

        Console.Write("Your option? ");

        return Console.ReadLine();
    }

    public string? ChooseOperation()
    {
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");

        Console.Write("Your option? ");

        return Console.ReadLine();
    }

    public double GetNumInput(string? num)
    {
        double cleanNum = 0;
        while (!double.TryParse(num, out cleanNum))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            num = Console.ReadLine();
        }
        return cleanNum;
    } 

    public void InvalidInput() => Console.WriteLine("Error: Unrecognized input or mathematical error.");

    public void TimesRan(int count) => Console.WriteLine($"Calculator has been used {count} times this session.");

    public void EndOptions()
    {
        Console.WriteLine("------------------------\n");
        Console.WriteLine("\tn - Close the app");
        Console.WriteLine("\tAny other key to continue"); 

        if (Console.ReadLine() == "n") Environment.Exit(0);
    }

    public void DisplayCalculations(List<Calculations> calculationsList)
    {
        for (int i = 0; i < calculationsList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {calculationsList[i]}");
        }
    }

    public int SelectCalculation(List<Calculations> calculationsList)
    {
        string? option1 = Console.ReadLine();
        int result = 0;

        while (!int.TryParse(option1, out result) || result <= 0 || result > calculationsList.Count)
        {
            InvalidInput();
            Console.WriteLine("Type a number from the list above:");
            option1 = Console.ReadLine();
        }

        return result - 1;
    }

    public void ClearCalculations(List<Calculations> calculationsList) => calculationsList.Clear();
}