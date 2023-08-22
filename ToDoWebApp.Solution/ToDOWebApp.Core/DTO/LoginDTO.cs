using System.ComponentModel.DataAnnotations;

namespace ToDOWebApp.Core.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email can't be blank.")]
        [EmailAddress(ErrorMessage = "Email Id has to follow the proper email standards.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password can't be blank.")]
        public string Password { get; set; } = string.Empty;
    }
}
