using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAppBLL.DTO
{
    public class ConversationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserDTO> Users { get; set; }
        public List<MessageDTO> Messages { get; set; }
        public UserDTO Owner { get; set; }
    }
}

