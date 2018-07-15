using Microsoft.EntityFrameworkCore;//add this dependency

namespace wall.Models
{
    public class YourContext : DbContext
    {
        public YourContext(DbContextOptions<YourContext> options) : base(options) { }
        public DbSet<User> users { get; set; } //Users = the table name
        //<Person> is the class model that will link to the database
        public DbSet<Message> messages { get; set; }
        public DbSet<Comment> comments { get; set; }
    }
}