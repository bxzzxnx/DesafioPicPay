using Microsoft.EntityFrameworkCore;

namespace DesafioPicPay.Models;
public class DatabaseContext: DbContext
{
    public DbSet<User> User {get;set;}
    public DbSet<Transfer> Transfer {get;set;}

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .Property(w => w.WalletType)
            .HasConversion<string>();

        modelBuilder.Entity<User>()
            .Property(u => u.Balance)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Transfer>()
            .Property(t => t.Value)
            .HasColumnType("decimal(10,2)");
        
        modelBuilder.Entity<User>()
            .HasIndex(u =>  new {u.Email, u.Document})
            .IsUnique();
    }

}