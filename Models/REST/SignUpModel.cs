using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PS256K.Models.REST;

public sealed class SignUpModel
{   
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }

    [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
    [DataType(DataType.Password)]
    [DisplayName("Confirm password")]
    [Required(ErrorMessage = "Password must be confirmed.")]
    public string ConfirmPassword { get; set; }

    [DisplayName("First name")]
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; }

    [DisplayName("Last name")]
    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; }
}