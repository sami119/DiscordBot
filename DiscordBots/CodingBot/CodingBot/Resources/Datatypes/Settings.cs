using System;
using System.Collections.Generic;
using System.Text;

namespace CodingBot.Resources.Datatypes
{
    public class Settings
    {
        public string token { get; set; }
        public ulong owner { get; set; }
        public List<ulong> log { get; set; }
        public string version { get; set; }
        public List<ulong> banned { get; set; }
    }
}
