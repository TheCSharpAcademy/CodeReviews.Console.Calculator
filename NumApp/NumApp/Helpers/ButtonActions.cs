namespace NumApp.Helpers;

internal class ButtonActions
{
    /// <summary>
    /// Clears input and values held by the calculator (all or current entry only).
    /// </summary>
    /// <param name="operationEntry"></param>
    /// <param name="operationLabel"></param>
    /// <param name="clearAll"></param>
    internal static void ClearCalculator(Entry operationEntry, Label operationLabel, bool clearAll = false)
    {
        operationEntry.Text = "";

        if (clearAll)
        {
            if (!String.IsNullOrEmpty(operationEntry.Text))
                CalculatorPage.CurrentCalculation.Clear();

            operationLabel.Text = "";
            CalculatorPage.LastOperation = "";
            CalculatorPage.CurrentValue = 0;
        }
    }

    /// <summary>
    /// Deletes the last character entered from the operation entry.
    /// </summary>
    /// <param name="operationEntry"></param>
    internal static void Delete(Entry operationEntry)
    {
        if (!String.IsNullOrEmpty(operationEntry.Text))
            operationEntry.Text = operationEntry.Text.Remove(operationEntry.Text.Length - 1);
    }

    /// <summary>
    /// Applies the action of the given operator, updates the operation entry and the operation label.
    /// </summary>
    /// <param name="buttonOperator"></param>
    /// <param name="operationEntry"></param>
    /// <param name="operationLabel"></param>
    internal static void ApplyOperator(string buttonOperator, Entry operationEntry, Label operationLabel, bool isKeyboardInput = false)
    {
        if (String.IsNullOrWhiteSpace(operationEntry.Text))
            return;

        string textInput;

        if (isKeyboardInput)
            textInput = operationEntry.Text.Remove(operationEntry.Text.Length - 1).Trim();
        else
            textInput = operationEntry.Text;

        double input;

        if (double.TryParse(textInput, out input))
        {
            if (CalculatorPage.LastOperation == "÷" && input == 0)
            {
                operationEntry.Text = "Error";
                return;
            }

            if (String.IsNullOrEmpty(CalculatorPage.LastOperation))
                CalculatorPage.CurrentValue = input;
            else
                Operations.PerformCalculation(CalculatorPage.LastOperation, input);

            // Update the last operation to this operator
            CalculatorPage.LastOperation = buttonOperator;

            if (String.IsNullOrEmpty(operationLabel.Text))
            {
                operationLabel.Text += (textInput + $" {buttonOperator} ");
            }
            else
            {
                // Keep current operation for future saving
                if (CalculatorPage.CurrentCalculation.Count == 0)
                {
                    // Store date if starting a new calculation
                    CalculatorPage.CurrentCalculation.Add(DateTime.Now.ToString());
                }
                CalculatorPage.CurrentCalculation.Add($"{operationLabel.Text}{input.ToString()}");

                operationLabel.Text = (CalculatorPage.CurrentValue.ToString() + $" {buttonOperator} ");
            }

            operationEntry.Text = "";
        }
        else
        {
            operationEntry.Text = "";
        }
    }

    /// <summary>
    /// Displays the given number in the operation entry.
    /// </summary>
    /// <param name="buttonNumber"></param>
    /// <param name="operationEntry"></param>
    internal static void DisplayNumber(string buttonNumber, Entry operationEntry)
    {
        operationEntry.Text += $"{buttonNumber}";
    }

    /// <summary>
    /// Stores a finished calculation for future saving and prepares space for the next calculation to be stored.
    /// </summary>
    private static void StoreCalculation()
    {
        foreach(string operation in CalculatorPage.CurrentCalculation)
        {
            CalculatorPage.Calculations.Add(operation);
        }
        CalculatorPage.CurrentCalculation.Clear();
    }

    /// <summary>
    /// Displays the result of the current operation held by the calculator.
    /// </summary>
    /// <param name="operationEntry"></param>
    /// <param name="operationLabel"></param>
    internal static void DisplayResult(Entry operationEntry, Label operationLabel, bool isKeyboardInput = false)
    {
        string textInput;

        if (isKeyboardInput)
            textInput = operationEntry.Text.Remove(operationEntry.Text.Length - 1).Trim();
        else
            textInput = operationEntry.Text;

        double input;

        if (!double.TryParse(textInput, out input))
        {
            operationEntry.Text = "";
            return;
        }

        if (isKeyboardInput)
            ApplyOperator("", operationEntry, operationLabel, true);
        else
            ApplyOperator("", operationEntry, operationLabel);

        if (CalculatorPage.LastOperation == "÷" && input == 0)
        {
            operationEntry.Text = "Error";
        }
        else
        {
            if (CalculatorPage.DecimalPrecision > 0)
            {
                operationEntry.Text = CalculatorPage.CurrentValue.ToString($"N{CalculatorPage.DecimalPrecision}");
            }
            else
            {
                operationEntry.Text = CalculatorPage.CurrentValue.ToString();
            }

            // Keep current result for future saving
            CalculatorPage.CurrentCalculation.Add(CalculatorPage.CurrentValue.ToString());
            StoreCalculation();
        }

        operationLabel.Text = "";
        CalculatorPage.LastOperation = "";
        CalculatorPage.CurrentValue = 0;
    }
    
    /// <summary>
    /// Changes the sign of the value in the operation entry to the opposite.
    /// </summary>
    /// <param name="operationEntry"></param>
    internal static void ChangeSign(Entry operationEntry)
    {
        double input;

        if (double.TryParse(operationEntry.Text, out input))
        {
            if (input != 0)
                input *= -1;

            operationEntry.Text = input.ToString();
        }
        else
        {
            operationEntry.Text = "";
        }
    }

    /// <summary>
    /// Changes the decimal precision of the calculator.
    /// </summary>
    /// <param name="operationEntry"></param>
    internal static bool ChangeDecimalPrecision(Entry operationEntry)
    {
        int input;

        if (int.TryParse(operationEntry.Text, out input))
        {
            if (input > 0 && input <= 10)
            {
                CalculatorPage.DecimalPrecision = input;
                return true;
            }
            else if (input == 0)
            {
                operationEntry.Text = "";
                return true;
            }
            else
            {
                operationEntry.Text = "Invalid";
                return false;
            }
        }
        operationEntry.Text = "";
        return true;
    }

    /// <summary>
    /// Handles operations that involve only the content of the operation entry.
    /// </summary>
    /// <param name="operationEntry"></param>
    /// <param name="operation"></param>
    internal static void ApplySingleVariableOperation(Entry operationEntry, string operation)
    {
        double input;

        if (double.TryParse(operationEntry.Text, out input))
            Operations.UpdateOperationEntryValue(operationEntry, operation, input);
        else
            operationEntry.Text = "";
    }

    /// <summary>
    /// Writes all finished calculations to a json file in the format of: date - operations - result (for each calculation).
    /// </summary>
    internal static void SaveCalculations()
    {
        bool operationBeingWritten = false;

        foreach (string operation in CalculatorPage.Calculations)
        {
            // Check for date as the starting point
            if (operation.Contains(':') || operation.Contains('/'))
            {
                if (!operationBeingWritten)
                {
                    CalculatorPage.Writer.WriteStartObject();
                    operationBeingWritten = true;

                    CalculatorPage.Writer.WritePropertyName("Date");
                    CalculatorPage.Writer.WriteValue(operation);
                }
            }
            // Check for operations
            else if (operation.Contains('+') || operation.Contains('-') || operation.Contains('×') || operation.Contains('÷'))
            {
                CalculatorPage.Writer.WritePropertyName("Operation");
                CalculatorPage.Writer.WriteValue(operation);
            }
            // Otherwise, write the result and end current calculation
            else
            {
                CalculatorPage.Writer.WritePropertyName("Result");
                CalculatorPage.Writer.WriteValue(operation);
                CalculatorPage.Writer.WriteEndObject();
                operationBeingWritten = false;
            }
        }
        CalculatorPage.Calculations.Clear();
    }

    /// <summary>
    /// Generates a random number between the content of the random entry from and the conent of the random entry to (inclusive).
    /// </summary>
    /// <param name="randomEntryFrom"></param>
    /// <param name="randomEntryTo"></param>
    internal static bool GenerateRandom(Entry randomEntryFrom, Entry randomEntryTo)
    {
        double inputFrom;
        double inputTo;

        if (!double.TryParse(randomEntryFrom.Text, out inputFrom))
        {
            randomEntryFrom.Text = "";
            randomEntryTo.Text = "";
            return false;
        }

        if (!double.TryParse(randomEntryTo.Text, out inputTo))
        {
            randomEntryFrom.Text = "";
            randomEntryTo.Text = "";
            return false;
        }

        if (inputTo < inputFrom)
        {
            randomEntryFrom.Text = "";
            randomEntryTo.Text = "";
            return false;
        }

        CalculatorPage.CurrentValue = Operations.GetRandom(inputFrom, inputTo);
        return true;
    }
}
