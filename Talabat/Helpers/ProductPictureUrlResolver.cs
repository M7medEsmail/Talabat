using AutoMapper;
using Talabat.Domain.Entities;
using Talabat.Dto;

namespace Talabat.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturn, string>
    {
        public ProductPictureUrlResolver( IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string Resolve(Product source, ProductToReturn destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            
                return $"{Configuration["BaseApiUrl"]}{source.PictureUrl}";
            
            return null;
        }
    }
}
