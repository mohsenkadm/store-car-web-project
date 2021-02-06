using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using store_car_web_project.Models.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models.EntityMap.Security
{
    public class NotificationMap : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notification", "Security");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.user_id1);
            builder.Property(x => x.user_id2);
            builder.Property(x => x.type_note);
            builder.Property(x => x.post_id);
            builder.Property(x => x.date);
            builder.Property(x => x.seen);
            builder.Ignore(x => x.userName);
            builder.Ignore(x => x.count);
        }
    }
}
