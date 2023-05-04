using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        private int _operationsDone;
        private int calculationID;
        JsonWriter writer;
        List<string> latestCalculations = new List<string>();

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
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            _operationsDone++;
            calculationID++;
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
                    latestCalculations.Add($"{calculationID}. {num1} + {num2} = {result}");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    latestCalculations.Add($"{calculationID}. {num1} - {num2} = {result}");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    latestCalculations.Add($"{calculationID}. {num1} * {num2} = {result}");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    latestCalculations.Add($"{calculationID}. {num1} / {num2} = {result}");
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Take the power");
                    latestCalculations.Add($"{calculationID}. {num1} ^ {num2} = {result}");
                    break;
                // Return text for an incorrect option entry.
                default:
                    _operationsDone--;
                    calculationID--;
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public double DoOperation(double num1, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            _operationsDone++;
            calculationID++;
            switch (op)
            {
                case "r":
                    result = Math.Sqrt(num1);
                    latestCalculations.Add($"{calculationID}. Sqrt({num1}) = {result}");
                    break;
                case "t":
                    result = num1 * 10;
                    latestCalculations.Add($"{calculationID}. 10({num1}) = {result}");
                    break;
                case "z":
                    result = Math.Sin(num1);
                    latestCalculations.Add($"{calculationID}. Sin({num1}) = {result}");
                    break;
                case "c":
                    result = Math.Cos(num1);
                    latestCalculations.Add($"{calculationID}. Cos({num1}) = {result}");
                    break;
                case "x":
                    result = Math.Tan(num1);
                    latestCalculations.Add($"{calculationID}. Tan({num1}) = {result}");
                    break;
                case "v": //Cotangent
                    result = 1 / Math.Tan(num1);
                    latestCalculations.Add($"{calculationID}. Tan({num1}) = {result}");
                    break;
                case "b": //Secant
                    result = 1 / Math.Cos(num1);
                    latestCalculations.Add($"{calculationID}. Tan({num1}) = {result}");
                    break;
                case "n": //Secant
                    result = 1 / Math.Sin(num1);
                    latestCalculations.Add($"{calculationID}. Tan({num1}) = {result}");
                    break;
                default:
                    _operationsDone--;
                    calculationID--;
                    break;
            }

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public void ShowLatestCalculations()
        {
            Console.WriteLine("Latest calculations: ");
            if (latestCalculations.Count > 0)
            {
                foreach (string calculation in latestCalculations)
                {
                    Console.WriteLine(calculation);
                }
                Console.WriteLine(); // Friednly linespacing.
                Console.WriteLine("Proceeding with the calculator...");
            }
            else
            {
                Console.WriteLine("You don't have recent calculations. Proceeding with the calculator...");
            }
        }

        public void DeleteLatestCalculations()
        {
            latestCalculations.Clear();
            Console.WriteLine("Latest calculations list is now empty");
        }

        public void showMenuOptions()
        {
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\tl - Show latest calculations");
            Console.WriteLine("\td - Delete latest calculations");
            Console.WriteLine("\tn - Close the app");
            Console.WriteLine("\tEnter / Any other letter - Continue with the calculator");
            Console.Write("Your option? ");
        }

        public bool ReadMenuOptions(string option)
        {
            bool validOption = true;
            switch (option)
            {
                case "l":
                    ShowLatestCalculations();
                    break;
                case "d":
                    DeleteLatestCalculations();
                    break;
                case "n":
                    break;
                default:
                    validOption = false; break;
            }
            return validOption;
        }

        public double PreviousOperationResult(int option)
        {
            double result = double.NaN;
            if (latestCalculations.Count == 0)
            {
                Console.WriteLine("You don't have recent calculations. Proceeding with the calculator...");
            }
            if (option >= 1 && option <= latestCalculations.Count)
            {
                double.TryParse(latestCalculations[option - 1].Split(" ").Last(), out result);
            }
            return result;
        }

        public int LatestCalculationsCount()
        {
            return latestCalculations.Count;
        }

        public double ReadNumber()
        {
            string numInput = "";
            string operationSelected = "";
            double previousOperation = double.NaN;

            Console.Write("Type a number or type h to use a previous result, and then press Enter: ");
            numInput = Console.ReadLine();

            double cleanNum = 0;
            bool breakLoop = false;
            while (!breakLoop)
            {
                double.TryParse(numInput, out cleanNum);
                if (numInput.Equals("h"))
                {
                    ShowLatestCalculations();
                    if (LatestCalculationsCount() > 0)
                    {
                        int operationSelectedClean = 0;

                        Console.Write("Choose the number of the operation that you want to use: ");
                        operationSelected = Console.ReadLine();

                        while (!int.TryParse(operationSelected, out operationSelectedClean) || double.IsNaN(previousOperation = PreviousOperationResult(operationSelectedClean)))
                        {
                            Console.Write("This is not valid input. Please enter an available option: ");
                            operationSelected = Console.ReadLine();
                        }

                        cleanNum = previousOperation;
                        break;
                    }
                    else
                    {
                        Console.Write("Type a number and then press Enter: ");
                    }
                }
                else if (double.IsNormal(cleanNum))
                {
                    break;
                }
                else
                {
                    Console.Write("This is not valid input. Please enter a valid value: ");
                }

                numInput = Console.ReadLine();
            }

            return cleanNum;
        }

        public int OperationsDone
        {
            get { return _operationsDone; }
        }
    }
}