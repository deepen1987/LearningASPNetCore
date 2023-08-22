using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDOWebApp.Core.Domain.Entities.Identity;
using ToDOWebApp.Core.DTO;
using ToDOWebApp.Core.ServiceContracts;

namespace ToDoWebApp.WebApi.Controllers
{
    [AllowAnonymous]
    public class AccountController : CustomControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwtService;
        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            RoleManager<ApplicationRole> roleManager,
            IJwtService jwtService) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }


        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> PostRegister(RegisterDTO registerDTO)
        {
            if(ModelState.IsValid == false)
            {
                string errorMessages = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessages);
            }

            //Create User
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                PersonName = registerDTO.FirstName + " " + registerDTO.LastName,
                UserName = registerDTO.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                var response = _jwtService.CreateJwtToken(user);

                return Ok(response);
            }

            string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
            return Problem(errorMessage);

        }

        [HttpPost("login")]
        public async Task<IActionResult> PostLogin(LoginDTO loginDTO)
        {
            if(ModelState.IsValid == false)
            {
                string errorMessages = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

                return Problem(errorMessages);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if(result.Succeeded)
            {
                ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);

                if (user == null)
                {
                    return NoContent();
                }

                var response = _jwtService.CreateJwtToken(user);
                return Ok(response);
            }

            return Problem("Invalid Email and Password");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> GetLogout()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if(user == null)
            {
                return Ok(true);
            }

            return Ok(false);
        }

    }
}
