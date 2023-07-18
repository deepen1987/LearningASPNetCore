using MiddleWareExample.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();

var app = builder.Build();

app.UseLoginMiddleware();

app.Run();
