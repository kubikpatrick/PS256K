using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PS256K.Models.REST;

public sealed class CustomerModel
{   
    [DisplayName("First name")]
    [Required]
    public string FirstName { get; set; }

    [DisplayName("Last name")]
    [Required]
    public string LastName { get; set; }

    [DataType(DataType.EmailAddress)]
    [Required]
    public string Email { get; set; }

    [DataType(DataType.PhoneNumber)]
    [Required]  
    public string Phone { get; set; }

    [Required]
    public string Occupation { get; set; }

    public IFormFile? File { get; set; }
}