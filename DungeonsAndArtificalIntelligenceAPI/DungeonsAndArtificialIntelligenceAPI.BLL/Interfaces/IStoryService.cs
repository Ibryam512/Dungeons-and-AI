using DungeonsAndArtificialIntelligenceAPI.Data.Entities;
using DungeonsAndArtificialIntelligenceAPI.Models.BindingModels;
using DungeonsAndArtificialIntelligenceAPI.Models.ViewModels;

namespace DungeonsAndArtificialIntelligenceAPI.BLL.Interfaces
{
    public interface IStoryService
    {
        List<StoryViewModel> GetStories();
        List<StoryViewModel> GetStories(Func<Story, bool> predicate);
        StoryViewModel GetStory(Func<Story, bool> predicate);
        void AddStory(StoryBindingModel storyBindingModel);
        void DeleteStory(int id);
    }
}
