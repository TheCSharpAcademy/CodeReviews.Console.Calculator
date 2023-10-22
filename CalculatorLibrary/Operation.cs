using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Operation
    {
        [JsonProperty]
        public double Operand1 { get; set; }

        [JsonProperty]
        public double Operand2 { get; set; }

        [JsonProperty]
        public string Type { get; set; }

        [JsonProperty]
        public double Result { get; set; }

        public override string ToString()
        {
            return $"{Operand1} {Constants.TPS[Type]} {Operand2} = {Result}";
        }
    }
}
