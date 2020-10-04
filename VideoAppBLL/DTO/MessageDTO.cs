using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAppBLL.DTO
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public ConversationDTO DialogId { get; set; }
        public UserDTO Sender { get; set; }
        public UserDTO Recipient { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}
