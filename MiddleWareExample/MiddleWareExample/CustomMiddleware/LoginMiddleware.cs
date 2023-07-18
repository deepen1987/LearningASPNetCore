using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.WebUtilities;

namespace MiddleWareExample.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(httpContext.Request.Path == "/"  && httpContext.Request.Method == "POST")
            {
                StreamReader reader = new StreamReader(httpContext.Request.Body);
                string body = await reader.ReadToEndAsync();
                string validEmail = "admin@example.com";
                string validPassword = "admin1234";
                string? email = null, password = null;

                Dictionary<string, StringValues> queryDict = QueryHelpers.ParseQuery(body);

                if (queryDict.ContainsKey("email"))
                {
                    email = Convert.ToString(queryDict["email"][0]);
                }
                else
                {
                    if (httpContext.Response.StatusCode == 200)
                        httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsync("Invalid email \n");
                }

                if (queryDict.ContainsKey("password"))
                {
                    password = Convert.ToString(queryDict["password"][0]);
                }
                else
                {
                    if (httpContext.Response.StatusCode == 200)
                        httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsync("Invalid password \n");
                }

                if(string.IsNullOrEmpty(email) == false && string.IsNullOrEmpty(password) == false)
                {
                    if(email == validEmail && password == validPassword)
                    {
                        await httpContext.Response.WriteAsync("Successful login \n");
                    }
                    else
                    {
                        httpContext.Response.StatusCode = 401;
                        await httpContext.Response.WriteAsync("Invalid Login \n");
                    }
                }

            }
            else
            {
                await _next(httpContext);

            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoginMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginMiddleware>();
        }
    }
}
