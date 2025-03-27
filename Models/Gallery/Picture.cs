using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using PS256K.Models.Commerce;

namespace PS256K.Models.Gallery;

[PrimaryKey(nameof(Id))]
public sealed class Picture
{
    public Picture()
    {
        
    }

    public Picture(string name, string path)
    {
        Name = name;
        Path = path;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public string Id { get; internal set; }

    [Required]
    public string Name { get; internal set; }

    [Required]
    public string Path { get; internal set; }

    public string? Longitude { get; internal set; }
    
    public string? Latitude { get; internal set; }

    [Required]
    public string ProjectId { get; internal set; }
    
    [NotMapped]
    public Project Project { get; internal set; }

    [NotMapped]
    public bool HasGeolocation => !string.IsNullOrEmpty(Longitude) && !string.IsNullOrEmpty(Latitude);
}