namespace DungeonsAndArtificialIntelligenceAPI.Data.Entities
{
    public class Story : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public List<Message> Messages { get; set; }
    }
}
