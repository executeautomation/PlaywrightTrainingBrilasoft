using PlaywrightTestDemo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlaywrightTestDemo.Config
{
    public class ConfigReader
    {
        public static PlaywrightConfigSettings ReadConfig()
        {
            //1. Read the JSON file
            //Get the JSON file from bin directory
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "PlaywrightConfig.json");

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);

                //2. Deserialize the JSON file to a Type
                return JsonSerializer.Deserialize<PlaywrightConfigSettings>(jsonData);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return null;


        }
    }
}
