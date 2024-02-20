using DungeonsAndArtificialIntelligenceAPI.Data.Entities;
using DungeonsAndArtificialIntelligenceAPI.Models.BindingModels;
using DungeonsAndArtificialIntelligenceAPI.Models.ViewModels;

namespace DungeonsAndArtificialIntelligenceAPI.BLL.Interfaces
{
    public interface IStoryService
    {
        List<StoryViewModel> GetStoriesById(string token);
        List<StoryViewModel> GetStories();
        List<StoryViewModel> GetStories(Func<Story, bool> predicate);
        StoryViewModel GetStory(Func<Story, bool> predicate);
        void AddStory(StoryBindingModel storyBindingModel, string token);
        void DeleteStory(int id);
    }
}
