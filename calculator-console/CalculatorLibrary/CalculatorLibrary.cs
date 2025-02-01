//using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public enum PossibleArithmeticOperations
    {
        Addition,
        Subtraction,
        Multiply,
        Division, 
        Power,
        SquareRoot,
        x10Power,
    }

    public enum PossibleTrigonometricOperations
    {
        Sin,
        Cos,
        Tan,
        Cosec,
        Sec,
        Cot
    }
    public enum PossibleOperations : int  
    {
        Arithmetic,
        Trigonometric
        
    }
    public class Calculator
    {
        JsonWriter writer;
        public Calculator()
        {

            StreamWriter logFile = File.CreateText("calculator.log");

            //Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            //Trace.AutoFlush = true;
            //Trace.WriteLine("Starting Calculator Log");
            //Trace.WriteLine(String.Format("Started {0}", System.DateTime.Now.ToString()));

            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        private void SavePropertiesToJson(string operation, double result, double number1, double number2 = double.NaN)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("OperationType");
            writer.WriteValue(operation);
            writer.WritePropertyName("Operand 1");
            writer.WriteValue(number1);
            if (!double.IsNaN(number2))
            {
                writer.WritePropertyName("Operand 2");
                writer.WriteValue(number2);
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
        }

        private double Add(double number1, double number2)
        {
            //Trace.WriteLine(String.Format("{0} + {1} = {2}", number1, number2, number1 + number2));
            double result = number1 + number2;
            SavePropertiesToJson("Add", result, number1, number2);
            return result;
        }
        private double Subtract(double number1, double number2)
        {
            //Trace.WriteLine(String.Format("{0} - {1} = {2}", number1, number2, number1 - number2));
            double result = number1 - number2;
            SavePropertiesToJson("Subtract", result, number1, number2);
            return result;
        }
        private double Multiply(double number1, double number2)
        {
            //Trace.WriteLine(String.Format("{0} * {1} = {2}", number1, number2, number1 * number2));
            double result = number1 * number2;
            SavePropertiesToJson("Multiply", result, number1, number2);
            return result;
        }
        private double Divide(double number1, double number2)
        {
            while (number2 == 0)
            {
                Console.WriteLine("You're performing an invalid division. Something divided by 0 is always 0. Select another number:");
                number2 = Convert.ToDouble(Console.ReadLine());
            }
            //Trace.WriteLine(String.Format("{0} + {1} = {2}", number1, number2, number1 / number2));
            double result = number1 / number2;
            SavePropertiesToJson("divide", result, number1, number2);
            return result;
        }

        private double Power(double number1, double number2)
        {
            //Trace.WriteLine(String.Format("{0} * {1} = {2}", number1, number2, number1 * number2));
            double result = Math.Pow(number1, number2);
            SavePropertiesToJson("Power", result, number1, number2);
            return result;
        }

        private double SquareRoot(double number1)
        {
            double result = Math.Sqrt(number1);
            SavePropertiesToJson("Square Root", result, number1);
            return result;
        }

        private double x10Power(double number1, double number2)
        {
            double result = number1 * Math.Pow(10, number2);
            SavePropertiesToJson("x10Power", result, number1, number2);
            return result;
        }
        private double Sin(double angle)
        {
            double radianAngle = (Math.PI/180) * angle;
            double result = Math.Sin(radianAngle);
            SavePropertiesToJson("Sin", result, angle);
            return result;
        }

        private double Cos(double angle)
        {
            double radianAngle = (Math.PI / 180) * angle;
            double result = Math.Cos(radianAngle);
            SavePropertiesToJson("Cos", result, angle);
            return result;
        }

        private double Tan(double angle)
        {
            double radianAngle = (Math.PI / 180) * angle;
            double result = Math.Tan(radianAngle);
            SavePropertiesToJson("Tan", result, angle);
            return result;
        }

        private double Cosec(double angle)
        {
            double radianAngle = (Math.PI / 180) * angle;
            double result = 1/Math.Sin(radianAngle);
            SavePropertiesToJson("Cosec", result, angle);
            return result;
        }

        private double Sec(double angle)
        {
            double radianAngle = (Math.PI / 180) * angle;
            double result = 1/Math.Cos(radianAngle);
            SavePropertiesToJson("Sec", result, angle);
            return result;
        }

        private double Cot(double angle)
        {
            double radianAngle = (Math.PI / 180) * angle;
            double result = 1 / Math.Tan(radianAngle);
            SavePropertiesToJson("Cot", result, angle);
            return result;
        }



        public double DoOperation(List<double> operands, int operation, int typeOfOperation)
        {
            switch (typeOfOperation)
            {
                case 0:
                    return operation switch
                    {
                        0 => this.Add(operands[0], operands[1]),

                        1 => this.Subtract(operands[0], operands[1]),

                        2 => this.Multiply(operands[0], operands[1]),

                        3 => this.Divide(operands[0], operands[1]),

                        4 => this.Power(operands[0], operands[1]),

                        5 => this.SquareRoot(operands[0]),

                        6 => this.x10Power(operands[0], operands[1]),


                        _ => throw new Exception("Operation yet to be added"),
                    };
                case 1:
                    return operation switch
                    {

                        0 => this.Sin(operands[0]),
                        1 => this.Cos(operands[0]),
                        2 => this.Tan(operands[0]),
                        3 => this.Cosec(operands[0]),
                        4 => this.Sec(operands[0]),
                        5 => this.Cot(operands[0]),
                        _ => throw new Exception("Operation yet to be added"),
                    };
                default:
                    throw new NotImplementedException("No other type of operations yet");
            }
            
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

    }


}
