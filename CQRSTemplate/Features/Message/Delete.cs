using CQRSTemplate.Infraestructure.Database;
using CQRSTemplate.Infraestructure.Exceptions;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Features.Message
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
                var message = await db.Messages.FindAsync(command.Id);

                if (message == null) throw new NotFoundException("Não foi possível encontrar a mensagem com o Id : " + command.Id);

                db.Messages.Remove(message);
                await db.SaveChangesAsync();
            }
        }
    }
}
