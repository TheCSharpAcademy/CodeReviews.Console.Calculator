
using System.Text.RegularExpressions;

namespace CalculatorLibrary;

public class CalculationsList
{
  private static readonly List<Calculation> recentCalculations = [];
  public static void AddCalculationToList(double numOne, double numTwo, double answer, string operation)
  {
    string symbol = GetOperation(operation);
    Calculation newCalculation = new(numOne, numTwo, answer, symbol);
    recentCalculations.Add(newCalculation);
  }
  public static Calculation? ListMenuOptions()
  {
    DisplayList();
    Console.WriteLine("[C]hoose calculation from list");
    Console.WriteLine("[D]elete list");
    Console.WriteLine("[E]xit");
    string? op = Console.ReadLine();
    
    if (op == null || !Regex.IsMatch(op.ToUpper(), "[C|D|E]"))
    {
      Console.WriteLine("Please choose a valid option");
      return ListMenuOptions();
    }
    else if (op.Equals("C", StringComparison.CurrentCultureIgnoreCase))
    {
      return ChooseCalculation();
    }
    else if (op.Equals("D", StringComparison.CurrentCultureIgnoreCase))
    {
      DeleteList();
      return null;
    }
    else
    {
      Console.WriteLine("Continuing to calculator");
      return null;
    }
  }
  public static bool AnyEntriesInList()
  {
    return recentCalculations.Count != 0;
  }
  public static void DeleteList()
  {
    recentCalculations.Clear();
  }
  public static Calculation ReturnRecentCalculation(int index)
  {
    return recentCalculations[index - 1];
  }

  public static Calculation ChooseCalculation()
  {
    Console.WriteLine("Enter the number corresponding to your chosen calculation");
    string response = Console.ReadLine() ?? string.Empty;
    bool validIndex = IsListOptionValid(response);
    if (validIndex)
    {
      return ReturnRecentCalculation(int.Parse(response));
    }
    else
    {
      Console.WriteLine("Please choose a valid index");
      return ChooseCalculation();
    }
  }

  public static bool IsListOptionValid(string index)
  {
    if (int.TryParse(index, out int chosenIndex) && chosenIndex > 0 && chosenIndex <= recentCalculations.Count)
    {
      return true;
    }
    return false;
  }

  private static void DisplayList()
  {
    for (int i = 0; i < recentCalculations.Count; i++)
    {
      var calculation = recentCalculations[i];
      Console.WriteLine($"{i + 1}. {calculation.NumberOne} {calculation.Operator} {calculation.NumberTwo} = {calculation.Answer}");
    }
  }
  private static string GetOperation(string op)
  {
    return op switch
    {
      "a" => "+",
      "s" => "-",
      "m" => "*",
      "d" => "/",
      "e" => "^",
      "t" => "10x",
      _ => "Invalid operation",
    };
  }

  public static bool ListPrompt()
  {
    Console.WriteLine("Do you want to view recent calculations? (Y/N)");
    string response = Console.ReadLine() ?? string.Empty;
    if (response == "Y" || response == "y")
    {
      if (AnyEntriesInList())
      {
      return true;
      }
      else 
      {
        Console.WriteLine("No entries to display");
        return false;
      }
    }
    else if (response == "N" || response == "n")
    {
      return false;
    }
    else{
      return ListPrompt();
    }
  }
}
