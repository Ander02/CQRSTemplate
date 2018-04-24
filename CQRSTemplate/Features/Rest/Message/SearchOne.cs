using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CQRSTemplate.Infraestructure;
using CQRSTemplate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSTemplate.Infraestructure.Exceptions;
using CQRSTemplate.Database.Repository.Interface;
using CQRSTemplate.Features.ResultViews;

namespace CQRSTemplate.Features.Rest.Message
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
            private readonly IMessageRepository messageRepository;

            public Handler(IMessageRepository messageRepository)
            {
                this.messageRepository = messageRepository;
            }

            protected override async Task<MessageViews.FullResult> HandleCore(Query query)
            {
                var message = await messageRepository.FindByIdAsync(query.Id);

                if (message == null) throw new NotFoundException("Não foi possível encontrar a mensagem com o Id : " + query.Id);

                else return new MessageViews.FullResult(message);
            }
        }
    }
}
