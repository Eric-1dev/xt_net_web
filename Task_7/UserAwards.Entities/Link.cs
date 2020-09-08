using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAwards.Entities
{
    public class Link : IHasId
    {
        public Guid Id { get; private set; }
        public int UserId { get; private set; }
        public int AwardId { get; private set; }
        private Link()
        {
            Id = Guid.NewGuid();
        }
        public Link(int userId, int awardId) : base()
        {
            UserId = userId;
            AwardId = awardId;
        }
    }
}
