using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Domain.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrdered productItem, decimal price, int quantity)
        {
            ProductItem = productItem;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrdered ProductItem {  get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
