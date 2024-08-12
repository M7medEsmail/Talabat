using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities.OrderAggregate;

namespace Talabat.Repository.Data.Config
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, NP => NP.WithOwner()); // take properities in address class to Order class (converted it in coulmn in Database)
            builder.Property(O => O.Status)
                .HasConversion(
                
                    OStatus => OStatus.ToString() , //store Enum Value As String
                    oStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), oStatus)
                );
            builder.HasMany(O=>O.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");

        }
    }
}
