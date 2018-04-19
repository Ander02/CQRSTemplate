using CQRSTemplate.Features.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Features.Message
{
    public class MessageViews
    {
        public class FullResult
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }

            public virtual UserViews.SimpleResult User { get; set; }

            public FullResult() { }

            public FullResult(Domain.Message message)
            {
                this.Id = message.Id;
                this.Title = message.Title;
                this.Content = message.Content;
                this.User = new UserViews.SimpleResult(message.User);
            }
        }

        public class SimpleResult
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }

            public SimpleResult() { }

            public SimpleResult(Domain.Message message)
            {
                this.Id = message.Id;
                this.Title = message.Title;
                this.Content = message.Content;
            }
        }
    }
}
