using CQRSTemplate.Infraestructure.Database;
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
            private readonly Db db;

            public Handler(Db db)
            {
                this.db = db;
            }

            protected override async Task<UserViews.SimpleResult> HandleCore(Command command)
            {
                var user = await db.Users.FindAsync(command.Id);

                if (user == null) throw new NotFoundException("Não foi possível encontrar o usuário com o Id : " + command.Id);

                user.Age = command.Age ?? user.Age;
                user.Name = command.Name ?? user.Name;
                user.Email = command.Email ?? user.Email;

                await db.SaveChangesAsync();

                return new UserViews.SimpleResult(user);
            }
        }
    }
}
