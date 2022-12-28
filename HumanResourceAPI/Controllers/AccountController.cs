using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using HumanResourceAPI.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IAuthenticationManager _authenticationManager;
        public AccountController(UserManager<User> userManager, ILoggerManager logger, IMapper mapper, IAuthenticationManager authenticationManager)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authenticationManager = authenticationManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userRegistrationDto)
        {
            var user = _mapper.Map<User>(userRegistrationDto);

            var result = await _userManager.CreateAsync(user, userRegistrationDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            await _userManager.AddToRolesAsync(user, userRegistrationDto.Roles);

            return StatusCode(201);
        }

        [HttpPost(template: "login")]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserAuthenticationDto user)
        {
            if (await _authenticationManager.ValidateUser(user))
            {
                return Ok(new
                {
                    Token = await _authenticationManager.CreateToken()
                });
            }

            return Unauthorized();
        }
    }
}
