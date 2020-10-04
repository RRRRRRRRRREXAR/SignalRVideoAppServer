using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRVideoApp.Models
{
    public class Message
    {
        public string text { get; set; }
        public string author { get; set; }
        public DateTime date { get; set; }
    }
}
