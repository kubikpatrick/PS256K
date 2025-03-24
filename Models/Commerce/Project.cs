using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

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
    public string Description { get; internal set; }

    [Required]
    public string CustomerId { get; internal set; }

    [NotMapped]
    public Customer Customer { get; internal set; }
}