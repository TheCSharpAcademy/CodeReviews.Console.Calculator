namespace CalculatorLibrary
{
    public class Helpers
    {
        public static double GetNumber()
        {
            string? num = Console.ReadLine();

            while (string.IsNullOrEmpty(num) || !double.TryParse(num, out double _))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                num = Console.ReadLine();
            }

            return double.Parse(num);
        }
    }
}
