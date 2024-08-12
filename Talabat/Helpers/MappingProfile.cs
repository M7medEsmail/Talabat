using AutoMapper;
using Talabat.Domain.Entities;
using Talabat.Domain.Entities.Identity;
using Talabat.Domain.Entities.OrderAggregate;
using Talabat.Dto;

namespace Talabat.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturn>()
                .ForMember(D => D.ProductBrand, d => d.MapFrom(s => s.ProductBrand.Name))
                .ForMember(D => D.ProductType, d => d.MapFrom(s => s.ProductType.Name))
                .ForMember(D => D.PictureUrl, d => d.MapFrom<ProductPictureUrlResolver>());

            CreateMap<AddressDto, Domain.Entities.Identity.Address>().ReverseMap();
            CreateMap<OrderAddressDto, Domain.Entities.OrderAggregate.Address>();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto , BasketItem>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(D => D.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(D => D.DeliveryMethodCost, O => O.MapFrom(S => S.DeliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductId, O => O.MapFrom(S => S.ProductItem.ProductId))
                .ForMember(D => D.ProductName, O => O.MapFrom(S => S.ProductItem.ProductName))
                .ForMember(D => D.PictureUrl, O => O.MapFrom(S => S.ProductItem.PictureUrl));


            
        }
    }
}
