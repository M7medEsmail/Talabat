using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Domain.Entities;
using Talabat.Domain.IRepositories;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis) // context ready to run redis (Not implement)
        {
            _database = redis.GetDatabase();
                
        }
        public async Task<bool> DeleteBasket(string BasketId)
        {
            return await _database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string BasketId)
        {
            var basket =  await _database.StringGetAsync(BasketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            if (basket == null || string.IsNullOrWhiteSpace(basket.Id))
            {
                throw new ArgumentException("Basket or Basket ID cannot be null or empty.");
            }
            var CreateOrUpdate = await _database.StringSetAsync(basket.Id , JsonSerializer.Serialize(basket) , TimeSpan.FromHours(12));
            if (CreateOrUpdate == false) return null;
            return await GetBasketAsync(basket.Id);

            /*
              if (basket == null || string.IsNullOrWhiteSpace(basket.Id))
    {
        throw new ArgumentException("Basket or Basket ID cannot be null or empty.");
    }

    // Ensure basket.Id is used as the key and is not null
    var key = basket.Id;
    var value = JsonConvert.SerializeObject(basket);
    return await _database.StringSetAsync(key, value);
             */
        }
    }
}
