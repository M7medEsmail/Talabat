using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Domain.Entities;
using Talabat.Domain.Entities.OrderAggregate;

namespace Talabat.Repository.Data
{
    public class TalabatContextSeed
    {
        public static async Task SeedData(TalabatContext context , ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    foreach (var brand in brands)
                    {
                        context.Set<ProductBrand>().Add(brand);
                    }

                }

                if (!context.ProductTypes.Any())
                {
                    var typeData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                    foreach (var type in types)
                    {
                        context.Set<ProductType>().Add(type);
                    }

                }

                if (!context.Products.Any())
                {
                    var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    foreach (var product in products)
                    {
                        context.Set<Product>().Add(product);
                    }

                }
                if (!context.DeliveryMethods.Any())
                {
                    var DeliveryMethodData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                    var DeliveryMethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodData);
                    foreach (var delivery in DeliveryMethod)
                    {
                        context.Set<DeliveryMethod>().Add(delivery);
                    }

                }

                await context.SaveChangesAsync();

            }   
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<TalabatContextSeed>();
                logger.LogError(ex, ex.Message);
            }
          
        }
    }
}
