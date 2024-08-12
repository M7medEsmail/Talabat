using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Domain.Entities.OrderAggregate;
using Talabat.Domain.Services;
using Talabat.Dto;
using Talabat.Errors;

namespace Talabat.Controllers
{
    [Authorize]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService OrderService , IMapper Mapper)
        {
            orderService = OrderService;
            mapper = Mapper;
        }

        [HttpPost] // POST:  api/Order
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder (OrderDto orderDto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var OrderAddress = mapper.Map<OrderAddressDto, Address>(orderDto.ShippingAddress);

            var Order = await orderService.CreateOrderAsync(BuyerEmail, orderDto.BasketId , orderDto.DeliveryMethodId , OrderAddress);
            if (Order == null) return BadRequest(new ApiErrorResponse(400, "Order is Null here"));
            return Ok(mapper.Map <Order , OrderToReturnDto >(Order));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderForUser()

        {
            var buyerEmail =  User.FindFirstValue(ClaimTypes.Email);

            var OrderForUser = await orderService.GetOrderForUserAsync(buyerEmail);
            if (OrderForUser == null) return NotFound();
            return Ok(mapper.Map<IReadOnlyList<Order> , IReadOnlyList<OrderToReturnDto>>(OrderForUser));

        }

        [HttpGet("id")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await orderService.GetOrderById(id , buyerEmail);
             if (order == null) return NotFound();
             return Ok(mapper.Map<Order , OrderToReturnDto>(order));
        }
        [HttpGet("deliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {
            var deliveryMethod = await orderService.GetDeliveryMethodAsync();
            return Ok(deliveryMethod);
        }
    }
}
