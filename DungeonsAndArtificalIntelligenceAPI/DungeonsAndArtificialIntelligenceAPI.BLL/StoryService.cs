using AutoMapper;
using DungeonsAndArtificialIntelligenceAPI.BLL.Interfaces;
using DungeonsAndArtificialIntelligenceAPI.Data.Entities;
using DungeonsAndArtificialIntelligenceAPI.Data.Repositories;
using DungeonsAndArtificialIntelligenceAPI.Models.BindingModels;
using DungeonsAndArtificialIntelligenceAPI.Models.ViewModels;

namespace DungeonsAndArtificialIntelligenceAPI.BLL
{
    public class StoryService : IStoryService
    {
        private readonly IRepository<Story> _storyRepository;
        private readonly IRepository<UserToken> _userTokenRepository;
        private readonly IMapper _mapper;

        public StoryService(IRepository<Story> storyRepository, IRepository<UserToken> userTokenRepository, IMapper mapper)
        {
            this._storyRepository = storyRepository ?? throw new ArgumentNullException(nameof(storyRepository));
            this._userTokenRepository = userTokenRepository ?? throw new ArgumentNullException(nameof(userTokenRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public List<StoryViewModel> GetStoriesById(string token)
        {
            int id = GetIdWithToken(token);
            return GetStories(story => story.UserId == id);
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

        public void AddStory(StoryBindingModel storyBindingModel, string token)
        {
            var story = this._mapper.Map<Story>(storyBindingModel);
            story.UserId = GetIdWithToken(token);
            this._storyRepository.Add(story);
        }

        public void DeleteStory(int id)
        {
            var story = this._storyRepository.Get(story => story.Id == id);
            this._storyRepository.Delete(story);
        }

        private int GetIdWithToken(string token)
        {
            var data = this._userTokenRepository.GetAll(x => x.Token == token);
            return data.Last().UserId;
        }
    }
}
