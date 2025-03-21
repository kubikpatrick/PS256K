using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using PS256K.Models.Gallery;
using PS256K.Models.Identity;

namespace PS256K.Data;

public sealed class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Picture> Pictures { get; set; }
    public DbSet<Connection> Connections { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasMany(user => user.Albums)
            .WithOne(album => album.User)
            .HasForeignKey(album => album.UserId);

        builder.Entity<Album>()
            .HasMany(album => album.Pictures)
            .WithOne(picture => picture.Album)
            .HasForeignKey(picture => picture.AlbumId);
        
        builder.Entity<Picture>()
            .HasOne(picture => picture.Album)
            .WithMany(album => album.Pictures)
            .HasForeignKey(picture => picture.AlbumId);

        builder.Entity<Favorite>()
            .HasOne(f => f.Album)
            .WithMany(a => a.Favorites)
            .HasForeignKey(f => f.AlbumId);

        builder.Entity<Favorite>()
            .HasOne(f => f.User)
            .WithMany(u => u.Favorites)
            .HasForeignKey(f => f.UserId);

        builder.Entity<Connection>()
            .HasOne(c => c.User)
            .WithMany(u => u.Connections)
            .HasForeignKey(c => c.UserId);

        base.OnModelCreating(builder);
    }
}