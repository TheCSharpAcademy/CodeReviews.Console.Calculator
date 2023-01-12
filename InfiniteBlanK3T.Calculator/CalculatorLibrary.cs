using System;

namespace CalculatorProgram
{
    class Calculator
    {
        private List<double> _results;
        public Calculator()
        {
            _results = new List<Double>();
        }
        public double DoOperationWithTwoOrMany(List<double> num, string op)
        {
            double result = double.NaN; 
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = 0;
                    foreach (double nub in num)
                    {
                        result += nub;
                    }
                    break;
                case "s":
                    result = num[0];
                    for (int i = 1; i < num.Count; i++)
                    {
                        result -= num[i];
                    }
                    break;
                case "m":
                    result = num[0];
                    for (int i = 1; i < num.Count; i++)
                    {
                        result *= num[i];
                    }
                    break;
                case "p":
                    if ( num.Count == 2)
                    {
                        result = Math.Pow(num[0], num[1]);
                    }
                    else
                    {
                        result = double.NaN;
                    }
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num[-1] != 0)
                    {
                        result = num[0];
                        for (int i = 0; i < num.Count; i++)
                        {
                            result /= num[i];
                        }
                    }
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            return result;
        }
        public double DoOperationWithOne(double num1, string op)
        {
            double result = 0;
            switch (op)
            {
                case "s":
                    if (num1 >= 0)
                    {
                        result = Math.Sqrt(num1);
                    }
                    break;
                case "p":
                    if (num1 != 0)
                    {
                        result = num1 * num1;
                    }
                    break;
                case "sin":
                    result = Math.Sin(num1);
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    break;
                case "tan":
                    result = Math.Tan(num1);
                    break;
                default:
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
                if ( number == item)
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
                for(int i = 0; i < _results.Count; i++)
                {
                    emptystring += _results[i].ToString() + " ";
                }             
                    
                return emptystring;
            }
        }

    }
}

