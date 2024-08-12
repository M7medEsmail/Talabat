using Talabat.Domain.Entities.OrderAggregate;

namespace Talabat.Dto
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } 
        public string Status { get; set; } 
        public Address ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; } 
        public decimal DeliveryMethodCost { get; set; } 

        public ICollection<OrderItemDto> Items { get; set; } // Navigation Properity [MANY]
        public decimal SubTotal { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal Total { get; set; }
    }
}
