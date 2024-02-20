using DungeonsAndArtificialIntelligenceAPI.BLL.Interfaces;
using DungeonsAndArtificialIntelligenceAPI.Models.BindingModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DungeonsAndArtificalIntelligenceAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            this._messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
        }

        [HttpGet]
        public IActionResult GetMessages([FromQuery] int storyId)
        {
            var messages = this._messageService.GetMessages(storyId);

            return Ok(messages);
        }

        [HttpPost]
        public IActionResult AddMessage([FromQuery] int storyId, [FromBody] MessageBindingModel messageBindingModel)
        {
            this._messageService.AddMessage(messageBindingModel, storyId);

            return StatusCode(201);
        }
    }
}
