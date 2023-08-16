using IdenAuthSec.Core.Domain.IdentityEntities;
using IdenAuthSec.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdenAuthSec.StartupExtensions
{
    public static class ConfigureServiceExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            
            //2. Enable Identity in the project
            services.AddIdentity<ApplicationUser, ApplicationRole>() // To create User and Role tables
                .AddEntityFrameworkStores<ApplicationDbContext>() // To specify what DB Context to be used here 1 & 2 are application level
                .AddDefaultTokenProviders() // Toekns like OTP / Token eg. email etc..
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>() //What to use for User at Repository level
                .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>(); //Same as above

            // 3.
            services.AddAuthorization(options =>
            {
                // options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser();
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.LogoutPath = "~/Acccount/Login";
            })

            return services;
        }
    }
}
