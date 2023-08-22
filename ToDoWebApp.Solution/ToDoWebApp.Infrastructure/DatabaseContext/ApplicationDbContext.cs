using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDOWebApp.Core.Domain.Entities.Identity;

namespace ToDoWebApp.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public ApplicationDbContext() { }

        //Declare the DbSets here for all the entities which we would have in the project


        //Define the dummy data here using OnModelCreating
    }
}
