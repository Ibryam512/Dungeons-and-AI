namespace DungeonsAndArtificialIntelligenceAPI.Data.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public List<Story> Stories { get; set; }
        public List<UserToken> Tokens { get; set; }
    }
}
