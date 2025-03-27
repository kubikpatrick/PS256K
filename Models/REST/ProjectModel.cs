using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PS256K.Models.REST;

public sealed class ProjectModel
{
    [Required(ErrorMessage = "Enter a project name.")]
    public string Name { get; set; }
}