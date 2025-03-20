using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PS256K.Models.REST;

public sealed class LoginModel
{
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }

    [DisplayName("Remember me")]
    public bool RememberMe { get; set; } = false;
}