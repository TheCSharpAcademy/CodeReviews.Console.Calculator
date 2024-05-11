using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        private static List<Game> _games = [];
        private JsonWriter _writer;
        private Symbol _symbols = new();

        private void AddToHistory(double num1, double num2, double result, string op)
        {
            _games.Add(new Game
            {
                FirstNumber = num1,
                SecondNumber = num2,
                Type = _symbols.OperationToSymbol(op) ,
                Result = result
            });
        }

        public void ShowHistory()
        {
            foreach (var game in _games)
            {
                Console.WriteLine($"{game.FirstNumber} {game.Type} {game.SecondNumber} = {game.Result}");
            }
            
            Console.WriteLine("Would you like to delete the history? Press d - Delete");
            // Call DeleteHistory function
            var op = Console.ReadLine();
            if (op == "d") DeleteHistory();
            
            Console.WriteLine("Press a key to continue back to main!");
            Console.ReadLine();
        }

        public void DeleteHistory()
        {
            _games.Clear();
            Console.WriteLine("History has been cleared!");
        }
        
        public void Finish()
        {
            _writer.WriteEndArray();
            _writer.WriteEndObject();
            _writer.Close();
        }
        
        public Calculator() {
            StreamWriter logFile = File.CreateText("calculator.log");
            logFile.AutoFlush = true;
            _writer = new JsonTextWriter(logFile);
            _writer.Formatting = Formatting.Indented;
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operations");
            _writer.WriteStartArray();
        }

        public double DoOperation(double num1, double num2, string op)
        {
            // Default value is "not a number" if an operation, such as division, could result
            // in an error
            double result = double.NaN; 
            
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operand1");
            _writer.WriteValue(num1);
            _writer.WritePropertyName("Operand2");
            _writer.WriteValue(num2);
            _writer.WritePropertyName("Operation");
            
            switch(op)
            {
                case "a":
                    result = num1 + num2;
                    _writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    _writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    _writer.WriteValue("Multiply");
                    break;
                case "d":
                    if (num2 != 0) 
                    {
                      result = num1 / num2;
                    }
                    
                    _writer.WriteValue("Divide");
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    _writer.WriteValue("Power");
                    break;
                case "sr":
                    result = Math.Sqrt(num1);
                    _writer.WriteValue("Square Root");
                    break;
                default:
                    Console.WriteLine("Incorrect option.");
                    break;
            }
            _writer.WritePropertyName("Result");
            _writer.WriteValue(result);
            _writer.WriteEndObject();
            
            AddToHistory(num1, num2, result, op);
            
            return result;
        }

    }

}
