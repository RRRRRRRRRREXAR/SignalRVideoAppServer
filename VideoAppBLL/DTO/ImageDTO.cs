using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAppBLL.DTO
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public UserDTO User { get; set; }
    }
}
