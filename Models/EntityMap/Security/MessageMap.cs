using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using store_car_web_project.Models.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models.EntityMap.Security
{
    public class MessageMap : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Message", "Security");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Message_txt);
            builder.Property(x => x.Message_date);
            builder.Property(x => x.User_sender_id);
            builder.Property(x => x.User_name_s);
            builder.Property(x => x.User_reciver_id);
            builder.Property(x => x.User_name_r);
        }
    }
}
