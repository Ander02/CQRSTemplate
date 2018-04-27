using CQRSTemplate.Database.Repository.Interface;
using CQRSTemplate.Infraestructure.Exceptions;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Features.User
{
    public class Update
    {
        public class Command : IRequest<UserViews.SimpleResult>
        {
            public Guid Id { get; set; }
            public int? Age { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                //Validations

            }
        }

        public class Handler : AsyncRequestHandler<Command, UserViews.SimpleResult>
        {
            private readonly IUserRepository userRepository;

            public Handler(IUserRepository userRepository)
            {
                this.userRepository = userRepository;
            }

            protected override async Task<UserViews.SimpleResult> HandleCore(Command command)
            {
                var user = await userRepository.FindByIdAsync(command.Id);

                if (user == null) throw new NotFoundException("Não foi possível encontrar o usuário com o Id : " + command.Id);

                user.Age = command.Age ?? user.Age;
                user.Name = command.Name ?? user.Name;
                user.Email = command.Email ?? user.Email;

                await userRepository.SaveChangesAsync();

                return new UserViews.SimpleResult(user);
            }
        }
    }
}
