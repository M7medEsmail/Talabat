using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections;
using System.Reflection.Metadata.Ecma335;
using Talabat.Domain.Entities;
using Talabat.Domain.IRepositories;
using Talabat.Dto;
using Talabat.Helpers;

namespace Talabat.Controllers
{
    public class ProductTypeController : BaseApiController
    {
        private readonly IGenericRepository<ProductType> _Repo;
        private readonly IMapper _mapper;
        private readonly IUniteOfWork _uniteOfWork;

        public ProductTypeController(IGenericRepository<ProductType> repo , IMapper mapper ,IUniteOfWork uniteOfWork)
        {
            _Repo = repo;
            _mapper = mapper;
            _uniteOfWork = uniteOfWork;
        }
        [CachedAttribute(600)]

        [HttpGet]
        [SwaggerOperation(summary: " ايجاد كل الفئات  ")]


        //Use IEnumrable to return and itirate or loop data that return from database
        //use IReadOnlyList just return
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var Types = await _Repo.GetAllAsync();
            return Ok(Types);

        }
         [CachedAttribute(600)]

        [HttpGet("{id}")]
        [SwaggerOperation(summary: " ايجاد فئه محدده عن طريق المعرف  ")]

        public async Task<ActionResult<ProductType>> GetTypeById(int id)
        {
            var type = await _Repo.GetByIdAsync(id);
            return Ok(type);
        }

        [HttpPost("AddType")]
        public async Task<ActionResult<ProductType>> AddProductType([FromBody]ProductTypeDto type)
        {
            if (type == null) return BadRequest();
            var ProductType = _mapper.Map<ProductTypeDto,ProductType>(type);
            var pType = await _Repo.CreateAsync(ProductType);
            return Ok(pType);
            
        }
        [HttpDelete("deleteType")]
        public async Task<ActionResult> DeleteProductType(int id) 
        {
            var type = await _Repo.GetByIdAsync(id);
            _Repo.DeleteAsync(type);
            return Ok();

        }
        [HttpPut("updateType")]
        public async Task<ActionResult> UpdateType([FromBody]ProductTypeDto type)
        {
            if (type == null)  return BadRequest();
            var productType= _mapper.Map<ProductTypeDto , ProductType>(type);
         
            _Repo.UpdateAsync(productType);
            return Ok(productType);
        }

        


    }
}
