using Newtonsoft.Json;
using System.Text.RegularExpressions;
namespace MansoorAZafar.CalculatorLibrary
{
    public class Calculator
    {
        private int calculatorUsageCounter;
        private List<Calculation> history = new List<Calculation>();
        private JsonWriter writer;

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

        
        public double GetExistingResult(int idx)
        {
            int index = idx - 1;
            if (index < 0 || index > this.calculatorUsageCounter)
            {
                Console.WriteLine($"Index must be in range of [0 - {this.calculatorUsageCounter}) ( = exclusive | [ = inclusive\nReturning double.NaN\n\n");
                return double.NaN;
            }
            return this.history[index].result;
        }

        public double DoOperation(double num1, double num2 = 0, string op = "")
        {
            ++this.calculatorUsageCounter;
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            // Use a switch statement to do the math.
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
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    break;
                // Return text for an incorrect option entry.
                case "r":
                    if (num1 >= 0) result = Math.Sqrt(num1);
                    this.writer.WriteValue("Square Root");
                    break;
                case "x":
                    this.writer.WriteValue("Power");
                    result = Math.Pow(num1, num2);
                    break;
                case "t":
                    result = this.SelectAndDoTrigonometryEquation(value: ref num1, operation: ref op);
                    this.writer.WriteValue($"{op}");
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            this.history.Add(new Calculation(firstOperand: num1, secondOperand: num2, operation: op, result: result, calculationNumber: this.calculatorUsageCounter));
            return result;
        }

        private double SelectAndDoTrigonometryEquation(ref double value, ref string operation)
        {
            Console.Clear();
            Console.Write("Select a Trigonometric equation\n1. Sin(x)\n2. Cos(x)\n3. Tan(x)\n4. Sinh(x)\n5. Cosh(x)\n6. Tanh(x)\n> ");
            int selection = 0;
            while(!(int.TryParse(Console.ReadLine(), out selection)) || (selection < 0|| selection > 6))
                Console.Write("This is not valid input. Please enter an integer value [1-6]:\n> ");

            switch(selection)
            {
                //Sin
                case 1:
                    operation = "sin";
                    return Math.Sin(value);
                //Cos
                case 2:
                    operation = "cos";
                    return Math.Cos(value);
                //Tan
                case 3:
                    operation = "tan";
                    return Math.Tan(value);
                //sinH
                case 4:
                    operation = "sinH";
                    return Math.Sinh(value);
                //cosH
                case 5:
                    operation = "cosH";
                    return Math.Cosh(value);
                //tanH
                default:
                    operation = "tanH";
                    return Math.Tanh(value);
            }   
        }

        public void ViewHistory()
        {
            Console.Clear();
            Console.WriteLine("Console Calculator History\r");
            Console.WriteLine("------------------------\n");
            foreach (var singleCalculation in this.history) 
                Console.WriteLine(singleCalculation);

            Console.Write("\nRemove from History? (Y)es/(N)o\n> ");
            string? option = Console.ReadLine();
            while(option == null || !Regex.IsMatch(option.ToLower(), "y|n"))
            {
                Console.WriteLine("Error: bad input.");
                Console.Write("\nRemove from History? (Y)es/(N)o\n> ");
                option = Console.ReadLine();
            }
            if (option == "n") return;
            RemoveFromHistory();
        }

        private void RemoveFromHistory()
        {
            if(this.calculatorUsageCounter == 0)
            {
                Console.WriteLine("Sorry! There's no items to delete.\n");
                return;
            }
            Console.Write("\nSelect a Calculation Number to delete\n> ");
            int calculationNumberToBeDeleted = 0;
            while (!(int.TryParse(Console.ReadLine(), out calculationNumberToBeDeleted)) || (calculationNumberToBeDeleted > this.calculatorUsageCounter || this.calculatorUsageCounter <= 0))
            {
                Console.WriteLine("Error: Unrecognized input.");
                Console.Write("Select a Calculation Number to delete\n> ");
            }
            this.history.RemoveAll(current => current.calculationNumber == calculationNumberToBeDeleted);
            --this.calculatorUsageCounter;
            UpdateHistoryCalculationNumber(startingPoint: calculationNumberToBeDeleted);
            Console.WriteLine($"Removed Calculation #{calculationNumberToBeDeleted}\n");
        }

        private void UpdateHistoryCalculationNumber(int startingPoint)
        {
            //just need to update all numbers after [startingPoint] and subtract 1
            // we use <= calculationUsageCounter because before this function is called -> we subtracted 1
            for(int current = startingPoint - 1; current < this.history.Count; ++current)
                --this.history[current].calculationNumber;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}
