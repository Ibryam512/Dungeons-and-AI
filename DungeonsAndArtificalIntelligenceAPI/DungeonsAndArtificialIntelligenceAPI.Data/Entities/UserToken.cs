namespace DungeonsAndArtificialIntelligenceAPI.Data.Entities
{
    public class UserToken : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Token { get; set; }
    }
}
