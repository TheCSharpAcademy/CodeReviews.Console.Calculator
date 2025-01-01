namespace Calculator
{
    internal class Calculator
    {
        private static HistoryCalcu historyCalcu;

        public static void SetHistoryCalcu(HistoryCalcu calcu)
        {
            historyCalcu = calcu;
        }
        public static double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN;

            switch (op)
            {

                case "1":
                    result = num1 + num2;
                    historyCalcu.AddCals("Add", result);
                    break;
                case "2":
                    result = num1 - num2;
                    historyCalcu.AddCals("Subctract", result);
                    break;
                case "3":
                    result = num1 * num2;
                    historyCalcu.AddCals("Multiply", result);
                    break;
                case "4":
                    if (num2 != 0)
                    {

                        result = num1 / num2;
                    }
                    historyCalcu.AddCals("Divide", result);
                    break;
                case "5":
                    result = Math.Sqrt(num1);
                    historyCalcu.AddCals("Sqrt", result);
                    break;
                case "6":
                    result = Math.Pow(num1, num2);
                    historyCalcu.AddCals("Take pow", result);
                    break;
                case "7":
                    result = Math.Sin(num1);
                    historyCalcu.AddCals("Sin", result);
                    break;
                case "8":
                    result = Math.Cos(num1);
                    historyCalcu.AddCals("Sin", result);
                    break;
                case "9":
                    result = Math.Tan(num1);
                    historyCalcu.AddCals("Sin", result);
                    break;


                default:
                    break;
            }
            return result;



        }



    }
}
