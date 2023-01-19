namespace Calculator.yashsachdev;

class Calculator
{
    private List<double> _storeValue = new List<double>();
    public List<double> store
    {
        get { return _storeValue; }
    }
    private int _count = 0;
    public int cnt
    {
        get { return _count; }
    }
    double result = double.NaN;

    public double DoOperation(double num1, double num2, string op)
    {
        switch (op)
        {
            case "a":
                result = num1 + num2;
                _storeValue.Add(result);
                break;
            case "s":
                result = num1 - num2;
                _storeValue.Add(result);
                break;
            case "m":
                result = num1 * num2;
                _storeValue.Add(result);
                break;
            case "d":
                if (num2 != 0)
                {
                    result = num1 / num2;
                    _storeValue.Add(result);
                }
                break;
            case "more":
                Moreoperations();
                    break;
            default:
                break;
        }
        _count++;
        Console.WriteLine($"The number of times Calculator was used {_count}");
        return result;
    }
    public void Moreoperations()
    {
        Console.WriteLine("Enter operation (pow,sqrt,10x,sin):");
        string opera = Console.ReadLine();
        if (opera == "pow")
        {
            Console.WriteLine("Enter number you want to use for power:");
            double Num = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine($"enter the number you wnat {Num} raised to ? ");
            double powerNum = Convert.ToDouble(Console.ReadLine());
            result = Math.Pow(Num, powerNum);
            _storeValue.Add(result);
        }
        else if (opera == "sqrt")
        {
            Console.WriteLine("Enter number you want to use for square root:");
            double sqrtNum = Convert.ToDouble(Console.ReadLine());
            result = Math.Sqrt(sqrtNum);
            _storeValue.Add(result);
        }
        else if (opera == "10x")
        {
            Console.WriteLine("Enter number you want to use for 10^x:");
            double tenXNum = Convert.ToDouble(Console.ReadLine());
            result = Math.Pow(10, tenXNum);
            _storeValue.Add(result);
        }
        else if (opera == "sin")
        {
            Console.WriteLine("Enter number you want to use for sin:");
            double sinNum = Convert.ToDouble(Console.ReadLine());
            result = Math.Sin(sinNum);
            _storeValue.Add(result);
        }
        else
        {
            Console.WriteLine("Invalid operation");
        }
    }
    public void RemoveList()
    {
        if (_storeValue.Count != 0)
        {
            _storeValue.Clear();
            Console.WriteLine("Result Cleared");
        }
        else
        {
            Console.WriteLine("Result is empty");
        }
    }
    public void DisplayResult()
    {
        if (!_storeValue.Any())
        {
            Console.WriteLine("List is empty");
        }
        for (int i = 0; i < _storeValue.Count; i++)
            Console.WriteLine("Result index=" + (i) + " :" + _storeValue[i]);
    }
}

