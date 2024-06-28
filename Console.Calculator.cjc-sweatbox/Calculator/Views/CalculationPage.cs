// -------------------------------------------------------------------------------------------------
// CalculatorProgram.Views.CalculationPage
// -------------------------------------------------------------------------------------------------
// The calculation page console view of the application.
// -------------------------------------------------------------------------------------------------
using System.Text;
using CalculatorLibrary.Constants;
using CalculatorLibrary.Models;
using CalculatorProgram.Utilities;

namespace CalculatorProgram.Views;

internal class CalculationPage
{
    #region Constants

    private const string PageTitle = "Calculation";

    #endregion
    #region Properties

    internal static string MenuText
    {
        get
        {
            var sb = new StringBuilder();
            sb.AppendLine("----------------------------------------");
            sb.AppendLine($"{Application.Title}: {PageTitle}");
            sb.AppendLine("----------------------------------------");
            sb.AppendLine("");
             
            return sb.ToString();
        }
    }

    internal static string OperatorQuestionText
    {
        get
        {
            var sb = new StringBuilder();
            sb.AppendLine("Choose an operator from the following list:");
            sb.AppendLine($"\t+ - Add ({OperationSymbol.Addition})");
            sb.AppendLine($"\t- - Subtract ({OperationSymbol.Subtraction})");
            sb.AppendLine($"\t* - Multiply ({OperationSymbol.Multiplication})");
            sb.AppendLine($"\t/ - Divide ({OperationSymbol.Division})");
            sb.AppendLine($"\tR - Square Root ({OperationSymbol.SquareRoot})");
            sb.AppendLine($"\tE - Exponentiation ({OperationSymbol.Exponentiation})");
            sb.AppendLine($"\tP - Power ({OperationSymbol.Power})");
            sb.AppendLine($"\tS - Sine ({OperationSymbol.Sine})");
            sb.AppendLine($"\tC - Cosine ({OperationSymbol.Cosine})");
            sb.AppendLine($"\tT - Tangent ({OperationSymbol.Tangent})");
            sb.Append("Your option? ");

            return sb.ToString();
        }
    }

    #endregion
    #region Methods: Internal Static

    internal static Calculation Show(double firstNumber = double.NaN)
    {
        Console.Clear();
        Console.Write(MenuText);

        return GetCalculation(firstNumber);
    }

    #endregion
    #region Methods: Private Static

    private static Calculation GetCalculation(double firstNumber = double.NaN)
    {
        // Declare variables and set to empty.
        double numInput1 = double.NaN;
        double numInput2 = double.NaN;

        if (double.IsNaN(firstNumber))
        {
            // Ask the user to type the first number.
            numInput1 = UserInputReader.GetDouble("Type a number, and then press Enter: ");
        }
        else
        {
            // Use the passed in first number and display it for the user.
            numInput1 = firstNumber;
            Console.WriteLine($"First number: {numInput1}");
        }

        // Ask the user to choose an operator.
        char option = UserInputReader.GetChar(OperatorQuestionText.ToString(), [.. AllowedChars.OneNumberCalculation, .. AllowedChars.TwoNumberCalculation]);

        // Only need to get a second number if the calculation option needs it.
        if (AllowedChars.TwoNumberCalculation.Contains(option)) 
        { 
            // Ask the user to type the second number.
            numInput2 = UserInputReader.GetDouble("Type another number, and then press Enter: ");
        }

        return new Calculation()
        {
            FirstNumber = numInput1,
            SecondNumber = numInput2,
            Option = option
        };
    }

    #endregion
}
