namespace CalculatorLibrary.Models;

internal class OperationalData
{
    internal static List<DataFormat> previousOperations = new List<DataFormat>();

    public class DataFormat
    {
        public DateTime OperationDate
        {
            get; set;
        }
        public string OperationString
        {
            get; set;
        }
        public string Operation
        {
            get; set;
        }
        public double Result
        {
            get; set;
        }
    }
}
