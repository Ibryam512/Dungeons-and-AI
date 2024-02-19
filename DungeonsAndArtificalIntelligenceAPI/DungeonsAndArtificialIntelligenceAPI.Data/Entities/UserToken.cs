using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndArtificialIntelligenceAPI.Data.Entities
{
    public class UserToken : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Token { get; set; }
    }
}
