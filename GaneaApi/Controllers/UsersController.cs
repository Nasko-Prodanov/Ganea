using Application.Common.Models.User;
using Application.Common.Services;
using Infrastructure.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace GaneaApi.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IdentityService identity;

        public UsersController(IdentityService identity)
        {
            this.identity = identity;
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
                await identity.CreateUserAsync(user, cancellationToken);
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
            AuthResponse response = await identity.AuthenticateAsync(model, token);

            return response;
        }
    }
}

