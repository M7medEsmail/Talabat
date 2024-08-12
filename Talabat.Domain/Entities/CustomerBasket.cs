using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Domain.Entities
{
    public class CustomerBasket
    {
    //    public CustomerBasket(string id )
    //    {
    //            id = id.ToLower();  
    //    }
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();    
    }
}
