using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Talabat.Domain.Entities;
using Talabat.Domain.IRepositories;
using Talabat.Dto;
using Talabat.Errors;
using Talabat.Helpers;

namespace Talabat.Controllers
{
    public class ProductBrandController : BaseApiController
    {
        private readonly IGenericRepository<ProductBrand> _repo;
        private readonly IMapper _mapper;

        public ProductBrandController(IGenericRepository<ProductBrand > repo , IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [CachedAttribute(600)]

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [SwaggerOperation(summary: " ايجاد كل الماركات الموجودة   ")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _repo.GetAllAsync();
            return Ok(brands);
        }
        [CachedAttribute(600)]

        [HttpGet("{id}")]
        [SwaggerOperation(summary: " ايجاد ماركة محدده عن طريق المعرف ")]

        public async Task<ActionResult<ProductBrand>> GetBrandById(int id)
        {
            
            var brand = await _repo.GetByIdAsync(id);
            if (brand == null) return NotFound(new ApiErrorResponse(404, "This Brand Not Found"));
            return Ok(brand);
        }

        [HttpPost("AddBrand")]
        public async Task<ActionResult<ProductBrand>> AddProductBrand([FromBody] ProductBrandDto brand)
        {
            if (brand == null) return BadRequest();
            var ProductBrand = _mapper.Map<ProductBrandDto, ProductBrand>(brand);
            var pBrand = await _repo.CreateAsync(ProductBrand);
            return Ok(pBrand);

        }
        [HttpDelete("DeleteBrand")]
        public async Task<ActionResult> DeleteProductBrand(int id)
        {
            var brand = await _repo.GetByIdAsync(id);
            _repo.DeleteAsync(brand);
            return Ok();

        }
        [HttpPut("UpdateBrand")]
        public async Task<ActionResult> UpdateType([FromBody] ProductBrandDto brand)
        {
            if (brand == null) return BadRequest();
            var productBrand = _mapper.Map<ProductBrandDto, ProductBrand>(brand);

            _repo.UpdateAsync(productBrand);
            return Ok(productBrand);
        }
    }
}
