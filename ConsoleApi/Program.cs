using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;

namespace ConsoleApi
{
    class Program
    {
        public static string lastError = "";
        public static string banks = "";

        static void Main(string[] args)
        {
            GetBanksInfoSync();
            Console.WriteLine("Sync data load: " + lastError);

            if (banks != "")
            {
                
                Banks currentBank = JsonConvert.DeserializeObject<Banks>(banks);
                Console.WriteLine(currentBank.КодБанку);
                
                /*
                JObject currbank = JObject.Parse(banks);
                Console.WriteLine(currbank.GetValue("Код банку"));*/
            }
            else
            {
                Console.WriteLine("No bank data available");
            }
            Console.ReadLine();
        }

        private static void GetBanksInfoSync()
        {
            lastError = "";
            WebClient client = new WebClient();
            try
            {
                using (Stream stream = client.OpenRead("https://raw.githubusercontent.com/dchaplinsky/nbu.rocks/master/www/jsons/307123.json"))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        banks = reader.ReadToEnd();
                    }
                }
                lastError = "successfuly";
            }
            catch (Exception e)
            {
                lastError = e.Message;
            }
        }
    }
}
