using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SQRSTemplate.Infraestructure;
using SQRSTemplate.Infraestructure.Database;
using SQRSTemplate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQRSTemplate.Features.Sample
{
    public class SearchOneSample
    {
        public class Query : IRequest<Result>
        {
            public Guid Id { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(q => q.Id).NotNull().NotEmpty();
            }
        }

        public class Result
        {
            public Guid Id { get; set; }
            public string Content { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, Result>
        {
            private readonly Db db;

            public Handler(Db db)
            {
                this.db = db;
            }

            protected override async Task<Result> HandleCore(Query query)
            {
                var result = await db.Sample.FindAsync(query.Id);

                return new Result()
                {
                    Id = result.Id,
                    Content = result.Content
                };
            }
        }
    }
}
