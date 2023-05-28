using Core;
using Core.Services;
//using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHsts(options => 
{ 
    options.MaxAge=TimeSpan.FromDays(1);
    options.IncludeSubDomains = true;

});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(30);
    options.Cookie.IsEssential = true;
}


    );
//builder.Services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();

//builder.Services.AddScoped<IResponseFormatter,GuidService>();
//builder.Services.Configure<FruitOptions>(options =>
//{
//    options.Name = "watermelon";
//});
//var servicesConfig = builder.Configuration;
//builder.Services.Configure<VegOptions>(servicesConfig.GetSection("Veg"));
var app = builder.Build();
app.UseSession();

app.Logger.LogDebug("Pipeline Configuration Starting");
//IResponseFormatter formatter =new TextResponseFormatter();
//app.MapGet("/formatter1", async (HttpContext context,IResponseFormatter formatter) =>
//{
//    await formatter.Format(context, "Formatter 1");
//}
//    );

//app.MapGet("/formatter2", async (HttpContext context, IResponseFormatter formatter) =>
//{
//    await formatter.Format(context, "Formatter 2");
//}
//    );

//app.UseMiddleware<CustomMiddleware>();


//app.MapGet("/endpoint", CustomEndpoint.Endpoint);

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

//app.UseMiddleware<FruitMiddleware>();
//app.UseMiddleware<VegMiddleware>();
//app.MapGet("/config", async (HttpContext context, IConfiguration config) =>
//{
//    string defaultDebug = config["Logging:LogLevel:Default"];
//    await context.Response.WriteAsync(defaultDebug);
//    await context.Response.WriteAsync("\n\n");

//    string enviroment = config["ASPNETCORE_ENVIRONMENT"];
//    await context.Response.WriteAsync(enviroment);

//    if(app.Environment.IsDevelopment())
//    {
//        await context.Response.WriteAsync($"\nis {enviroment}.\n");
//    }
//});
app.MapGet("/session",async context=>
    {
        //int counter = int.Parse(context.Request.Cookies["counter"] ?? "0") + 1;
        //int counter = context.Request.Cookies.Count;
        int counter = (context.Session.GetInt32("counter") ?? 0) + 1;

        context.Session.SetInt32("counter", counter);

        await context.Session.CommitAsync();

        await context.Response.WriteAsync($"Session:{counter}");

});
//app.MapGet("/cookie", async context =>
//{
//    int counter = int.Parse(context.Request.Cookies["counter"] ?? "0") + 1;
//    //int counter = context.Request.Cookies.Count;
//    context.Response.Cookies.Append(
//        "counter",
//        counter.ToString(),
//        new CookieOptions
//        {
//            MaxAge = TimeSpan.FromMinutes(30)

//        }

//        );
//    await context.Response.WriteAsync($"Cookie:{counter}");

//});
//app.MapGet("/clear", context =>
//{
//    context.Response.Cookies.Delete("counter");
//    context.Response.Redirect("/");
//    return Task.CompletedTask;
//});
app.MapGet("/https", async context => 
{
    await context.Response.WriteAsync($"HTTPS Request:{context.Request.IsHttps}");
});
app.UseHttpsRedirection();

if(app.Environment.IsProduction())
{
    app.UseHsts();
}
app.MapGet("/", () => "Hello World");
app.Logger.LogDebug("Pipeline Configuration Completed");

//app.UseStaticFiles();

app.Run();