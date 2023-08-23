using ToDoWebApp.WebApi.StartupExtnsions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
