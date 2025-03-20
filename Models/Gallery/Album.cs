using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using PS256K.Models.Identity;

namespace PS256K.Models.Gallery;

[PrimaryKey(nameof(Id))]
public sealed class Album
{
    public Album()
    {
        
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public string Id { get; internal set; }

    [Required]
    public string Name { get; internal set; }

    [Required]
    public bool IsPublic { get; internal set; } = false;

    [Required]
    public DateTime CreatedAt { get; internal set; }

    [Required]
    public string UserId { get; internal set; }

    [NotMapped]
    public User User { get; internal set; }

    [NotMapped]
    public List<Picture> Pictures { get; internal set; }

    [NotMapped]
    public List<Favorite> Favorites { get; internal set; }

    [Required]
    public string Cover => Pictures.First().Path ?? "empty-cover.png";
}