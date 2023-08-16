using AppWeb.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppWeb.WebAPI.StartupExtensions
{
    public static class ConfigureServiceExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddControllers(options =>
            {
                // This 2 would specify that this Web API accepts and response types is only application/json globally.
                options.Filters.Add(new ProducesAttribute("application/json"));
                options.Filters.Add(new ConsumesAttribute("application/json"));
            })
                .AddXmlDataContractSerializerFormatters();

            services.AddApiVersioning(config =>
            {
                config.ApiVersionReader = new UrlSegmentApiVersionReader(); // eeded to identify correct url of the version need to be used, hw as we have to specify version as part of route.
                                                                            //config.ApiVersionReader = new QueryStringApiVersionReader();
                                                                            //config.ApiVersionReader = new HeaderApiVersionReader("api-version");
                                                                            //config.DefaultApiVersion = new ApiVersion(1, 0);
                                                                            //config.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //Swagger 
            services.AddEndpointsApiExplorer(); // It enables to read metdata
            services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));

                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Cities Web API",
                    Version = "1.0"
                });

                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Cities Web API",
                    Version = "2.0"
                });
            }); //Generates OpenAPI specification and to read xml documentation from the action methods

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}
