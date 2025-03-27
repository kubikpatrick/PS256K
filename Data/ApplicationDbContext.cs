using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using PS256K.Models.Commerce;
using PS256K.Models.Gallery;
using PS256K.Models.Identity;

namespace PS256K.Data;

public sealed class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Picture> Pictures { get; set; }
    public DbSet<Connection> Connections { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasMany(user => user.Projects)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);

        builder.Entity<User>()
            .HasMany(user => user.Customers)
            .WithOne(customer => customer.User)
            .HasForeignKey(customer => customer.UserId);

        builder.Entity<Project>()
            .HasOne(p => p.Customer)
            .WithMany(c => c.Projects)
            .HasForeignKey(p => p.CustomerId);

        builder.Entity<Picture>()
            .HasOne(picture => picture.Project)
            .WithMany(project => project.Pictures)
            .HasForeignKey(picture => picture.ProjectId);

        builder.Entity<Connection>()
            .HasOne(c => c.User)
            .WithMany(u => u.Connections)
            .HasForeignKey(c => c.UserId);

        builder.Entity<Customer>()
            .HasMany(c => c.Projects)
            .WithOne(p => p.Customer)
            .HasForeignKey(p => p.CustomerId);

        base.OnModelCreating(builder);
    }
}