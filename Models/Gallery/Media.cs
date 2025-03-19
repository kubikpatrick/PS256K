using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace PS256K.Models.Gallery;

[PrimaryKey(nameof(Id))]
public sealed class Media
{
    public Media()
    {
        
    }

    public Media(string name, string path, string albumId)
    {
        Name = name;
        Path = path;
        AlbumId = albumId;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public string Id { get; internal set; }

    [Required]
    public string Name { get; internal set; }

    [Required]
    public string Path { get; internal set; }

    [Required]
    public string AlbumId { get; internal set; }

    [NotMapped]
    public Album Album { get; internal set; }
}