namespace DungeonsAndArtificialIntelligenceAPI.Data.Entities
{
    public class Message : BaseEntity
    {
        public string Text { get; set; }
        public int StoryId { get; set; }
        public virtual Story Story { get; set; }
        public bool SentByAI { get; set; }  
    }
}
