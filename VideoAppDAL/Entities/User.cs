using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAppDAL.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public Image ProfileImage {get;set;}
        public bool IsOnline { get; set; }
        public string Password { get; set; }
        public int ProfileImageId { get; set; }
    }
}
