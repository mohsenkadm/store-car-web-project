using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using store_car_web_project.Models.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models.EntityMap.Security
{
    public class PostsMap : IEntityTypeConfiguration<Posts>
    {

        public void Configure(EntityTypeBuilder<Posts> builder)
        {
            builder.ToTable("posts", "Security");
            builder.HasKey(x => x.post_id);
            builder.Property(x => x.title);
            builder.Property(x => x.commend);
            builder.Property(x => x.address);
            builder.Property(x => x.price);
            builder.Property(x => x.phone);
            builder.Property(x => x.image);
            builder.Property(x => x.date);
            builder.Property(x => x.Company_id);
            builder.Property(x => x.Model);
            builder.Property(x => x.Type_id);
            builder.Property(x => x.user_id);
            builder.Property(x => x.post_id2);
            builder.Property(x => x.like_bit);
            builder.Property(x => x.count_comment);
            builder.Property(x => x.count_like);
            builder.Ignore(x => x.userName);
            builder.Ignore(x => x.isliked);
            builder.Ignore(x => x.imagecount);
            builder.Ignore(x => x.imagepath);
            builder.Ignore(x => x.Isonline);

        }
    }
}
