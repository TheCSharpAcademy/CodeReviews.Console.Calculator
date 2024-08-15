using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string usageFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usageCount.txt");
            IncrementUsageCount(usageFilePath);
            int usageCount = GetUsageCount(usageFilePath);

            bool endApp = false;

            do
            {
                Calculator.ShowMenu();
            }
            while (!endApp);
        }

        private static void IncrementUsageCount(string filePath)
        {
            int count = GetUsageCount(filePath);
            count++;
            File.WriteAllText(filePath, count.ToString());
        }

        private static int GetUsageCount(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return 0;
            }

            string countText = File.ReadAllText(filePath);
            if (int.TryParse(countText,out int count))
            {
                return count;
            }

            return 0;
        }
    }
}