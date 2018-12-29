using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CodingBot.Resources.Database
{
    public class Spam
    {
        [Key]
        public ulong UserId { get; set; }
        public ulong MessagesSend { get; set; }
        public string Name { get; set; }
    }
}
