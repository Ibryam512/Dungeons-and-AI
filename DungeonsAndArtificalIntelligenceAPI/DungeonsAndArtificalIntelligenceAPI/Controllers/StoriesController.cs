using DungeonsAndArtificialIntelligenceAPI.BLL.Interfaces;
using DungeonsAndArtificialIntelligenceAPI.Models.BindingModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DungeonsAndArtificalIntelligenceAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly IStoryService _storyService;

        public StoriesController(IStoryService storyService)
        {
            this._storyService = storyService ?? throw new ArgumentNullException(nameof(storyService));
        }

        [HttpGet]
        public IActionResult GetStories([FromQuery] string token)
        {
            var stories = this._storyService.GetStoriesById(token);

            return Ok(stories);
        }

        [HttpPost]
        public IActionResult AddStory([FromQuery] string token, [FromBody] StoryBindingModel storyBindingModel)
        {
            this._storyService.AddStory(storyBindingModel, token);
            var story = this._storyService.GetStories().OrderByDescending(x => x.CreationDate).First();

            return Created("", story);
        }
    }
}
