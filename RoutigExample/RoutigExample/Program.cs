using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("", async (httpContext) =>
    {
        await httpContext.Response.WriteAsync("In Home Page");
    });

    endpoints.MapPost("/map1/{id}", async (httpContext) =>
    {
        await httpContext.Response.WriteAsync("In Map 1");
    });

    //Route Params/ Default params / Optional Params

    endpoints.Map("employee/{empName:minlength(8):maxlength(17)=deep}", async (httpContext) =>
    {
        string? empName = Convert.ToString(httpContext.Request.RouteValues["empName"]);

        await httpContext.Response.WriteAsync($"Employee Name: {empName}");
    });

    endpoints.Map("files/{filename?}", async (httpContext) =>
    {
        if (httpContext.Request.RouteValues.ContainsKey("filename"))
        {
            await httpContext.Response.WriteAsync("present");
        }
        else
        {
            await httpContext.Response.WriteAsync("not present");
        }
    });
});

app.Run();
