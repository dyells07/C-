using Core;
using Core.Services;
//using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();
builder.Services.AddTransient<IResponseFormatter, GuidService>();
builder.Services.Configure<FruitOptions>(options =>
{
    options.Name = "watermelon";
});
var app = builder.Build();
//IResponseFormatter formatter =new TextResponseFormatter();
app.MapGet("/formatter1", async (HttpContext context,IResponseFormatter formatter) =>
{
    await formatter.Format(context, "Formatter 1");
}
    );

app.MapGet("/formatter2", async (HttpContext context, IResponseFormatter formatter) =>
{
    await formatter.Format(context, "Formatter 2");
}
    );

app.UseMiddleware<CustomMiddleware>();
app.MapGet("/endpoint", CustomEndpoint.Endpoint);

//app.MapGet("/fruit", async (HttpContext context) =>
//{
//    var options = context.RequestServices.GetRequiredService<IOptions<FruitOptions>>().Value;
//    await context.Response.WriteAsync($"{options.Name}, {options.Color}");
//});


//((IApplicationBuilder)app).Map("/branch", branch =>
//{
//    branch.Use(async (HttpContext context, Func<Task> next) =>
//    {
//        await context.Response.WriteAsync("Branch Middleware");

//    });

//}
//    );

//((IApplicationBuilder)app).Map("/branch", branch =>
//{
//    //branch.Run(async (HttpContext context) =>
//    //{
//    //    await context.Response.WriteAsync("Second Branch Middleware");
//    //}
//    //);
//    branch.Run(new Middleware().Invoke);

//}
//);

app.UseMiddleware<FruitMiddleware>();

app.MapGet("/", () => "Hello World");

app.Run();