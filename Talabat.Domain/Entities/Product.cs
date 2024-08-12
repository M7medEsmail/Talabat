﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int ProductBrandId { get; set; } // As A forign Key
        public ProductBrand ProductBrand { get; set; }
        public int ProductTypeId { get; set; } // As A forign Key
        public ProductType ProductType { get; set; }

    }
}
