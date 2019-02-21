using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
 
namespace Wall.Models
{
    public class WallContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public WallContext(DbContextOptions<WallContext> options) : base(options) { }

        public DbSet<Users> Users {get;set;}
        public DbSet<Messages> Messages {get; set;}
        public DbSet<Comments> Comments {get; set;}
        public void createUser(HttpContext context, Users user)
            {
                PasswordHasher<Users> Hasher = new PasswordHasher<Users>();
                user.Password = Hasher.HashPassword(user, user.Password);
                Add(user);
                SaveChanges();
                context.Session.SetInt32("id", user.userId);
            }
    }
}