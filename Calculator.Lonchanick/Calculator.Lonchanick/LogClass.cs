using Newtonsoft.Json;

namespace Calculator.Lonchanick
{
    public class LogClass
    {
        List<Operations> list = new List<Operations>();
        string path = "D:/.NET FOLDER/C# ACADEMY/Lonchanick9427.Calculator" +
                "/Calculator.Lonchanick/Calculator.Lonchanick/bin/Debug/net6.0/calculatorlog.json";
        public LogClass() 
        { 
            if(File.Exists(path)) 
            { 
                list = JsonConvert.DeserializeObject<List<Operations>>(File.ReadAllText(path));
            }
        }
        public void AddOperation(Operations ops) 
        { 
            list.Add(ops);
        }
        public void CloseLog()
        {
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(path, json);
        }
        public void PrintContent()
        {
            Console.Clear();
            Console.WriteLine("\n\t >>>>>>> LOG <<<<<<<<\n");
            foreach (var op in list) 
            { 
            Console.WriteLine(string.Format("Operando #1: {0}",op.Operando1));
            Console.WriteLine(string.Format("Operando #2: {0}",op.Operando2));
            Console.WriteLine(string.Format("Operation: {0}",op.Operation));
            Console.WriteLine(string.Format("Result: {0}{1}",op.Result,"\n"));
            }
        }

    }
}
