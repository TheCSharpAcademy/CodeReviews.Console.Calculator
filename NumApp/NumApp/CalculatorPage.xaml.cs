using Newtonsoft.Json;
using NumApp.Helpers;

namespace NumApp;

public partial class CalculatorPage : ContentPage
{
    public static double CurrentValue { get; set; }
    public static string LastOperation { get; set; }
    public static List<string> Calculations { get; set; }
    public static List<string> CurrentCalculation { get; set; }
    public static JsonWriter Writer { get; set; }
    public static int DecimalPrecision { get; set; }

    public bool RandomOn { get; set; }
    public bool DecimalPrecisionOn { get; set; }

    private bool _moreOptionsShown = false;
    private List<Button> moreOptionsButtons = new List<Button>();

    private bool _randomEntryFromWasFocused = false;
    private bool _randomEntryToWasFocused = false;

    public CalculatorPage()
    {
        InitializeComponent();
        BindingContext = this;

        CurrentValue = 0;
        LastOperation = "";
        Calculations = new List<string>();
        CurrentCalculation = new List<string>();
        DecimalPrecision = 0;

        RandomOn = false;
        DecimalPrecisionOn = false;
        moreOptionsButtons = new List<Button>() { SaveButton, RandomButton, HexButton, BinButton };

        // Save json file at C:\Users\Username\AppData\Local\Packages\com.companyname.numapp_9zz4h110yvjzm\LocalCache\Local\numapplog.json
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "numapplog.json");
        StreamWriter logFile = File.CreateText(filePath);
        logFile.AutoFlush = true;
        Writer = new JsonTextWriter(logFile);
        Writer.Formatting = Formatting.Indented;
        Writer.WriteStartObject();
        Writer.WritePropertyName("Calculations");
        Writer.WriteStartArray();

        SaveButton.IsEnabled = false;
        OperationEntry.Loaded += (s, e) => { OperationEntry.Focus(); };
    }

    protected override void OnDisappearing()
    {
        Writer.WriteEndArray();
        Writer.WriteEndObject();
        Writer.Close();
    }

    /// <summary>
    /// Handles direct keyboard input in the operation entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckKeyboardInput(object sender, EventArgs e)
    {
        if (RandomOn)
            return;

        if (DecimalPrecisionOn)
            return;

        if (String.IsNullOrEmpty(OperationEntry.Text))
            return;

        string lastInput = OperationEntry.Text.Substring(OperationEntry.Text.Length - 1);

        switch (lastInput)
        {
            case "/":
                ButtonActions.ApplyOperator(DivideButton.Text, OperationEntry, OperationLabel, true);
                SaveButton.IsEnabled = false;
                OperationEntry.Focus();
                break;
            case "*":
                ButtonActions.ApplyOperator(MultiplyButton.Text, OperationEntry, OperationLabel, true);
                SaveButton.IsEnabled = false;
                OperationEntry.Focus();
                break;
            case "-":
                ButtonActions.ApplyOperator(SubtractButton.Text, OperationEntry, OperationLabel, true);
                SaveButton.IsEnabled = false;
                OperationEntry.Focus();
                break;
            case "+":
                ButtonActions.ApplyOperator(AddButton.Text, OperationEntry, OperationLabel, true);
                SaveButton.IsEnabled = false;
                OperationEntry.Focus();
                break;
            case "=":
                ButtonActions.DisplayResult(OperationEntry, OperationLabel, true);
                SaveButton.IsEnabled = true;
                OperationEntry.Focus();
                break;
            default:
                return;
        }
    }

    /// <summary>
    /// Shows or hides the additional buttons.
    /// </summary>
    /// <param name="shouldShow"></param>
    private void ShowMoreOptions(bool shouldShow)
    {
        foreach (Button moreOptionsButton in moreOptionsButtons)
        {
            if (shouldShow)
                moreOptionsButton.IsVisible = true;
            else
                moreOptionsButton.IsVisible = false;
        }
    }

    /// <summary>
    /// Handles showing/hiding additional buttons and adjusting the layout accordingly.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnMoreButtonClicked(object sender, EventArgs e)
    {
        if (!_moreOptionsShown)
        {
            this.Window.Height += SaveButton.MinimumHeightRequest;
            this.Window.MinimumHeight += SaveButton.MinimumHeightRequest;
            _moreOptionsShown = true;
            MoreButton.Text = "Less";
            ShowMoreOptions(true);
        }
        else
        {
            this.Window.MinimumHeight -= SaveButton.MinimumHeightRequest;
            this.Window.Height -= SaveButton.MinimumHeightRequest;
            _moreOptionsShown = false;
            MoreButton.Text = "More";
            ShowMoreOptions(false);
        }
        OperationEntry.Focus();
    }

    /// <summary>
    /// Resets current values in all currently visible entries.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnClearButtonClicked(object sender, EventArgs e)
    {
        if (DecimalPrecisionOn)
        {
            ButtonActions.ClearCalculator(OperationEntry, OperationLabel);
            OperationEntry.Focus();
            return;
        }

        if (!RandomOn)
        {
            ButtonActions.ClearCalculator(OperationEntry, OperationLabel, true);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
            return;
        }
            
        ButtonActions.ClearCalculator(RandomEntryFrom, OperationLabel);
        ButtonActions.ClearCalculator(RandomEntryTo, OperationLabel);
        RandomEntryFrom.Focus();
    }

    /// <summary>
    /// Resets currently used entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnClearEntryButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.ClearCalculator(OperationEntry, OperationLabel);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
            return;
        }

        if (_randomEntryFromWasFocused)
        {
            ButtonActions.ClearCalculator(RandomEntryFrom, OperationLabel);
            RandomEntryFrom.Focus();
        }
            
        if (_randomEntryToWasFocused)
        {
            ButtonActions.ClearCalculator(RandomEntryTo, OperationLabel);
            RandomEntryTo.Focus();
        }
    }

    /// <summary>
    /// Removes the last input in the currently used entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.Delete(OperationEntry);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
            return;
        }
            
        if (_randomEntryFromWasFocused)
        {
            ButtonActions.Delete(RandomEntryFrom);
            RandomEntryFrom.Focus();
        }
            
        if (_randomEntryToWasFocused)
        {
            ButtonActions.Delete(RandomEntryTo);
            RandomEntryTo.Focus();
        } 
    }

    /// <summary>
    /// Saves all finished calculations since the app was started.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSaveButtonClicked(object sender, EventArgs e)
    {
        ButtonActions.SaveCalculations();
        SaveButton.IsEnabled = false;
    }

    /// <summary>
    /// Switches buttons on/off depending on the specific options being on/off.
    /// </summary>
    /// <param name="setActive"></param>
    /// <param name="isRandom"></param>
    private void SwitchButtons(bool setActive, bool isRandom = true)
    {
        SaveButton.IsEnabled = !setActive;
        HexButton.IsEnabled = !setActive;
        BinButton.IsEnabled = !setActive;
        SquareButton.IsEnabled = !setActive;
        SqrtButton.IsEnabled = !setActive;
        DivideButton.IsEnabled = !setActive;
        MultiplyButton.IsEnabled = !setActive;
        SubtractButton.IsEnabled = !setActive;
        AddButton.IsEnabled = !setActive;
        PointButton.IsEnabled = !setActive;

        if (isRandom)
        {
            DecimalPrecisionButton.IsEnabled = !setActive;

            OperationEntry.IsVisible = !setActive;
            OperationLabel.IsVisible = !setActive;

            RandomEntryFrom.IsVisible = setActive;
            RandomEntryTo.IsVisible = setActive;
            RandomLabelFrom.IsVisible = setActive;
            RandomLabelTo.IsVisible = setActive;
        }
        else
        {
            RandomButton.IsEnabled = !setActive;
            SignButton.IsEnabled = !setActive;
            EqualsButton.IsEnabled = !setActive;
        }
    }

    /// <summary>
    /// Updates the last focused random entry to the random from entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RandomEntryFrom_Focused(object sender, FocusEventArgs e)
    {
        _randomEntryFromWasFocused = true;
        _randomEntryToWasFocused = false;
    }

    /// <summary>
    /// Updates the last focused random entry to the random to entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RandomEntryTo_Focused(object sender, FocusEventArgs e)
    {
        _randomEntryToWasFocused = true;
        _randomEntryFromWasFocused = false;
    }

    /// <summary>
    /// Switches the calculator to the random generator mode.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnRandomButtonClicked(object sender, EventArgs e)
    {
        CurrentValue = 0;
        LastOperation = "";
        OperationEntry.Text = "";

        if (RandomOn)
        {
            RandomOn = false;
            _randomEntryFromWasFocused = false;
            _randomEntryToWasFocused = false;
            RandomEntryFrom.Text = "";
            RandomEntryTo.Text = "";
            SwitchButtons(false);
            OperationEntry.Focus();
        }
        else
        {
            RandomOn = true;
            SwitchButtons(true);
            RandomEntryFrom.Focus();
        }
    }

    /// <summary>
    /// Converts the number in the operation entry to hexadecimal.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnHexButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.ApplySingleVariableOperation(OperationEntry, HexButton.Text);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
        }
    }

    /// <summary>
    /// Converts the number in the operation entry to binary.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnBinButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.ApplySingleVariableOperation(OperationEntry, BinButton.Text);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
        }
    }

    /// <summary>
    /// Applies the power of 2 to the content of the operation entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSquareButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.ApplySingleVariableOperation(OperationEntry, SquareButton.Text);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
        }
    }

    /// <summary>
    /// Applies square root to the content of the operation entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSqrtButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.ApplySingleVariableOperation(OperationEntry, SqrtButton.Text);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
        }   
    }

    /// <summary>
    /// Switches the calculator to the decimal precision setting mode.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnDecimalPrecisionButtonClicked(object sender, EventArgs e)
    {
        if (RandomOn)
            return;

        if (!DecimalPrecisionOn)
        {
            DecimalPrecisionOn = true;
            CurrentValue = 0;
            LastOperation = "";
            OperationLabel.Text = "Decimal precision:";
            OperationEntry.Text = "";
            DecimalPrecisionButton.Text = "Apply";
            SwitchButtons(true, false);
        }
        else
        {
            if (ButtonActions.ChangeDecimalPrecision(OperationEntry))
            {
                DecimalPrecisionOn = false;
                OperationLabel.Text = "";
                OperationEntry.Text = "";
                DecimalPrecisionButton.Text = "DP";
                SwitchButtons(false, false);
            }
        }
        OperationEntry.Focus();
    }

    /// <summary>
    /// Applies division to the content of the operation entry and the operation label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnDivideButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.ApplyOperator(DivideButton.Text, OperationEntry, OperationLabel);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
        } 
    }

    /// <summary>
    /// Applies multiplication to the content of the operation entry and the operation label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnMultiplyButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.ApplyOperator(MultiplyButton.Text, OperationEntry, OperationLabel);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
        } 
    }

    /// <summary>
    /// Applies subtraction to the content of the operation entry and the operation label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSubtractButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.ApplyOperator(SubtractButton.Text, OperationEntry, OperationLabel);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
        }
    }

    /// <summary>
    /// Applies addition to the content of the operation entry and the operation label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnAddButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.ApplyOperator(AddButton.Text, OperationEntry, OperationLabel);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
        }   
    }

    /// <summary>
    /// Shows the value of the current operation and ends it.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnEqualsButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.DisplayResult(OperationEntry, OperationLabel);
            SaveButton.IsEnabled = true;
            OperationEntry.Focus();
            return;
        }

        bool randomSuccessful = ButtonActions.GenerateRandom(RandomEntryFrom, RandomEntryTo);

        RandomOn = false;
        _randomEntryFromWasFocused = false;
        _randomEntryToWasFocused = false;
        RandomEntryFrom.Text = "";
        RandomEntryTo.Text = "";
        SwitchButtons(false);

        if (randomSuccessful)
        {
            if (DecimalPrecision > 0)
                OperationEntry.Text = CurrentValue.ToString($"N{DecimalPrecision}");
            else
                OperationEntry.Text = CurrentValue.ToString();
        }
        OperationEntry.Focus();
    }

    /// <summary>
    /// Inverts the sign of the value in the currently used entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSignButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.ChangeSign(OperationEntry);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
            return;
        }

        if (_randomEntryFromWasFocused)
        {
            ButtonActions.ChangeSign(RandomEntryFrom);
            RandomEntryFrom.Focus();
        }
            
        if (_randomEntryToWasFocused)
        {
            ButtonActions.ChangeSign(RandomEntryTo);
            RandomEntryTo.Focus();
        }
    }

    /// <summary>
    /// Displays the floating point in the operation entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnPointButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.DisplayNumber(PointButton.Text, OperationEntry);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
        }
    }

    /// <summary>
    /// Displays 0 in the currently used entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnZeroButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.DisplayNumber(ZeroButton.Text, OperationEntry);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
            return;
        }

        if (_randomEntryFromWasFocused)
        {
            ButtonActions.DisplayNumber(ZeroButton.Text, RandomEntryFrom);
            RandomEntryFrom.Focus();
        }
            
        if (_randomEntryToWasFocused)
        {
            ButtonActions.DisplayNumber(ZeroButton.Text, RandomEntryTo);
            RandomEntryTo.Focus();
        }
    }

    /// <summary>
    /// Displays 1 in the currently used entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnOneButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.DisplayNumber(OneButton.Text, OperationEntry);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
            return;
        }
        
        if (_randomEntryFromWasFocused)
        {
            ButtonActions.DisplayNumber(OneButton.Text, RandomEntryFrom);
            RandomEntryFrom.Focus();
        }
            
        if (_randomEntryToWasFocused)
        {
            ButtonActions.DisplayNumber(OneButton.Text, RandomEntryTo);
            RandomEntryTo.Focus();
        } 
    }

    /// <summary>
    /// Displays 2 in the currently used entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnTwoButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.DisplayNumber(TwoButton.Text, OperationEntry);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
            return;
        }

        if (_randomEntryFromWasFocused)
        {
            ButtonActions.DisplayNumber(TwoButton.Text, RandomEntryFrom);
            RandomEntryFrom.Focus();
        }
            
        if (_randomEntryToWasFocused)
        {
            ButtonActions.DisplayNumber(TwoButton.Text, RandomEntryTo);
            RandomEntryTo.Focus();
        } 
    }

    /// <summary>
    /// Displays 3 in the currently used entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnThreeButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.DisplayNumber(ThreeButton.Text, OperationEntry);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
            return;
        }

        if (_randomEntryFromWasFocused)
        {
            ButtonActions.DisplayNumber(ThreeButton.Text, RandomEntryFrom);
            RandomEntryFrom.Focus();
        }
            
        if (_randomEntryToWasFocused)
        {
            ButtonActions.DisplayNumber(ThreeButton.Text, RandomEntryTo);
            RandomEntryTo.Focus();
        }
    }

    /// <summary>
    /// Displays 4 in the currently used entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnFourButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.DisplayNumber(FourButton.Text, OperationEntry);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
            return;
        }

        if (_randomEntryFromWasFocused)
        {
            ButtonActions.DisplayNumber(FourButton.Text, RandomEntryFrom);
            RandomEntryFrom.Focus();
        }
            
        if (_randomEntryToWasFocused)
        {
            ButtonActions.DisplayNumber(FourButton.Text, RandomEntryTo);
            RandomEntryTo.Focus();
        }
    }

    /// <summary>
    /// Displays 5 in the currently used entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnFiveButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.DisplayNumber(FiveButton.Text, OperationEntry);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
            return;
        }

        if (_randomEntryFromWasFocused)
        {
            ButtonActions.DisplayNumber(FiveButton.Text, RandomEntryFrom);
            RandomEntryFrom.Focus();
        }
            
        if (_randomEntryToWasFocused)
        {
            ButtonActions.DisplayNumber(FiveButton.Text, RandomEntryTo);
            RandomEntryTo.Focus();
        }   
    }

    /// <summary>
    /// Displays 6 in the currently used entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSixButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.DisplayNumber(SixButton.Text, OperationEntry);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
            return;
        }

        if (_randomEntryFromWasFocused)
        {
            ButtonActions.DisplayNumber(SixButton.Text, RandomEntryFrom);
            RandomEntryFrom.Focus();
        }
            
        if (_randomEntryToWasFocused)
        {
            ButtonActions.DisplayNumber(SixButton.Text, RandomEntryTo);
            RandomEntryTo.Focus();
        } 
    }

    /// <summary>
    /// Displays 7 in the currently used entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSevenButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.DisplayNumber(SevenButton.Text, OperationEntry);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
            return;
        }

        if (_randomEntryFromWasFocused)
        {
            ButtonActions.DisplayNumber(SevenButton.Text, RandomEntryFrom);
            RandomEntryFrom.Focus();
        }
            
        if (_randomEntryToWasFocused)
        {
            ButtonActions.DisplayNumber(SevenButton.Text, RandomEntryTo);
            RandomEntryTo.Focus();
        }
    }

    /// <summary>
    /// Displays 8 in the currently used entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnEightButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.DisplayNumber(EightButton.Text, OperationEntry);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
            return;
        }

        if (_randomEntryFromWasFocused)
        {
            ButtonActions.DisplayNumber(EightButton.Text, RandomEntryFrom);
            RandomEntryFrom.Focus();
        }
            
        if (_randomEntryToWasFocused)
        {
            ButtonActions.DisplayNumber(EightButton.Text, RandomEntryTo);
            RandomEntryTo.Focus();
        } 
    }

    /// <summary>
    /// Displays 9 in the currently used entry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnNineButtonClicked(object sender, EventArgs e)
    {
        if (!RandomOn)
        {
            ButtonActions.DisplayNumber(NineButton.Text, OperationEntry);
            SaveButton.IsEnabled = false;
            OperationEntry.Focus();
            return;
        }

        if (_randomEntryFromWasFocused)
        {
            ButtonActions.DisplayNumber(NineButton.Text, RandomEntryFrom);
            RandomEntryFrom.Focus();
        }
            
        if (_randomEntryToWasFocused)
        {
            ButtonActions.DisplayNumber(NineButton.Text, RandomEntryTo);
            RandomEntryTo.Focus();
        }
    }
}
