// -------------------------------------------------------------------------------------------------
// CalculatorLibrary.Calculator
// -------------------------------------------------------------------------------------------------
// Main calculator logic and json file writing.
// Note: tired to keep as true to sample as I could, even though I would have design differently:
// https://learn.microsoft.com/en-us/visualstudio/get-started/csharp/tutorial-console?view=vs-2022
// -------------------------------------------------------------------------------------------------
using CalculatorLibrary.Models;
using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    #region Variables

    private readonly JsonWriter _writer;

    #endregion
    #region Constructors

    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        
        logFile.AutoFlush = true;
        
        _writer = new JsonTextWriter(logFile)
        {
            Formatting = Formatting.Indented
        };

        // Create the root object, and start the operations array.
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operations");
        _writer.WriteStartArray();

        // Initialise the History list.
        History = [];
    }

    #endregion
    #region Properties

    public bool HasHistoryItems => History.Count > 0;

    public int UsageCount { get; private set; }

    public List<Calculation> History { get; }

    #endregion
    #region Methods: Public

    public void ClearHistory()
    {
        History.Clear();
    }

    public Calculation DoOperation(Calculation calculation)
    {
        // Default value is "not-a-number" if an operation, such as division, could result in an error.
        calculation.Result = double.NaN;

        // Write operation data to JSON log file.
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operand1");
        _writer.WriteValue(calculation.FirstNumber);
        if (!double.IsNaN(calculation.SecondNumber))
        {
            _writer.WritePropertyName("Operand2");
            _writer.WriteValue(calculation.SecondNumber);
        }
        _writer.WritePropertyName("Operation");

        // Use a switch statement to do the math.
        switch (char.ToLower(calculation.Option))
        {
            case '+':
                // Addition.
                calculation.Result = calculation.FirstNumber + calculation.SecondNumber;
                _writer.WriteValue("Add");
                break;
            case '-':
                // Subtraction.
                calculation.Result = calculation.FirstNumber - calculation.SecondNumber;
                _writer.WriteValue("Subtract");
                break;
            case '*':
                // Multiplication.
                calculation.Result = calculation.FirstNumber * calculation.SecondNumber;
                _writer.WriteValue("Multiply");
                break;
            case '/':
                // Division.
                // Ask the user to enter a non-zero divisor.
                if (calculation.SecondNumber != 0)
                {
                    calculation.Result = calculation.FirstNumber / calculation.SecondNumber;
                }
                _writer.WriteValue("Divide");
                break;
            case 'r':
                // Square Root.
                calculation.Result = Math.Sqrt(calculation.FirstNumber);
                _writer.WriteValue("Square Root");
                break;
            case 'e':
                // Exponentation.
                calculation.Result = Math.Pow(calculation.FirstNumber, calculation.SecondNumber);
                _writer.WriteValue("Exponent");
                break;
            case 'p':
                // Power.
                calculation.Result = Math.Pow(10, calculation.FirstNumber);
                _writer.WriteValue("Power");
                break;
            case 's':
                // Sine.
                calculation.Result = Math.Sin(calculation.FirstNumber);
                _writer.WriteValue("Sine");
                break;
            case 'c':
                // Cosine.
                calculation.Result = Math.Cos(calculation.FirstNumber);
                _writer.WriteValue("Cosine");
                break;
            case 't':
                // Tangent.
                calculation.Result = Math.Tan(calculation.FirstNumber);
                _writer.WriteValue("Tangent");
                break;
            default:
                break;
        }

        // Write result data to JSON log file.
        _writer.WritePropertyName("Result");
        _writer.WriteValue(calculation.Result);
        _writer.WriteEndObject();

        // Add to History.
        History.Add(calculation);

        // Increment the usage counter that records how many operations have been performed.
        UsageCount++;

        return calculation;
    }

    public void Finish()
    {
        // End the operations array.
        _writer.WriteEndArray();
        
        // Write the usage count to the JSON log file.
        _writer.WritePropertyName("UsageCount");
        _writer.WriteValue(UsageCount);
        
        // End the root object and close the lock on the json file.
        _writer.WriteEndObject();
        _writer.Close();
    }

    #endregion
}
