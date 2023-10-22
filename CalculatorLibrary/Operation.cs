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
            if (Constants.TWO_PARA_TYPES.Contains(Type))
            {
                return $"{Operand1} {Type} {Operand2} = {Result}";
            }
            return $"{Type} {Operand1} = {Result}";
        }
    }
}
