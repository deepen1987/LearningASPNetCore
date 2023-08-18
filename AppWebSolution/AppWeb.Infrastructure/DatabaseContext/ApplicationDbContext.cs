using AppWeb.Core.Domain.Entities;
using AppWeb.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppWeb.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public ApplicationDbContext()
        {

        }

        public virtual DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().HasData(new City() { Id = Guid.Parse("93E3497F-2699-4E5B-B428-74EDD3FDB642"), CityName = "Denver" });
            modelBuilder.Entity<City>().HasData(new City() { Id = Guid.Parse("B9BEBBE2-D4BC-4589-87AD-17BECF4318A7"), CityName = "Boston" });
            modelBuilder.Entity<City>().HasData(new City() { Id = Guid.Parse("EDB4CA39-2D6E-42F0-8BC1-15942BEF1711"), CityName = "New York" });
        }

        internal Task FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
