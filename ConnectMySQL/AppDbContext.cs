using Microsoft.EntityFrameworkCore;

namespace ConnectMySQL
{
    public class AppDbContext:DbContext
    {
        public DbSet<School> Schools { get; set; }
        
        public DbSet<Class> Classes { get; set; }
        
        public DbSet<Student> Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "Server=localhost;Database=SchollManagement;User=root;Password=Mramra1234!@;");
        }
    }
}