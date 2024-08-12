using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Domain.Services;

namespace Talabat.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;

        public CachedAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cahceServise = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheResponse = await cahceServise.GetCacheResponseAsync(cacheKey);

            if(!string.IsNullOrEmpty(cacheResponse)) // if it executed at least one saving this in cache
            {
                var contentResult = new ContentResult()
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200

                };

                context.Result = contentResult;
                return;

            }
            var executedEndPointContext = await next(); // Will Execute End Point if this not Executed any one
            if (executedEndPointContext.Result is OkObjectResult okObjectResult) // if EndPoint Succeed , save this in cache
            {
                await cahceServise.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }
                
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            //{{url}}/api/product?pageindex=1&pageSize=5&sort=name
            var keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Path); // /api/product


            //pageIndex=3
            //pageSize=5
            //sort=name
            foreach(var (key, value) in request.Query.OrderBy(O=>O.Key))
            {
                keyBuilder.Append($"|{key} - {value}");
                // /api/product|pageIndex=3
                // /api/product|pageIndex=3|pageSize=5
                // /api/product|pageIndex=3|pageSize=5|sort=name
                
            }
            return keyBuilder.ToString();
        }
    }
}
