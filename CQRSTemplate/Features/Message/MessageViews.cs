using CQRSTemplate.Features.User;
using System;

namespace CQRSTemplate.Features.Message
{
    public class MessageViews
    {
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

        public class FullResult : SimpleResult
        {
            public UserViews.SimpleResult User { get; set; }

            public FullResult() { }

            public FullResult(Domain.Message message) : base(message)
            {
                this.User = new UserViews.SimpleResult(message.User);
            }
        }
    }
}
