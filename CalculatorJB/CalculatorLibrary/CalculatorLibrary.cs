using System.Diagnostics;
using Newtonsoft.Json;


namespace CalculatorLibrary

{
    // public keyword exposed it to outside current library
    public class Calculator
    {
        // Count times used
        private int _timesUsed { get; set; }
        // List of previous games
        private List<PreviousCalculations> _previousCalculations;

        JsonWriter writer;
        public Calculator()
        {
            _timesUsed = 0;
            _previousCalculations = new List<PreviousCalculations>();

            StreamWriter logFile = File.CreateText("calulator.log"); // creater an I/O obj then pass it a name to create
            Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            Trace.AutoFlush = true;

            writer = new JsonTextWriter(logFile);   // JSON writer obj passed the logfile/stream
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            string operand; // holds operand for storage purposes
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
                    operand = "+";
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;
                case "s":
                    operand = "-";
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    operand = "*";
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    operand = "/";
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    break;
                // Return text for an incorrect option entry.
                default:
                    operand = "%%";
                    break;
            }

            int uses = GetTimesUsed();

            // Store calculation in method
            StoreCalculation(num1, operand ,num2, result);       

            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WritePropertyName("Used");
            writer.WriteValue(uses);
            writer.WriteEndObject();

            IncrementTimesUsed();

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();

        }
   
        // increments the _timesUsed 
        private void IncrementTimesUsed()
        {
            _timesUsed++;
        }
        
        // Return _timesUsed
        public int GetTimesUsed() { return _timesUsed; }
        
        // Private helper to store calculations
        private void StoreCalculation(double num1, string operand, double num2, double result)
        {
            PreviousCalculations previousCalc = new PreviousCalculations(num1, operand, num2, result);
            _previousCalculations.Add(previousCalc);

        }

        // Print all stores calculations
        public void PrintPreviousCalculations()
        {
            if (_previousCalculations != null && _previousCalculations.Count != 0)
            {
                foreach (var calculation in _previousCalculations)
                {
                    Console.WriteLine($"|{calculation.Num1} {calculation.Operand}" +
                        $" {calculation.num2} = {calculation.Result}|");
                }


            }
            else
            { 
                Console.WriteLine("No previous games.");
            }
            
        }
      
        // Wipe previous calculations
        public void ClearPreviousCalculations()
        {
            _previousCalculations.Clear();
        }
    }
}
