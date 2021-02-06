using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using store_car_web_project.Models.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models.EntityMap.Security
{
    public class ImagesMap : IEntityTypeConfiguration<Images>
    {
        public void Configure(EntityTypeBuilder<Images> builder)
        {
            builder.ToTable("images", "Security");
            builder.HasKey(x => x.image_id);
            builder.Property(x => x.Image_path);
            builder.Property(x => x.post_id);
        }
    }
}
