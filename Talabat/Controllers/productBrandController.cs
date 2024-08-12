using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Domain.Entities;
using Talabat.Domain.IRepositories;
using Talabat.Errors;
using Talabat.Helpers;

namespace Talabat.Controllers
{
    public class productBrandController : BaseApiController
    {
        private readonly IGenericRepository<ProductBrand> Repo;

        public productBrandController(IGenericRepository<ProductBrand > repo)
        {
            Repo = repo;
        }
        [CachedAttribute(600)]

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await Repo.GetAllAsync();
            return Ok(brands);
        }
        [CachedAttribute(600)]

        [HttpGet("{id}")]

        public async Task<ActionResult<ProductBrand>> GetBrandById(int id)
        {
            
            var brand = await Repo.GetByIdAsync(id);
            if (brand == null) return NotFound(new ApiErrorResponse(404, "This Brand Not Found"));
            return Ok(brand);
        }
    }
}
