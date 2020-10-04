using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAppBLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public ImageDTO ProfileImage { get; set; }
        public bool IsOnline { get; set; }
        public string Password { get; set; }
    }
}
