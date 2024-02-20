using DungeonsAndArtificialIntelligenceAPI.Models.BindingModels;
using DungeonsAndArtificialIntelligenceAPI.Models.ViewModels;

namespace DungeonsAndArtificialIntelligenceAPI.BLL.Interfaces
{
    public interface IMessageService
    {
        List<MessageViewModel> GetMessages(int storyId);
        void AddMessage(MessageBindingModel messageBindingModel, int storyId);
    }
}
