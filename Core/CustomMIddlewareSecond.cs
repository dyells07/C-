using Core.Services;

namespace Core
{
    public class CustomMIddlewareSecond
    {
        private RequestDelegate _next;
        private IResponseFormatter _formatter;



        public CustomMIddlewareSecond(RequestDelegate next, IResponseFormatter formatter)
        {
            _next = next;
            _formatter = formatter;

        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/middlewaresecond")
            {
                await _formatter.Format(context, "Custom MiddleWare Second Used <h2>Fine!</h2>");

            }

            else
            {
                await _next(context);
            }
        }
    }
}

