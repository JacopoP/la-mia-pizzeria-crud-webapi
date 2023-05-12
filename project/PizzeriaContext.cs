using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using project.Models;

namespace project
{
    public class PizzeriaContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Pizza> pizze { get; set; }        
        public DbSet<Category> categories { get; set; }
        public DbSet<Ingrediente> ingredienti { get; set; }

        public PizzeriaContext() { }
        public PizzeriaContext(DbContextOptions<PizzeriaContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost\\MSSQLSERVER01;Initial Catalog=Pizzeria;Integrated Security=True; TrustServerCertificate=True");
        }
    }
}
