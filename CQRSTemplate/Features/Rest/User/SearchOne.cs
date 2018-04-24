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

namespace CQRSTemplate.Features.Rest.User
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
            private readonly IUserRepository userRepository;

            public Handler(IUserRepository userRepository)
            {
                this.userRepository = userRepository;
            }

            protected override async Task<UserViews.FullResult> HandleCore(Query query)
            {
                var user = await userRepository.FindByIdAsync(query.Id);

                if (user == null) throw new NotFoundException("Não foi possível encontrar o usuário com o Id : " + query.Id);

                else return new UserViews.FullResult(user);
            }
        }
    }
}
