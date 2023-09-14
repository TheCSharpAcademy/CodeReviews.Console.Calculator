namespace CalculatorLibrary
{
    public class Calculation
    {
        public DateTime CalculatedAt { get; set; }
        public double Result { get; set; }
        public double? Value { get; set; }
        public double? Operand1 { get; set; }
        public double? Operand2 { get; set; }
        public string? Operation { get; set; }

        public OperationType Type { get; set; }
        public override string ToString()
        {
            string tempOperation;

            if (Type == OperationType.Binary)
            {
                tempOperation = Operation switch
                {
                    "a" => "+",
                    "s" => "-",
                    "m" => "*",
                    "d" => "/",
                    "p" => "^",
                    _ => throw new Exception("Invalid operation."),
                };

                return $"{CalculatedAt} - {Operand1} {tempOperation} {Operand2} = {Result}";
            }

            tempOperation = Operation switch
            {
                "l" => "sin",
                "k" => "cos",
                "r" => "sqrt",
                _ => throw new Exception("Invalid operation.")
            };

            return $"{CalculatedAt} - {tempOperation}({Value}) = {Result}";
        }
    }
}
