using MimeKit.Encodings;
using System.ComponentModel.DataAnnotations;
using Talabat.Domain.Entities;

namespace Talabat.Dto
{
    public class CustomerBasketDto
    {
        public string BasketId { get; set; }
        public BasketItemDto BasketItem { get; set; }

    }
}
