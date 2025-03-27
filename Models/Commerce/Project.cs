using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using PS256K.Models.Gallery;
using PS256K.Models.Identity;

namespace PS256K.Models.Commerce;

[PrimaryKey(nameof(Id))]
public sealed class Project
{
    public Project()
    {
        
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public string Id { get; internal set; }

    [Required]
    public string Name { get; internal set; }

    [Required]
    public string ShareLink { get; internal set; } = Guid.NewGuid().ToString();

    [Required]
    public DateTime CreatedAt { get; internal set; }

    [Required]
    public string CustomerId { get; internal set; }

    [Required]
    public string UserId { get; internal set; }

    [NotMapped]
    public Customer Customer { get; internal set; }

    [NotMapped]
    public User User { get; internal set; }

    [NotMapped]
    public List<Picture> Pictures { get; internal set; }

    [NotMapped]
    public bool IsEmpty => Pictures.Count == 0;
}