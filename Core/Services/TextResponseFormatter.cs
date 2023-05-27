namespace Core.Services
{
    public class TextResponseFormatter:IResponseFormatter
    {
        private int _responseCounter = 0;

        public async Task Format(HttpContext context,string content)
        {
            await context.Response.WriteAsync($"Response {++_responseCounter}\n{context}");
        }
    }
}
