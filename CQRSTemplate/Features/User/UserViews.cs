using CQRSTemplate.Features.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Features.User
{
    public class UserViews
    {
        public class SimpleResult
        {
            public Guid Id { get; set; }
            public int Age { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }

            public SimpleResult() { }

            public SimpleResult(Domain.User user)
            {
                this.Id = user.Id;
                this.Age = user.Age;
                this.Email = user.Email;
                this.Name = user.Name;
            }
        }

        public class FullResult : SimpleResult
        {
            public virtual ICollection<MessageViews.SimpleResult> Messages { get; set; }

            public FullResult() { }

            public FullResult(Domain.User user)
            {
                this.Id = user.Id;
                this.Age = user.Age;
                this.Email = user.Email;
                this.Name = user.Name;
                this.Messages = user.Messages.Select(u => new MessageViews.SimpleResult(u)).ToList();
            }
        }
    }
}
