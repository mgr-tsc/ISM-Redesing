
using Microsoft.AspNetCore.Mvc;
using ISM_Redesing.DTO;
using ISM_Redesing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;

namespace ISM_Redesign.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "User")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<User>? _userManager;

        public RegisterController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        [Authorize(Roles = "Admin")]
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
            if (registerDTO.Role == "Admin")
                await _userManager.AddToRoleAsync(user, "Admin"); // Add Admin role if the incoming role is Admin
            await _userManager.AddToRoleAsync(user, "User"); // Add User role to all new users
            return Created("Registration successful.", new MessageResponse { Action = "Register new user", Message = "Registration successful" });
        }

    }
}
