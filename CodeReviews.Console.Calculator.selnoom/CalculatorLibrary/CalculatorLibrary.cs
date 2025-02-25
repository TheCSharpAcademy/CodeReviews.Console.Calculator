using System.Diagnostics;
using CalculatorLibrary.Models;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        List<Calculation> calculations = new List<Calculation>();
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; 
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    break;
                case "r":
                    result = Math.Pow(num1, 1.0 / num2);
                    writer.WriteValue("Root");
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    break;
                case "h":
                    ShowHistory();
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            calculations.Add(new Calculation
            {
                Operation = op,
                Num1 = num1.ToString(),
                Num2 = num2.ToString(),
                Result = result.ToString()
            });
            writer.WriteEndObject();

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public Calculation? ShowHistory()
        {
            Console.Clear();
            if (calculations.Count <= 0)
            {
                Console.WriteLine("No calculations yet.");
                Console.WriteLine("\nPress enter to continue");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Calculations made so far:\n");
                foreach (Calculation calc in calculations)
                {
                    Console.WriteLine($"Id: {calc.Id}\tOperation: {calc.Operation}\t1st number: {calc.Num1}\t2nd number: {calc.Num2}\tResult: {calc.Result}");
                }
                Console.WriteLine("\nWould you like to delete the list or use the results for future calculations?");
                Console.WriteLine("\nPress D to delete, R to use the results or anything else to ignore");
                string historyChoice = Console.ReadLine().ToLower();
                if (historyChoice == "d")
                {
                    calculations.Clear();
                    Console.WriteLine("List deleted!");
                    Console.WriteLine("\nPress enter to continue");
                    Console.ReadLine();
                }
                else if (historyChoice == "r")
                {
                    int chosenId;
                    List<int> validIds = calculations.Select(x => x.Id).ToList();
                    Console.WriteLine("\nType the Id of the result you wish to use in a future calculation");
                    while (true)
                    {
                        string userInput = Console.ReadLine();

                        if (!int.TryParse(userInput, out chosenId))
                        {
                            Console.WriteLine("Invalid input. Please type a valid integer Id.\n");
                            continue;
                        }

                        if (!validIds.Contains(chosenId))
                        {
                            Console.WriteLine("This Id doesn't exist. Please type a valid Id.\n");
                            continue;
                        }

                        break;
                    }
                    Console.Clear();
                    Console.WriteLine($"The chosen id was {chosenId}");
                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();
                    Calculation chosenCalc = calculations.First(c => c.Id == chosenId);
                    return chosenCalc;
                }
            }
            Console.Clear();
            return null;
        }
    }
}
