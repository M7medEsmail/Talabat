﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;

namespace Talabat.Repository.Data.Config
{
    public class ProductConfigruation : IEntityTypeConfiguration<Product>
    {
       public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x=>x.Description).IsRequired();
            builder.Property(x=>x.PictureUrl).IsRequired();
            builder.HasOne(x => x.ProductBrand).WithMany().HasForeignKey(x => x.ProductBrandId);
            builder.HasOne(x=>x.ProductType).WithMany().HasForeignKey(x=>x.ProductTypeId);
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
        }
    }
}
