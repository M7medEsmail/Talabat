using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Reflection.Metadata.Ecma335;
using Talabat.Domain.Entities;
using Talabat.Domain.IRepositories;
using Talabat.Helpers;

namespace Talabat.Controllers
{
    public class ProductTypeController : BaseApiController
    {
        private readonly IGenericRepository<ProductType> Repo;

        public ProductTypeController(IGenericRepository<ProductType> repo)
        {
            Repo = repo;
        }
        [CachedAttribute(600)]

        [HttpGet]

        //Use IEnumrable to return and itirate or loop data that return from database
        //use IReadOnlyList just return
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var Types = await Repo.GetAllAsync();
            return Ok(Types);

        }
         [CachedAttribute(600)]

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductType>> GetTypeById(int id)
        {
            var type = await Repo.GetByIdAsync(id);
            return Ok(type);
        } 
        


    }
}
