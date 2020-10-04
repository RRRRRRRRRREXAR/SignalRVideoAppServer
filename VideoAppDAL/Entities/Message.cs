using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAppDAL.Entities
{
    public class Message : BaseEntity
    {
        public User Sender { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public Conversation DialogId { get; set; }
    }
}
