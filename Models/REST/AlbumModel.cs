using System.ComponentModel.DataAnnotations;

namespace PS256K.Models.REST;

public sealed class AlbumModel
{
    [Required]
    public string Name { get; set; }

    public bool IsPublic { get; set; }

    // public List<IFormFile> Files { get; set; }
}