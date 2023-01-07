using Newtonsoft.Json;
using System;
using System.IO;

namespace Calculator.Lonchanick
{
    public class Program
    {
        static void Execute()
        {
            LogClass logClass = new LogClass();
            bool flag = false;
            Calculator calc = new Calculator();
            while (!flag)
            {
                // Declare variables and then initialize to zero.
                double num1 = 0; double num2 = 0;

                // Display title as the C# console calculator app.
                Console.WriteLine("Console Calculator in C#\r");
                Console.WriteLine("------------------------\n");

                // Ask the user to type the first number.
                Console.WriteLine("Type a number, and then press Enter");
                num1 = ToolBox.GetInputDouble();

                // Ask the user to type the second number.
                Console.WriteLine("Type another number, and then press Enter");
                num2 = ToolBox.GetInputDouble();

                // Ask the user to choose an option.
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.Write("Your option? ");

                //pedimos los valores por teclado
                string op = ToolBox.GetValidOption();
                //mandamos parametros a la funcion DoOperations para hacer el calculo
                calc.DoOperation(num1, num2, op);
                Console.WriteLine("Press q to close app, or press any other key to continue.. ");
                if (Console.ReadLine() == "q") flag = true;
            }
            calc.Finish();
        }

        static void ShowCurrentOperations()
        {
            string path = "D:/.NET FOLDER/C# ACADEMY/Lonchanick9427.Calculator" +
                "/Calculator.Lonchanick/Calculator.Lonchanick/bin/Debug/net6.0/calculatorlog.json";

            if(File.Exists(path)) 
            { 
                string content = File.ReadAllText(path);
                Console.WriteLine(content);
            } else 
            { 
                Console.WriteLine("Error: File does not exist! (But it was just created!");
                StreamWriter logFile = File.CreateText("calculatorlog.json");
                logFile.AutoFlush = true;
                logFile.Close();
            }

        }

        static void JsonSerializer()
        {
            string path = "D:/.NET FOLDER/C# ACADEMY/Lonchanick9427.Calculator" +
                "/Calculator.Lonchanick/Calculator.Lonchanick/bin/Debug/net6.0/calculatorlog.json";
            Operations ops = new Operations(1.1,2.1,3.1,"hola");
            Operations ops2 = new Operations(1.1,2.1,3.1,"hola2");
            List<Operations> list = new List<Operations>();
            list.Add(ops);
            list.Add(ops2);
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            // string json = JsonConvert.SerializeObject(ops, Formatting.Indented);
            //string json2 = JsonConvert.SerializeObject(ops2, Formatting.Indented);


            //Console.WriteLine(json);
            File.AppendAllText(path, json);
            //File.AppendAllText(path, json2);
            //Console.WriteLine("ARCHIVO JSON fue escrito en el ESCRITORIO");
        }
        static void Main(string[] args)
        {
            //JsonSerializer();
            Execute();
        }
    }
}
