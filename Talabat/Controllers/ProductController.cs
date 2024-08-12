using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Domain.Entities;
using Talabat.Domain.IRepositories;
using Talabat.Domain.ISpecification;
using Talabat.Dto;
using Talabat.Errors;
using Talabat.Helpers;

namespace Talabat.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IMapper Mapper;

        public ProductController(IGenericRepository<Product> productRepo , IMapper mapper)
        {
            ProductRepo = productRepo;
            Mapper = mapper;
        }

        public IGenericRepository<Product> ProductRepo { get; }


        [CachedAttribute(600)]
        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<ProductToReturn>>> GetProducts([FromQuery]ProductSpecParameters productParams)
        {
            var spec = new ProductWithBrandAndTypeSpecficataion(productParams);
            var products = await ProductRepo.GetAllWithSpecAsync(spec);
            return Ok(Mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturn>>(products));
        }

        [CachedAttribute(600)]

        [HttpGet("{id}")]
            
        public async Task<ActionResult<ProductToReturn>> GetProductById(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecficataion(id);
            var productById = await ProductRepo.GetByIdWithSpecAsync(spec);

            if (productById == null) return NotFound(new ApiErrorResponse(404, "Product Not Found"));
            return Ok(Mapper.Map<Product ,ProductToReturn >(productById));
        }

    } 
}
 