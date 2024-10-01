
using Newtonsoft.Json;
using System.Diagnostics.Metrics;


namespace CalculatorLibrary
{

    public class CalculationLog
    {
        public double Num1 { get; set; }
        public double Num2 { get; set; }
        public double Result { get; set; }
        public string? Operation { get; set; }

    }

    public class CalculatorTracker
    {
        public int Counter;
    }

    public class Calculator
    {  
        string jsonFileLoc = "calculation.json";
        string counterFileLoc = "counter.json";
        List<CalculationLog> calcLogList = new List<CalculationLog>();
        List<CalculationLog> jsonList = new List<CalculationLog>();
        CalculatorTracker counter = new CalculatorTracker();
        public int calcCounter;
        bool loadCounterJsonAlready;
        string? input = "";
        public double DoOperation(double num1, string op, double num2 = double.NaN)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            // Use a switch statement to do the math.
            string operationUsed = "";
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    operationUsed = "Addition";
                    break;
                case "s":
                    result = num1 - num2;
                    operationUsed = "Subtraction";
                    break;
                case "m":
                    result = num1 * num2;
                    operationUsed = "Multiplication";
                    break;
                case "d":
                    //Ask the user to enter a non-zero divisor
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        operationUsed = "Division";
                    }
                    else
                    {
                        throw new DivideByZeroException("Mathematical Error. Dividing by Zero");
                    }
                    break;
                case "sqr":
                    result = Math.Sqrt(num1);
                    operationUsed = "Square Root";
                    break;
                case "e":
                    result = Math.Pow(num1, num2);
                    operationUsed = "Exponential";
                    break;
                case "10x":
                    result = Math.Pow(10, num1);
                    operationUsed = "10x";
                    break;
                case "sin":
                    result = Math.Sin(num1);
                    operationUsed = "Sine Function";
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    operationUsed = "Cosine Function";
                    break;
                case "tan":
                    result = Math.Tan(num1);
                    operationUsed = "Tangent Function";
                    break;
                // Return text for an incorrect ption entry
                default:
                    break;
            }
            if (!File.Exists(jsonFileLoc))
            {
                if (operationUsed == "Square Root" || operationUsed == "10x" || operationUsed == "Cosine Function" || operationUsed == "Sine Function" || operationUsed == "Tangent Function")
                {
                    calcLogList.Add(new CalculationLog { Num1 = num1, Num2 = double.NaN, Operation = operationUsed, Result = result });
                }
                else
                {
                    calcLogList.Add(new CalculationLog { Num1 = num1, Num2 = num2, Operation = operationUsed, Result = result });
                }
            }
            else
            {
                if (operationUsed == "Square Root" || operationUsed == "10x" || operationUsed == "Cosine Function" || operationUsed == "Sine Function" || operationUsed == "Tangent Function")
                {
                    jsonList.Add(new CalculationLog { Num1 = num1, Num2 = double.NaN, Operation = operationUsed, Result = result });
                }
                else
                {
                    jsonList.Add(new CalculationLog { Num1 = num1, Num2 = num2, Operation = operationUsed, Result = result });
                }
                
            }
            if (!File.Exists (counterFileLoc) || loadCounterJsonAlready)
            {
                counter.Counter++;
            }
            else if (File.Exists(counterFileLoc) && !loadCounterJsonAlready)
            {
                loadCounterJsonAlready = true;
                string jsonCounter = File.ReadAllText(counterFileLoc);
                int index = jsonCounter.IndexOf (": ");
                string number = jsonCounter.Substring(index + 1).Trim().TrimEnd('\r', '\n', '}');
                counter.Counter = Convert.ToInt32 (number);
                counter.Counter++;
            }
            calcCounter = counter.Counter;
            
            return result;
        }

        public void SaveCalculationToJSon()
        {
            if (!File.Exists(jsonFileLoc))
            {
                string calcLogJson = JsonConvert.SerializeObject(calcLogList, Formatting.Indented);
                File.WriteAllText(jsonFileLoc, calcLogJson);
            }
            else
            {
                string calcLogJson = JsonConvert.SerializeObject(jsonList, Formatting.Indented);
                File.WriteAllText(jsonFileLoc, calcLogJson);
            }
            SaveCounterToJson();
            
        }

        private void SaveCounterToJson()
        {
            string counterLogJson = JsonConvert.SerializeObject(counter, Formatting.Indented);
            File.WriteAllText(counterFileLoc, counterLogJson);
        }

        public void LoadCalculationJson()
        {
            string jsonFile = File.ReadAllText(jsonFileLoc);
            jsonList = JsonConvert.DeserializeObject<List<CalculationLog>>(jsonFile);
        }
        public void DeleteJson()
        {
            File.Delete(jsonFileLoc);
            Console.WriteLine("File deleted successfully.");
        }

        public void ViewPreviousCalculations()
        {
            Console.WriteLine("Showing previous calculations...");
            string operationUsed = "";

            if (File.Exists(jsonFileLoc))
            {
                for (int i = 0; i < jsonList.Count; i++)
                {
                    switch (jsonList[i].Operation)
                    {
                        case "Addition":
                            operationUsed = "+";
                            break;
                        case "Subtraction":
                            operationUsed = "-"; 
                            break;
                        case "Multiplication":
                            operationUsed = "*";
                            break;
                        case "Division":
                            operationUsed = "/";
                            break;
                        case "Square Root":
                            operationUsed = "√";
                            break;
                        case "Exponential":
                            operationUsed = "^";
                            break;
                        case "10x":
                            operationUsed = "10^x";
                            break;
                        case "Sine Function":
                            operationUsed = "sin";
                            break;
                        case "Cosine Function":
                            operationUsed = "cos";
                            break;
                        case "Tangent Function":
                            operationUsed = "tan";
                            break;
                    }
                    if (operationUsed == "√")
                    {
                        Console.WriteLine($"{i + 1}. {operationUsed}{jsonList[i].Num1} = {jsonList[i].Result}");
                    }
                    else if (operationUsed == "^")
                    {
                        Console.WriteLine($"{i + 1}. {jsonList[i].Num1}{operationUsed}{jsonList[i].Num2} = {jsonList[i].Result}");
                    }
                    else if (operationUsed == "10^x")
                    {
                        Console.WriteLine($"{i + 1}. 10^{jsonList[i].Num1} = {jsonList[i].Result}");
                    }
                    else if (operationUsed == "sin" || operationUsed == "cos" || operationUsed == "tan")
                    {
                        Console.WriteLine($"{i + 1}. {operationUsed}({jsonList[i].Num1}) = {jsonList[i].Result}");
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1}. {jsonList[i].Num1} {operationUsed} {jsonList[i].Num2} = {jsonList[i].Result}");
                    }
                    
                }
            }
        }

        public double UseResultAsOperand()
        {
            int chosenCalc = 0;

            Console.WriteLine("Enter the number at the left of the calculation of the result you wish to use.");
            input = Console.ReadLine();
            while (!int.TryParse(input, out chosenCalc) || (chosenCalc > jsonList.Count || chosenCalc < 1))
            {
                Console.WriteLine("Invald input. Please type the number at the left of the calculation you wish to choose.");
                input = Console.ReadLine();
            }
            return jsonList[chosenCalc - 1].Result;

        }

        
    }
}


