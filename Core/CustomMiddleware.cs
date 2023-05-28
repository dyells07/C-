using Core.Services;
using System.Runtime.Serialization;
//using Microsoft.Extensions.Options;

namespace Core
{
    public class CustomMiddleware
    {
        private RequestDelegate _next;
        //private IResponseFormatter _formatter;



        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
            //_formatter = formatter;

        }
        public async Task Invoke(HttpContext context, IResponseFormatter formatter1, IResponseFormatter formatter2, IResponseFormatter formatter3)
        {
            if (context.Request.Path == "/middleware")
            {
                await formatter1.Format(context, "Custom MiddleWare 1 Used\n\n");
                await formatter2.Format(context, "Custom MiddleWare 2 Used\n\n");
                await formatter3.Format(context, "Custom MiddleWare 3 Used ");

            }

            else
            {
                await _next(context);
            }
        }

    }
}
