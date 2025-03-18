using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

using PS256K.Models.Gallery;

namespace PS256K.Models.Identity;

public sealed class User : IdentityUser
{
    public User()
    {
        
    }

    public User(string firstName, string lastName, string avatar)
    {
        FirstName = firstName;
        LastName = lastName;
        Avatar = avatar;
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
    public List<Album> Albums { get; internal set; }
}