using Pugnetta.Calculator;
using System.Globalization;

Console.WriteLine("Note: decimals are defined with \".\"");
CultureInfo cultureInfo = CultureInfo.InvariantCulture;
var failedOperations = new List<Operation>();
var completedOperations = new List<Operation>();
var operators = Enum.GetValues(typeof(Operator)).Cast<Operator>().ToArray();
var map = Enumerable.Range(0, operators.Length).ToDictionary(key => key, val => operators[val]);
while (true)
{
    foreach (var kvp in map) Console.WriteLine($"{kvp.Key}: {kvp.Value}");
    var op = OperatorSelectionLoop();
    var (n1, n2) = NumberSelectionLoop();
    var operation = Operation.Create(n1, n2, op);
    try
    {
        var res = operation.Execute(op);
        completedOperations.Add(Operation.Create(operation, res));
        Console.WriteLine($"Operation result: {res}");
    }
    catch (Exception ex)
    {
        failedOperations.Add(operation);
        Console.WriteLine(ex.ToString());
    }
    Console.Read();
    Console.Clear();
}

Operator OperatorSelectionLoop()
{
    while (true)
    {
        Console.WriteLine("Select operation id");
        bool b = int.TryParse(Console.ReadLine(), out int r);
        if (b && map.TryGetValue(r, out var op)) return op;
    }
}
(decimal n1, decimal n2) NumberSelectionLoop()
{
    bool firstSelected = false;
    bool secondSelected = false;
    decimal n1 = 0, n2 = 0;
    while (true)
    {
        if (secondSelected) return (n1, n2);
        if (!firstSelected)
        {
            Console.WriteLine("Select operation number1");
            if (decimal.TryParse(Console.ReadLine(),
                cultureInfo, out var r))
            {
                n1 = r; firstSelected = true;
            }
        }
        else
        {
            Console.WriteLine("Select operation number2");
            if (decimal.TryParse(Console.ReadLine(),
                cultureInfo, out var r))
            {
                n2 = r; secondSelected = true;
            }
        }
    }
}
