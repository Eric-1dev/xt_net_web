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
        public string Image { get; set; }

        private Award()
        {
            Id = Guid.NewGuid();
        }

        public Award(string title, string image = null) : this()
        {
            Title = title;
            Image = image;
        }
    }
    public enum AwardCheckStatus
    {
        NULL,
        CORRECT,
        ALLREADY_EXIST,
        INCORRECT_TITLE
    }
}
