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
    public DbSet<Media> Medias { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasMany(user => user.Albums)
            .WithOne(album => album.User)
            .HasForeignKey(album => album.UserId);

        builder.Entity<Album>()
            .HasMany(album => album.Medias)
            .WithOne(media => media.Album)
            .HasForeignKey(media => media.AlbumId);
        
        builder.Entity<Media>()
            .HasOne(media => media.Album)
            .WithMany(album => album.Medias)
            .HasForeignKey(media => media.AlbumId);

        base.OnModelCreating(builder);
    }
}