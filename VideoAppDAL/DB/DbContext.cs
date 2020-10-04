using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VideoAppDAL.Entities;

namespace VideoAppDAL.DB
{
    public class AppContext:DbContext
    {
        DbSet<Conversation> Conversations { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<Message> Messages { get; set; }

        public AppContext(DbContextOptions<AppContext> options):base(options)
        {
            Database.EnsureCreated();
        }

    }
}
