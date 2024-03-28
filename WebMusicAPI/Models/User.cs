using Microsoft.EntityFrameworkCore;

namespace WebMusicAPI.Models
{
    public class WebMusicContext : DbContext
    {
        public DbSet<Users> users { get; set; }
        public DbSet<Media> media { get; set; }
        public DbSet<Genre> genre { get; set; }
        public DbSet<Executor> executor { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Media>()
                .HasOne(m => m.User)
                .WithMany(u => u.media)
                .HasForeignKey(m => m.Id_User)
                .IsRequired();

            modelBuilder.Entity<Media>()
                .HasOne(m => m.Genre)
                .WithMany(u => u.media)
                .HasForeignKey(m => m.id_Genre)
                .IsRequired();

            modelBuilder.Entity<Media>()
               .HasOne(m => m.Executor)
               .WithMany(u => u.media)
               .HasForeignKey(m => m.id_Executor)
               .IsRequired();


        }
        public WebMusicContext(DbContextOptions<WebMusicContext> options)
           : base(options)
        {

            if (Database.EnsureCreated())
            {

                users.Add(new Users { FirstName = "lev", LastName = "s", Email = "s", Login = "sss", Password = "d", Salt = "s", Status = 2 });
                media?.Add(new Media { Title = "Богдан", id_Executor = 1, id_Genre = 2, Id_User = 1, Path = "dod"});
                SaveChanges();
            }
        }
        
    }
    public class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public int? Status { get; set; }
        public ICollection<Media>? media { get; set; }
    }
}
