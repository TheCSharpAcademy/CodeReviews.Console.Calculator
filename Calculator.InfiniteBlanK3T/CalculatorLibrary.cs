using System;

namespace CalculatorProgram;

class Calculator
{
    private List<double> _results;
    public Calculator()
    {
        _results = new List<Double>();
    }
    public List<double> Results
    { get { return _results; } }
    public int CheckUserDigitInputINT(string? command)
    {
        int num;
        while (!int.TryParse(command, out num))
        {
            Console.Write("Invalid input. Please try again: ");
            command = Console.ReadLine();
        }
        return num;
    }
    public double CheckUserDigitInputDOUBLE(string? command)
    {
        double num;
        while (!double.TryParse(command, out num))
        {
            Console.Write("Invalid input. Please try again: ");
            command = Console.ReadLine();
        }
        return num;
    }
    public double DoOperationWithTwoOrMany(List<double> num)
    {
        double result = double.NaN;
        string op = Console.ReadLine();

        switch (op)
        {
            case "1":
                result = 0;
                foreach (double nub in num)
                {
                    result += nub;
                }
                break;
            case "2":
                result = num[0];
                for (int i = 1; i < num.Count; i++)
                {
                    result -= num[i];
                }
                break;
            case "3":
                result = num[0];
                for (int i = 1; i < num.Count; i++)
                {
                    result *= num[i];
                }
                break;
            case "4":
                if (num.Count == 2)
                {
                    result = Math.Pow(num[0], num[1]);
                }
                else
                {
                    result = double.NaN;
                }
                break;
            case "5":
                if (num[-1] != 0)
                {
                    result = num[0];
                    for (int i = 0; i < num.Count; i++)
                    {
                        result /= num[i];
                    }
                }
                break;
            default:
                Console.Write("Invalid input. ");
                break;
        }
        return result;
    }
    public double DoOperationWithOne(double num1)
    {
        double result = double.NaN;
        string op = Console.ReadLine();

        switch (op)
        {
            case "1":
                if (num1 >= 0)
                {
                    result = Math.Sqrt(num1);
                }
                break;
            case "2":
                if (num1 != 0)
                {
                    result = num1 * num1;
                }
                break;
            case "3":
                result = Math.Sin(num1);
                break;
            case "4":
                result = Math.Cos(num1);
                break;
            case "5":
                result = Math.Tan(num1);
                break;
            default:
                Console.Write("Invalid input. ");
                break;
        }
        return result;
    }
    public void StoreResult(double result)
    {
        _results.Add(result);
    }
    public int CountResultsList()
    {
        return _results.Count;
    }
    public void ClearResult()
    {
        _results.Clear();
    }
    public double GetResultFromList(string num)
    {
        var number = Convert.ToDouble(num);

        foreach (var item in _results)
        {
            if (number == item)
            {
                return item;
            }

        }
        return double.NaN;
    }
    public string ResultsList
    {
        get
        {
            string emptystring = "";
            for (int i = 0; i < _results.Count; i++)
            {
                emptystring += _results[i].ToString() + " ";
            }

            return emptystring;
        }
    }

}

