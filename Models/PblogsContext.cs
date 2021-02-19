using Microsoft.EntityFrameworkCore;
using store_car_web_project.Models.Entity.Security;
using store_car_web_project.Models.EntityMap.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models
{
    public class PblogsContext:DbContext
    {
        public PblogsContext(DbContextOptions<PblogsContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersMap());
            modelBuilder.ApplyConfiguration(new PostsMap());
            modelBuilder.ApplyConfiguration(new ImagesMap());
            modelBuilder.ApplyConfiguration(new NotificationMap());
            modelBuilder.ApplyConfiguration(new MessageMap());
        }
        public  DbSet<Users> Users { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Images> images { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Messag> messags { get; set; }

    }
}
