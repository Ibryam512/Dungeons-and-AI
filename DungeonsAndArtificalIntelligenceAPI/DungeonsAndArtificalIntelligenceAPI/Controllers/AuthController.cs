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
            var token = this._userService.Login(loginBindingModel);

            return Ok(token);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterBindingModel registerBindingModel)
        {
            this._userService.Register(registerBindingModel);

            return Ok(); //TODO: return 201
        }
    }
}
