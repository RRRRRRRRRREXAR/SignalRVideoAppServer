using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAppDAL.Entities
{
    public class Image:BaseEntity
    {
        public string Link { get; set; }
        public User UserId { get; set; }
    }
}
