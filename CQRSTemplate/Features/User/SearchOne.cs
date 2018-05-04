using CQRSTemplate.Infraestructure.Database;
using CQRSTemplate.Infraestructure.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Features.User
{
    public class SearchOne
    {
        public class Query : IRequest<UserViews.FullResult>
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

        public class Handler : AsyncRequestHandler<Query, UserViews.FullResult>
        {
            private readonly Db db;

            public Handler(Db db)
            {
                this.db = db;
            }

            protected override async Task<UserViews.FullResult> HandleCore(Query query)
            {
                var user = await db.Users.Include(u => u.Messages).Where(u => u.Id.Equals(query.Id)).FirstOrDefaultAsync();

                if (user == null) throw new NotFoundException("Não foi possível encontrar o usuário com o Id : " + query.Id);

                else return new UserViews.FullResult(user);
            }
        }
    }
}
