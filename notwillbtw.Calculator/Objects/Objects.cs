namespace Objects
{
    public class Uses
    {
        public static List<CalcUse> uses = new List<CalcUse>
        {
            new CalcUse { Operand1 = 1, Operand2 = 2, Result = 3, Op = "+" },
            new CalcUse { Operand1 = 4, Operand2 = 5, Result = -1, Op = "-" },
            new CalcUse { Operand1 = 7, Operand2 = 8, Result = 56, Op = "*" },
            new CalcUse { Operand1 = 10, Operand2 = 2, Result = 5, Op = "/" },
            new CalcUse { Operand1 = 3, Operand2 = 4, Result = 7, Op = "+" },
            new CalcUse { Operand1 = 6, Operand2 = 7, Result = -1, Op = "-" },
            new CalcUse { Operand1 = 9, Operand2 = 10, Result = 90, Op = "*" },
            new CalcUse { Operand1 = 12, Operand2 = 3, Result = 4, Op = "/" },
            new CalcUse { Operand1 = 5, Operand2 = 6, Result = 30, Op = "*" },
            new CalcUse { Operand1 = 8, Operand2 = 2, Result = 4, Op = "/" },
            new CalcUse { Operand1 = 11, Operand2 = 3, Result = 14, Op = "+" },
            new CalcUse { Operand1 = 14, Operand2 = 7, Result = 7, Op = "-" },
            new CalcUse { Operand1 = 17, Operand2 = 5, Result = 85, Op = "*" },
            new CalcUse { Operand1 = 20, Operand2 = 4, Result = 5, Op = "/" },
            new CalcUse { Operand1 = 13, Operand2 = 2, Result = 15, Op = "+" }
        };
    }
    public class CalcUse
    {
        public double Operand1 { get; set; }

        public double Operand2 { get; set; }

        public string? Op { get; set; }

        public double Result { get; set; }

    }
}
