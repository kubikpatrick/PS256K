using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using PS256K.Models.Identity;

namespace PS256K.Models.Gallery;

[PrimaryKey(nameof(Id))]
public sealed class Favorite
{
    public Favorite()
    {
        
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public string Id { get; internal set; }

    [Required]
    public DateTime CreatedAt { get; internal set; }

    [Required]
    public string AlbumId { get; internal set; }

    [Required]
    public string UserId { get; internal set; }

    [NotMapped]
    public Album Album { get; internal set; }

    [NotMapped]
    public User User { get; internal set; }
}
