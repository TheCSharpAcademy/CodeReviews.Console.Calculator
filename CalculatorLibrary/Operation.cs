using Newtonsoft.Json;

namespace CalculatorLibrary
{
    internal class Operation
    {
        [JsonProperty]
        public double Operand1 { get; set; }

        [JsonProperty] 
        public double Operand2 { get; set; }

        [JsonProperty]
        public string Type { get; set; }

        [JsonProperty]
        public double Result { get; set; }
    }
}
