using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace PS256K.Models.Identity;

[PrimaryKey(nameof(Id))]
public sealed class Connection
{
    public Connection()
    {
        
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public string Id { get; internal set; }

    [Required]
    public ConnectionState State { get; internal set; }

    [Required]
    public DateTime CreatedAt { get; internal set; }

    [Required]
    public string UserId { get; internal set; }

    [NotMapped]
    public User User { get; internal set; }
}
