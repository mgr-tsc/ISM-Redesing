
using Microsoft.AspNetCore.Mvc;
using ISM_Redesing.DTO;
using ISM_Redesing.Models;
using Microsoft.AspNetCore.Identity;

namespace ISM_Redesign.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<User>? _userManager;

        public RegisterController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        [HttpPost]
        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <remarks>
        /// This endpoint is used to create a new user account. It requires a username, email, and password.
        /// </remarks>
        /// <param name="registerDTO">The registration data transfer object containing the username, email, and password.</param>
        /// <response code="201">Returns a message response indicating successful registration.</response>
        /// <response code="400">If the user registration fails or if there is an issue with the user manager service.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MessageResponse>> Register([FromBody] RegisterDTO registerDTO)
        {
            User user = new User
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
            };
            if (_userManager == null) return BadRequest("User manager service not working");
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return Created("Registration successful.", new MessageResponse { Action = "Register new user", Message = "Registration successful" });
        }

    }
}
