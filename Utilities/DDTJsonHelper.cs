using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlaywrightTestDemo.Utilities
{
    internal class DDTJsonHelper
    {
       

        public static void ReadJsonFile()
        {
            //1. Read the JSON file
            //Get the JSON file from bin directory
            var jsonFilePath = @"E:\source\PlaywrightTrainingBrilasoft\bin\Debug\net8.0\Data\LoginData.json";

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);

                //2. Deserialize the JSON file to a Type
                var loginData = JsonSerializer.Deserialize<LoginDetail>(jsonData);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }



     
        }

        

    }

    public record LoginDetail(string UserName, string Password);
}
