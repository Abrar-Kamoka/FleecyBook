using FleecyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace FleecyBook.DataAccess;             //install first Package Microsoft.EntityFrameworkCore  _3

                              //WE DOING CODE 1st APRROACH 🙌❤️
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Product> Products { get; set; }

}