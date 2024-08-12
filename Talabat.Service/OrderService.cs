using MimeKit.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;
using Talabat.Domain.Entities.OrderAggregate;
using Talabat.Domain.IRepositories;
using Talabat.Domain.ISpecification;
using Talabat.Domain.Services;

namespace Talabat.Service
{
    public class OrderService : IOrderService

    {
        private readonly IBasketRepository basketRepo;
        private readonly IUniteOfWork _uniteOfWork;

        //private readonly IGenericRepository<Product> productRepo;
        //private readonly IGenericRepository<DeliveryMethod> deliveryMethodRepo;
        //private readonly IGenericRepository<Order> orderRepo;

        public OrderService(
            IBasketRepository BasketRepo ,
            IUniteOfWork uniteOfWork)
            //IGenericRepository<Product> ProductRepo ,
            //IGenericRepository<DeliveryMethod> DeliveryMethodRepo ,
            //IGenericRepository<Order> OrderRepo )
        {
            basketRepo = BasketRepo;
            _uniteOfWork = uniteOfWork;
            //productRepo = ProductRepo;
            //deliveryMethodRepo = DeliveryMethodRepo;
            //orderRepo = OrderRepo;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string BasketId, int deliveryMethodId, Address shippingAddress)
        {
            // 1 Get basket from basket repository
            var basket = await basketRepo.GetBasketAsync(BasketId);
            
            // 2 Get selected item at basket from product repo
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _uniteOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var productItemOrder = new ProductItemOrdered(product.Id , product.Name , product.PictureUrl);
                var orderItem = new OrderItem(productItemOrder , product.Price , item.Quantity);
                orderItems.Add(orderItem);
            }

            // Calculate SubTotal
            var SubTotal = orderItems.Sum(item=>item.Price * item.Quantity);

            //Get Delivery Method from Delivery Method Repository
            var deliveryMethod = await _uniteOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            
            //Create Order
            var order = new Order(buyerEmail , shippingAddress , deliveryMethod , orderItems , SubTotal);
            await _uniteOfWork.Repository<Order>().CreateAsync(order);

            //Save To Database 
             var result = _uniteOfWork.Complete();
            
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            var deliveryMethod = await _uniteOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethod;
        }

        public async Task<Order> GetOrderById(int orderId, string buyerEmail)
        {
            var spec = new OrderWithItemAndWithDeliveryMethodSpecification(orderId,buyerEmail);
            var order = await _uniteOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail) 
        {
            var spec = new OrderWithItemAndWithDeliveryMethodSpecification(buyerEmail);
            var Orders = await _uniteOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return Orders;
        }
    }
}
