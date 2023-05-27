namespace Core.Services
{
    public interface IResponseFormatter
    {
        Task Format(HttpContext context,string content);
    }
}
