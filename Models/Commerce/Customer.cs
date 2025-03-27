using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using PS256K.Models.Identity;

namespace PS256K.Models.Commerce;

[PrimaryKey(nameof(Id))]
public sealed class Customer
{
    public Customer()
    {
        
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public string Id { get; internal set; }

    [Required]
    public string FirstName { get; internal set; }

    [Required]
    public string LastName { get; internal set; }

    [EmailAddress]
    [Required]
    public string Email { get; internal set; }

    [Phone]
    public string Phone { get; internal set; }

    [Required]
    public string Occupation { get; internal set; }
    
    public string Avatar { get; internal set; } = "DEFAULT.png";

    [Required]
    public DateTime CreatedAt { get; internal set; }

    [Required]
    public string UserId { get; internal set; }

    [NotMapped]
    public User User { get; internal set; }

    [NotMapped]
    public List<Project> Projects { get; internal set; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
}