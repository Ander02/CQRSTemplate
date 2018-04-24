using CQRSTemplate.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Domain
{
    public class Message : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        internal class SearchMany
        {
        }
    }
}
