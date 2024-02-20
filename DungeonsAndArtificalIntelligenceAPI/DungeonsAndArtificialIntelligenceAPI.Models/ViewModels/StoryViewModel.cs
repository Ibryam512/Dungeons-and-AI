using DungeonsAndArtificialIntelligenceAPI.Common.Enums;

namespace DungeonsAndArtificialIntelligenceAPI.Models.ViewModels
{
    public class StoryViewModel
    {
        public int Id { get; set; }
        public List<MessageViewModel> Messages { get; set; }
        public Category Category { get; set; }
        public WorldType World { get; set; }
        public Race Race { get; set; }
        public ClassType Class { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
