namespace CalculatorLibrary;

public class Operation
{
    public int TimesUsed;
    public string Expression { get; set; } = "";
    public object Result { get; set; } = "";
}

public class ListOfOperations
{
    public List<Operation>? Operations { get; set; } = [];
}