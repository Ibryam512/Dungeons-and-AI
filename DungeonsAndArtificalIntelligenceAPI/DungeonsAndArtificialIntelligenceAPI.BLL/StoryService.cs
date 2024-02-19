using AutoMapper;
using DungeonsAndArtificialIntelligenceAPI.BLL.Interfaces;
using DungeonsAndArtificialIntelligenceAPI.Data.Entities;
using DungeonsAndArtificialIntelligenceAPI.Data.Repositories;
using DungeonsAndArtificialIntelligenceAPI.Models.BindingModels;
using DungeonsAndArtificialIntelligenceAPI.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DungeonsAndArtificialIntelligenceAPI.BLL
{
    public class StoryService : IStoryService
    {
        private readonly IRepository<Story> _storyRepository;
        private readonly IMapper _mapper;

        public StoryService(IRepository<Story> storyRepository, IMapper mapper)
        {
            this._storyRepository = storyRepository ?? throw new ArgumentNullException(nameof(storyRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public List<StoryViewModel> GetStories()
        {
            var stories = this._storyRepository.GetAll().ToList();
            return this._mapper.Map<List<StoryViewModel>>(stories);
        }

        public List<StoryViewModel> GetStories(Func<Story, bool> predicate)
        {
            var stories = this._storyRepository.GetAll(predicate).ToList();
            return this._mapper.Map<List<StoryViewModel>>(stories);
        }

        public StoryViewModel GetStory(Func<Story, bool> predicate)
        {
            var story = this._storyRepository.Get(predicate);
            return this._mapper.Map<StoryViewModel>(story);
        }

        public void AddStory(StoryBindingModel storyBindingModel)
        {
            var story = this._mapper.Map<Story>(storyBindingModel);
            this._storyRepository.Add(story);
        }

        public void DeleteStory(int id)
        {
            var story = this._storyRepository.Get(story => story.Id == id);
            this._storyRepository.Delete(story);
        }
    }
}
