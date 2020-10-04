using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAppDAL.Entities
{
    public class Message : BaseEntity
    {
        public User Receiver { get; set; }
        public DateTime Time { get; set; }
        public Dialog DialogId { get; set; }
    }
}
