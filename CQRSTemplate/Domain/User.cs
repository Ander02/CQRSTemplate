using System;
using System.Collections.Generic;

namespace CQRSTemplate.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public int Age { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
