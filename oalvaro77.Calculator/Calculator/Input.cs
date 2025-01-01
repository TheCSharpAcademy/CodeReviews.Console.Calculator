namespace Calculator
{
    internal class Input
    {
        private static HistoryCalcu historyCalcu;

        public static void SetHistoryInput(HistoryCalcu _historyCalcu)
        {
            historyCalcu = _historyCalcu;
        }
        public static double Input1()
        {
            double previusResult = historyCalcu.GetPResult();
            //Ask the user to type the first type
            Console.WriteLine("Enter the first value, and then press enter or press enter to use the previus result", previusResult);
            string numInput = Console.ReadLine();

            if (string.IsNullOrEmpty(numInput))
            {
                numInput = previusResult.ToString();
            }

            double cleanNum1 = 0;
            while (!double.TryParse(numInput, out cleanNum1))
            {
                Console.WriteLine("This is a not valid input, please enter a numeric value");
                numInput = Console.ReadLine();
            }

            return cleanNum1;

        }

        public static double Input2()
        {
            Console.WriteLine("Enter the second value, and thne press enter");
            string numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.WriteLine("this is a not valid input, please enter a numeric value");
                numInput2 = Console.ReadLine();
            }

            return cleanNum2;
        }
    }
}
