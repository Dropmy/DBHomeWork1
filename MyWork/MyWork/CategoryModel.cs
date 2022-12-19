using Microsoft.EntityFrameworkCore;

namespace MyWork
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string? Category { get; set; }
        public int Discount { get; set; }
    }

    public class CategoryContext : DbContext
    {
        public DbSet<CategoryModel> Categories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Привязка контекста к существующей бд
            optionsBuilder.UseMySql("Server=MYSQL8001.site4now.net;Database=db_a8e04e_user090;Uid=a8e04e_user090;Pwd=qwerty5656;",
            new MySqlServerVersion(new Version(5,0)));
        }
    }
}
