using Talabat.Domain.Entities.OrderAggregate;

namespace Talabat.Dto
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public OrderAddressDto ShippingAddress { get; set; }
    }
}
