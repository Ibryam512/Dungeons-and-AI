using DungeonsAndArtificialIntelligenceAPI.BLL.Interfaces;
using DungeonsAndArtificialIntelligenceAPI.Models.BindingModels;
using Microsoft.AspNetCore.Mvc;

namespace DungeonsAndArtificalIntelligenceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            this._userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginBindingModel loginBindingModel)
        {
            var response = this._userService.Login(loginBindingModel);

            if (response.Status == "Not found")
                return NotFound();
            else if (response.Status == "Login was unsuccessful")
                return Unauthorized();

            return Ok(response);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterBindingModel registerBindingModel)
        {
            this._userService.Register(registerBindingModel);

            return StatusCode(201);
        }
    }
}
