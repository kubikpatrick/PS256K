using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;
using PS256K.Models.Commerce;
using PS256K.Models.Gallery;

namespace PS256K.Models.Identity;

public sealed class User : IdentityUser
{
    public User()
    {
        
    }

    public User(string firstName, string lastName, string avatar, DateTime createdAt)
    {
        FirstName = firstName;
        LastName = lastName;
        Avatar = avatar;
        CreatedAt = createdAt;
    }

    [Required]
    public string FirstName { get; internal set; }

    [Required]
    public string LastName { get; internal set; }

    [Required]
    public string Avatar { get; internal set; }

    [Required]
    public DateTime CreatedAt { get; internal set; }

    [NotMapped]
    public List<Connection> Connections { get; internal set; }

    [NotMapped]
    public List<Customer> Customers { get; internal set; }

    [NotMapped]
    public List<Project> Projects { get; internal set; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
}