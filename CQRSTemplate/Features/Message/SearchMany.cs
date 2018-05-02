using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CQRSTemplate.Infraestructure;
using CQRSTemplate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSTemplate.Infraestructure.Database;

namespace CQRSTemplate.Features.Message
{
    public class SearchMany
    {
        public class Query : IRequest<List<MessageViews.FullResult>>
        {
            public string Title { get; set; } = "";
            public string Content { get; set; } = "";
            public Guid? UserId { get; set; }
            public int Limit { get; set; } = 100;
            public int Page { get; set; } = 0;
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(u => u.Limit).GreaterThanOrEqualTo(1);
                RuleFor(u => u.Page).GreaterThanOrEqualTo(0);
            }
        }

        public class Handler : AsyncRequestHandler<Query, List<MessageViews.FullResult>>
        {
            private readonly Db db;

            public Handler(Db db)
            {
                this.db = db;
            }

            protected override async Task<List<MessageViews.FullResult>> HandleCore(Query query)
            {
                var q = db.Messages.Include(m => m.User).AsQueryable();

                if (query.Title != null && query.Title.NotEquals("")) q = q.Where(m => m.Title.Contains(query.Title));

                if (query.Content != null && query.Content.NotEquals("")) q = q.Where(m => m.Content.Contains(query.Content));

                if (query.UserId.HasValue) q = q.Where(m => m.UserId.Equals(query.UserId));

                q = q.OrderBy(m => m.Title);
                q = q.Skip(query.Limit * query.Page).Take(query.Limit);

                return (await q.ToListAsync()).Select(m => new MessageViews.FullResult(m)).ToList();
            }
        }
    }
}
