using AppWeb.Core.Domain.Entities.Identity;
using AppWeb.Core.DTO;
using AppWeb.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppWeb.WebAPI.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [AllowAnonymous]
    [ApiVersion("1.0")]
    public class AccountController : CustomControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwtService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="jwtService"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
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
                UserName = registerDTO.Email,
                PersonName = registerDTO.PersonName
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                var authenticationResponse = _jwtService.CreateJwtToken(user);
                user.RefreshToken = authenticationResponse.RefreshToken;
                user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDateTime;
                await _userManager.UpdateAsync(user);

                return Ok(authenticationResponse);
            }
            else
            {
                string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
                return Problem(errorMessage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> PostLogin(LoginDTO loginDTO)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessages = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

                return Problem(errorMessages);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);
                if (user == null)
                {
                    return NoContent();
                }

                var authenticationResponse = _jwtService.CreateJwtToken(user);

                return Ok(authenticationResponse);
            }

            return Problem("Invalid email and password");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]
        public async Task<IActionResult> GetLogout()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if(user == null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }
}
