using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Talabat.Domain.Entities.Identity;
using Talabat.Domain.IRepositories;
using Talabat.Domain.Services;
using Talabat.Helpers;
using Talabat.Repository;
using Talabat.Repository.Data.Identity;
using Talabat.Service;

namespace Talabat.Extensions
{
    public static class AppServices
    {

        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IResponseCacheService ), typeof(ResponseCacheService)); // used singleton to created service as once time until hte project run
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IUniteOfWork), typeof(UniteOfWork));

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped(typeof(IOrderService), typeof(OrderService));

            services.AddAutoMapper(typeof(MappingProfile));



            return services;

        }
    }
}
