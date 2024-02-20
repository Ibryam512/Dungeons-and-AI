using DungeonsAndArtificialIntelligenceAPI.Common.Enums;

namespace DungeonsAndArtificialIntelligenceAPI.Models.BindingModels
{
    public class StoryBindingModel
    {
        public Category Category { get; set; }
        public WorldType World { get; set; }
        public Race Race { get; set; }
        public ClassType Class { get; set; }
    }
}
