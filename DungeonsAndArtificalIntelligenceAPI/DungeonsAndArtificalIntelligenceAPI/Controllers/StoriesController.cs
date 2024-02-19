using DungeonsAndArtificialIntelligenceAPI.BLL.Interfaces;
using DungeonsAndArtificialIntelligenceAPI.Models.BindingModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DungeonsAndArtificalIntelligenceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly IStoryService _storyService;

        public StoriesController(IStoryService storyService)
        {
            this._storyService = storyService ?? throw new ArgumentNullException(nameof(storyService));
        }

        [HttpGet]
        public IActionResult GetStories()
        {
            var stories = this._storyService.GetStories();

            return Ok(stories);
        }

        //[Authorize]
        [HttpPost]
        public IActionResult AddStory(StoryBindingModel storyBindingModel)
        {
            this._storyService.AddStory(storyBindingModel);

            return Ok();
        }
    }
}
