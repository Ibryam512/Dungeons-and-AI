using DungeonsAndArtificialIntelligenceAPI.Common.Enums;

namespace DungeonsAndArtificialIntelligenceAPI.Data.Entities
{
    public class Story : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public List<Message> Messages { get; set; }
        public Category Category { get; set; }
        public WorldType World { get; set; }
        public Race Race { get; set; }
        public ClassType Class { get; set; }
    }
}
