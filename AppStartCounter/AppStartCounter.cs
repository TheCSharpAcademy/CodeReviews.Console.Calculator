namespace AppStartCounter
{
    public class AppStartCounter
    {
        public static int counter;
        public static void GetAndSaveStartCount()
        {
            string counterValue;
            counter = 0;
            try
            {
                StreamReader sr = new StreamReader("appcounter");
                counterValue = sr.ReadLine();
                sr.Close();
                counter = int.Parse(counterValue);
                counter++;
                StreamWriter sw = new StreamWriter("appcounter", false);
                sw.WriteLine(counter);
                sw.Close();
            }
            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter("appcounter", false);
                sw.WriteLine("1");
                sw.Close();
                Console.Error.WriteLine(ex.Message);
                counter = 1;
            }
        }

    }
}
