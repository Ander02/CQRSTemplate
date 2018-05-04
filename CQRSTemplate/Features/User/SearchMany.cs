using CQRSTemplate.Infraestructure.Database;
using CQRSTemplate.Util;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Features.User
{
    public class SearchMany
    {
        public class Query : IRequest<List<UserViews.FullResult>>
        {
            public string Name { get; set; }
            public int? Age { get; set; }
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

        public class Handler : AsyncRequestHandler<Query, List<UserViews.FullResult>>
        {
            private readonly Db db;

            public Handler(Db db)
            {
                this.db = db;
            }

            protected override async Task<List<UserViews.FullResult>> HandleCore(Query query)
            {
                var q = db.Users.Include(u => u.Messages).AsQueryable();

                if (query.Name != null && query.Name.NotEquals("")) q = q.Where(u => u.Name.Contains(query.Name));

                if (query.Age.HasValue && query.Age.Value > 0) q = q = q.Where(u => u.Age == query.Age.Value);

                q = q.OrderBy(u => u.Name);
                q = q.Skip(query.Limit * query.Page).Take(query.Limit);

                return (await q.ToListAsync()).Select(u => new UserViews.FullResult(u)).ToList();
            }
        }
    }
}
