using MiddleWareExample.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();

var app = builder.Build();

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Lambda middleware 1\n");
    await next(context);
});

//app.UseMiddleware<MyCustomMiddleware>();
//app.UseMyCustomMiddleware();
app.UseHelloCustomMiddleware();

app.Run(async (context) =>
{
    await context.Response.WriteAsync($"Lambda middleware 3\n");
});

app.Run();
