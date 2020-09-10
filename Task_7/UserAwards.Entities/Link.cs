using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAwards.Entities
{
    public class Link : IHasId
    {
        public Guid Id { get; set; }
        public Guid UserId { get; private set; }
        public Guid AwardId { get; private set; }
        private Link()
        {
            Id = Guid.NewGuid();
        }
        public Link(Guid userId, Guid awardId) : this()
        {
            UserId = userId;
            AwardId = awardId;
        }
    }
}
