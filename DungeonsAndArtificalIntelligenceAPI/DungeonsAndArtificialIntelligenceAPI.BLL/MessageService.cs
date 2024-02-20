using AutoMapper;
using DungeonsAndArtificialIntelligenceAPI.BLL.Interfaces;
using DungeonsAndArtificialIntelligenceAPI.Data.Entities;
using DungeonsAndArtificialIntelligenceAPI.Data.Repositories;
using DungeonsAndArtificialIntelligenceAPI.Models.BindingModels;
using DungeonsAndArtificialIntelligenceAPI.Models.ViewModels;

namespace DungeonsAndArtificialIntelligenceAPI.BLL
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> _messageRepository;
        private readonly IMapper _mapper;

        public MessageService(IRepository<Message> messageRepository, IMapper mapper)
        {
            this._messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public List<MessageViewModel> GetMessages(int storyId)
        {
            var messages = this._messageRepository.GetAll(message => message.StoryId == storyId);
            var messagesToShow = this._mapper.Map<List<MessageViewModel>>(messages);

            return messagesToShow;
        }

        public void AddMessage(MessageBindingModel messageBindingModel, int storyId)
        {
            var message = this._mapper.Map<Message>(messageBindingModel);
            message.StoryId = storyId;
            this._messageRepository.Add(message);
        }
    }
}
