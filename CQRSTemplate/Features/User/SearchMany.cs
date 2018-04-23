using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CQRSTemplate.Infraestructure;
using CQRSTemplate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSTemplate.Database.Repository.Interface;

namespace CQRSTemplate.Features.User
{
    public class SearchMany
    {
        public class Query : IRequest<List<UserViews.FullResult>>
        {
            public string Name { get; set; } = "";
            public int Age { get; set; }
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
            private readonly IUserRepository userRepository;

            public Handler(IUserRepository userRepository)
            {
                this.userRepository = userRepository;
            }

            protected override async Task<List<UserViews.FullResult>> HandleCore(Query query)
            {
                var q = userRepository.GetEntityQuery();

                if (query.Name != null || query.Name != "")
                {
                    q = userRepository.QueryFindByName(q, query.Name);
                }
                if (query.Age > 0)
                {
                    q = userRepository.QueryFindByAge(q, query.Age);
                }

                q = q.OrderBy(u => u.Name);
                q = q.Skip(query.Limit * query.Page).Take(query.Limit);

                return (await q.ToListAsync()).Select(u => new UserViews.FullResult(u)).ToList();
            }
        }
    }
}
