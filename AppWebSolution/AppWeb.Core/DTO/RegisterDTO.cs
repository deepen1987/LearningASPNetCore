using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace AppWeb.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Person Name can't be blank")]
        public string PersonName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Incorrect email address.")]
        [Remote(action: "IsEmailAlreadyRegistered", controller: "AccountController", ErrorMessage = "Email is already in use.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pasword can't be blank")]
        public string Password { get; set; } = string.Empty;
    }
}
