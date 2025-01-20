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
       
        public static LoginDetail ReadJsonFile()
        {
            //1. Read the JSON file
            //Get the JSON file from bin directory
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "LoginData.json");

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);

                //2. Deserialize the JSON file to a Type
                return JsonSerializer.Deserialize<LoginDetail>(jsonData);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return null;

     
        }

        

    }

    public record LoginDetail(string UserName, string Password);
}
