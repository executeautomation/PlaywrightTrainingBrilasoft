using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTestDemo.Config
{
    public class PlaywrightConfigSettings
    {

        public string ApplicationUrl { get; set; }
        public string ApplicationApiUrl { get; set; }

        public bool Headless { get; set; }

        public int SlowMo { get; set; }
    }
}
