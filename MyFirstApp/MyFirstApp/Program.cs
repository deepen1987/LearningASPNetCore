var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    var firstNumber = context.Request.Query.ContainsKey("firstNumber");
    var secondNumber = context.Request.Query.ContainsKey("secondNumber");
    var operation = context.Request.Query.ContainsKey("operation");

    if (firstNumber && secondNumber && operation)
    {
        var first = Convert.ToInt32(context.Request.Query["firstNumber"]);
        var second = Convert.ToInt32(context.Request.Query["secondNumber"]);
        var _operator = context.Request.Query["operation"];
        long? result = null;

        switch(_operator)
        {
            case "multiply":
                result = first * second;
                break;

            case "divide":
                result = first / second;
                break;

            case "add":
                result = first + second;
                break;

            case "sub":
                result = first - second;
                break;

            case "mod":
                result = first % second;
                break;

            case "default":
                result = null;
                break;

        }

        if(result == null)
        {
            if (context.Response.StatusCode == 200)
                context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid input for 'operation'\n");
        }
        else
        {
            await context.Response.WriteAsync(text: result.ToString());
        }
    }
    else
    {
        if (!firstNumber)
        {
            if(context.Response.StatusCode == 200)
                context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid input for 'firstNumber'\n");
        }
        if(!secondNumber)
        {
            if (context.Response.StatusCode == 200)
                context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid input for 'secondNumber'\n");
        }
        if (!operation)
        {
            if (context.Response.StatusCode == 200)
                context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid input for 'operation'\n");
        }
    }
    
});

app.Run();
