using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SQRSEmptyTemplate.Infraestructure;
using SQRSEmptyTemplate.Infraestructure.Database;
using SQRSEmptyTemplate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQRSEmptyTemplate.Features.Sample
{
    public class SearchManySamples
    {
        public class Query : IRequest<List<Result>>
        {

        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {

            }
        }

        public class Result
        {
            public Guid Id { get; set; }
            public string Content { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, List<Result>>
        {
            private readonly Db db;

            public Handler(Db db)
            {
                this.db = db;
            }

            protected override async Task<List<Result>> HandleCore(Query query)
            {
                var result = await db.Sample.ToListAsync();

                return result.Select(r => new Result
                {
                    Id = r.Id,
                    Content = r.Content
                }).ToList();
            }
        }
    }
}
