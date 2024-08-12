using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Domain.Entities;
using Talabat.Domain.IRepositories;
using Talabat.Dto;

namespace Talabat.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository BasketRepository;
        private readonly IMapper Mapper;

        public BasketController(IBasketRepository basketRepository , IMapper mapper)
        {
            BasketRepository = basketRepository;
            Mapper = mapper;
        }


        //[HttpGet] // Get: /Api/Basket/id
        //public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        //{
        //    var basket = await BasketRepository.GetBasketAsync(id);
        //    return Ok(basket ?? new CustomerBasket(id)); // if basket spent period of timeSpan (return null) , created the last basket by id
        //}

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket (CustomerBasket customerBasket)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            //Mapper.Map<BasketItemDto, BasketItem>(customerBasket.BasketItem);
            //var customer = Mapper.Map<CustomerBasketDto, CustomerBasket>(customerBasket);
    
            var CreatedOrUpdated = await BasketRepository.UpdateBasketAsync(customerBasket);

            return Ok(CreatedOrUpdated);
        }

        [HttpDelete]
        public async Task DeleteBasket (string id)
        {
           await BasketRepository.DeleteBasket(id);
        }
    }
}
