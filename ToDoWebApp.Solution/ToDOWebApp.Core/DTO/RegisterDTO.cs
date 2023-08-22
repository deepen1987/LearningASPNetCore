using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ToDOWebApp.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "First Name can't be blank.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name can't be blank.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email can't be blank.")]
        [EmailAddress(ErrorMessage = "Email needs to be in proper format.")]
        [Remote(action: "IsEmailAlreadyRegistered", controller: "AccountController", ErrorMessage = "Email is already in use.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password can't be blank.")]
        public string Password { get; set; } = string.Empty;
    }
}
