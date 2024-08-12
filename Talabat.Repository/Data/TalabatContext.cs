using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;
using Talabat.Domain.Entities.OrderAggregate;

namespace Talabat.Repository.Data
{
    public class TalabatContext : DbContext
    {
        public TalabatContext(DbContextOptions<TalabatContext> options):base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // call FLuent Api (Call All Class that implement IEntityTypeConfigration)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //call all class that implement IEntityTypeConfiguration

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    }
}
