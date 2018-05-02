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
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                //Validations
                RuleFor(q => q.Id).NotNull().NotEmpty();
            }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly Db db;

            public Handler(Db db)
            {
                this.db = db;
            }

            protected override async Task HandleCore(Command command)
            {
                var user = await db.Users.FindAsync(command.Id);

                if (user == null) throw new NotFoundException("Não foi possível encontrar o usuário com o Id : " + command.Id);

                db.Users.Remove(user);
                await db.SaveChangesAsync();
            }
        }
    }
}
