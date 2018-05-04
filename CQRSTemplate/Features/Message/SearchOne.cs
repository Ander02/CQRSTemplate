using CQRSTemplate.Infraestructure.Database;
using CQRSTemplate.Infraestructure.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Features.Message
{
    public class SearchOne
    {
        public class Query : IRequest<MessageViews.FullResult>
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

        public class Handler : AsyncRequestHandler<Query, MessageViews.FullResult>
        {
            private readonly Db db;

            public Handler(Db db)
            {
                this.db = db;
            }

            protected override async Task<MessageViews.FullResult> HandleCore(Query query)
            {
                var message = await db.Messages.Include(m => m.User).Where(m => m.Id.Equals(query.Id)).FirstOrDefaultAsync();

                if (message == null) throw new NotFoundException("Não foi possível encontrar a mensagem com o Id : " + query.Id);

                else return new MessageViews.FullResult(message);
            }
        }
    }
}
