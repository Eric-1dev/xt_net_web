using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAwards.Entities
{
    public class Award : IHasId
    {
        public Guid Id { get; set; }
        public string Title { get; private set; }

        private Award()
        {
            Id = Guid.NewGuid();
        }

        public Award(string title) : this()
        {
            Title = title;
        }
    }
}
