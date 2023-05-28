using Microsoft.Extensions.Options;

namespace Core
{
    public class VegMiddleware
    {
        private RequestDelegate _next;
        private VegOptions _options;



        public VegMiddleware(RequestDelegate next, IOptions<VegOptions> options)
        {
            _next = next;
            _options = options.Value;

        }
        public async Task Invoke(HttpContext context,ILogger<VegMiddleware> logger)
        {
            if (context.Request.Path == "/veg")
            {
                logger.LogDebug($"Strated Processing For {context.Request.Path}");
                await context.Response.WriteAsync($"{_options.Name},{_options.Color}");
                logger.LogDebug($"End Processing For {context.Request.Path}");

            }

            else
            {
                await _next(context);
                logger.LogDebug($"/veg not requested  {context.Request.Path}");
            }
            logger.LogDebug($"/veg was or was not requested  {context.Request.Path}");
        }
    }
}
