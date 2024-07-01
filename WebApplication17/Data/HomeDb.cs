using Microsoft.EntityFrameworkCore;
using WebApplication17.Models.Products;
using WebApplication17.Models.Users;

namespace WebApplication17.Data
{
    public class HomeDb : DbContext
    {
        public HomeDb(DbContextOptions<HomeDb> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<UserRegister> Users { get; set; }
        public DbSet<Product> products { get; set; }
    }
}
