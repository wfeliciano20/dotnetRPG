using dotnetRPG.Models;
using Microsoft.EntityFrameworkCore;


namespace dotnetRPG.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Character> Characters => Set<Character>();
    }
}