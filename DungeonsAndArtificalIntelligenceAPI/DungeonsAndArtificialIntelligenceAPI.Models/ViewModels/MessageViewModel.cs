namespace DungeonsAndArtificialIntelligenceAPI.Models.ViewModels
{
    public class MessageViewModel
    {
        public string Text { get; set; }
        public bool SentByAI { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
