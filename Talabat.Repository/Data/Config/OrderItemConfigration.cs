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
    public class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(O => O.ProductItem, NP => NP.WithOwner()); // take properities in ProductItemOrdered class to OrderItem class (converted it in coulmn in Database)

            builder.Property(OI => OI.Price).HasColumnType("decimal(18,2)");


        }
    }
}
