using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities.OrderAggregate;

namespace Talabat.Domain.ISpecification
{
    public class OrderWithItemAndWithDeliveryMethodSpecification : BaseSpecification<Order>
    {
        // this constractor is used to get all orders for spec User
        public OrderWithItemAndWithDeliveryMethodSpecification(string BuyerEmail) : base(O=>O.BuyerEmail == BuyerEmail)
        {
                Includes.Add(O=>O.DeliveryMethod);
                Includes.Add(O=>O.Items);
                AddOrderByDescending(O=>O.OrderDate);
        }

        public OrderWithItemAndWithDeliveryMethodSpecification(int orderId , string BuyerEmail) :base(O=> O.BuyerEmail == BuyerEmail && O.Id == orderId)
        {

            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
            
        }
    }
}
