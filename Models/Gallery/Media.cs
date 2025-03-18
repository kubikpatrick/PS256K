using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using PS256K.Models.Identity;

namespace PS256K.Models.Gallery;

[PrimaryKey(nameof(Id))]
public sealed class Media
{
    public Media()
    {
        
    }

    public Media(string name, string hash, MediaType type, string albumId)
    {
        Name = name;
        Hash = hash;
        Type = type;
        AlbumId = albumId;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public string Id { get; internal set; }

    [Required]
    public string Name { get; internal set; }

    [Required]
    public string Hash { get; internal set; }

    [Required]
    public MediaType Type { get; internal set; }

    [Required]
    public string AlbumId { get; internal set; }

    [NotMapped]
    public Album Album { get; internal set; }
}