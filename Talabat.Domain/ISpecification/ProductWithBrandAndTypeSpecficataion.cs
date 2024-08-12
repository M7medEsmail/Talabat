using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;

namespace Talabat.Domain.ISpecification
{
    public class ProductWithBrandAndTypeSpecficataion :BaseSpecification<Product>
    {
        // this constructor use to get all specific product
        public ProductWithBrandAndTypeSpecficataion(ProductSpecParameters productParams ) 
            :base(P =>
            (!productParams.BrandId.HasValue || P.ProductBrandId == productParams.BrandId.Value)&&
            (!productParams.TypeId.HasValue || P.ProductTypeId == productParams.TypeId.Value)
            )

        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p =>p.ProductType);

             ApplyPagination((productParams.PageSize * (productParams.PageIndex- 1)),productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort) {

                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;

                 
                }
            }
        }

        // this constructor used to get specific product
        public ProductWithBrandAndTypeSpecficataion(int id):base(P =>P.Id ==  id) 
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
    }
}
