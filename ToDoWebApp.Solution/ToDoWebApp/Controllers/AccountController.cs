using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using ToDOWebApp.Core.Domain.Entities.Identity;
using ToDOWebApp.Core.DTO;
using ToDOWebApp.Core.ServiceContracts;

namespace ToDoWebApp.WebApi.Controllers
{
    /// <summary>
    /// The [AllowAnonymous] is added so the controller can be accessed without authenticaation and authorization 
    /// as we need it so the controller can be accessed by frontend without issue.
    /// </summary>
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
                var authenticationResponse = _jwtService.CreateJwtToken(user);
                user.RefreshToken = authenticationResponse.RefreshToken;
                user.RefreshTokenExpirationTime = authenticationResponse.RefreshTokenExpirationTime;

                await _userManager.UpdateAsync(user);
                

                return Ok(authenticationResponse);
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
                user.RefreshToken = response.RefreshToken;
                user.RefreshTokenExpirationTime = response.RefreshTokenExpirationTime;

                await _userManager.UpdateAsync(user);
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

        [HttpPost("generate-new-jwt-token")]
        public async Task<IActionResult> GenerateNewRefreshToken(TokenModel tokenModel)
        {
            if(ModelState.IsValid == false)
            {
                string? errorMessages = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

                return Problem(errorMessages);
            }


            ClaimsPrincipal? claimsPrincipal = _jwtService.GetPrincipalFromJwtToken(tokenModel.Token);
            if(claimsPrincipal == null)
            {
                return BadRequest("Invalid Refresh Token");
            }

            string? email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

            if(email == null)
            {
                return BadRequest("Invalid Refresh Token");
            }

            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if(user == null ||
                user.RefreshToken != tokenModel.RefreshToken ||
                user.RefreshTokenExpirationTime <= DateTime.Now)
            {
                return BadRequest("Invalid Refresh Token");
            }

            AuthenticationResponse authenticationResponse = _jwtService.CreateJwtToken(user);
            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpirationTime = authenticationResponse.RefreshTokenExpirationTime;

            await _userManager.UpdateAsync(user);

            return Ok(authenticationResponse);
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
