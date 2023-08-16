using IdenAuthSec.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting(); //Identify action method based route
app.UseAuthentication(); //Reading Identity Cookie
app.UseAuthorization(); // Validate access permissions of the user
app.MapControllers();

app.Run();
