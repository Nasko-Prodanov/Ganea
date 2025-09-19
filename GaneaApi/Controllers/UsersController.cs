using Application.Common.Interfaces;
using Application.Common.Models.ChangePassword;
using Application.Common.Models.User;
using Application.Common.Services;
using Infrastructure.Common.Models;
using Infrastructure.Persistance.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GaneaApi.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IIdentityService identityService;
        private readonly ICurrentUserService currentUserService;

        public UsersController(IdentityService identity,ICurrentUserService currentUserService)
        {
            this.identityService = identity;
            this.currentUserService = currentUserService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserModel user, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                await identityService.CreateUserAsync(user, cancellationToken);
            }
            catch (Exception)
            {
                return BadRequest("User registration failed");
            }

            return Created();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginInputModel model, CancellationToken token)
        {
            AuthResponse response = await identityService.AuthenticateAsync(model, token);

            return response;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ChangePasswordDto model, string userId)
        {
            IdentityResult result = await identityService.ResetPasswordAsync(userId, model.NewPassword);
            
            if (!result.Succeeded)
            {
                return BadRequest(string.Join(", ", result.Errors));
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            IdentityResult result = await identityService.ChangeCurrentUserPasswordAsync(currentUserService.UserId , model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(string.Join(", ", result.Errors));
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/SetRole")]
        public async Task<ActionResult> SetRole([FromRoute] string id, [FromBody] Role role)
        {
            IdentityResult result = await identityService.SetUserRoleAsync(id, role);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join(", ", result.Errors));
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult<IdentityResult>> DeleteUser(string userId)
        {
            IdentityResult result = await identityService.DeleteUserAsync(userId);

            if (!result.Succeeded)
            {
                return BadRequest(string.Join(", ", result.Errors));
            }

            return result;
        }
    }
}

