using CQRSTemplate.Features.Message;
using System;
using System.Collections.Generic;
using System.Linq;

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
            public ICollection<MessageViews.SimpleResult> Messages { get; set; }

            public FullResult() { }

            public FullResult(Domain.User user) : base(user)
            {
                if (user.Messages != null)
                {
                    this.Messages = user.Messages.Select(m => new MessageViews.SimpleResult(m)).ToList();
                }
                else
                {
                    this.Messages = new List<MessageViews.SimpleResult>();
                }
            }
        }
    }
}
