namespace CalculatorLibrary
{
    public class Helper
    {
        public static List<OperationsList> games = new List<OperationsList>();
        public class OperationsList
        {

            public DateTime Date { get; set; }


            public string Operation { get; set; }

            public double Num1 { get; set; }

            public double Num2 { get; set; }

            public double Result { get; set; }

        }

        public static void AddToHistory(double Num1, double Num2, string op, double result)
        {


            games.Add(new OperationsList
            {
                Date = DateTime.Now,
                Operation = op,
                Num1 = Num1,
                Num2 = Num2,
                Result = result
            });

        }
    }
}
