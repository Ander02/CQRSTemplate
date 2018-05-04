using CQRSTemplate.Infraestructure.Database;
using CQRSTemplate.Infraestructure.Exceptions;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;

namespace CQRSTemplate.Features.Message
{
    public class Register
    {
        public class Command : IRequest<MessageViews.FullResult>
        {
            public Guid UserId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                //Validations
                RuleFor(q => q.UserId).NotNull().NotEmpty();
                RuleFor(q => q.Title).NotNull().NotEmpty();
                RuleFor(q => q.Content).NotNull().NotEmpty();
            }
        }

        public class Handler : AsyncRequestHandler<Command, MessageViews.FullResult>
        {
            private readonly Db db;

            public Handler(Db db)
            {
                this.db = db;
            }

            protected override async Task<MessageViews.FullResult> HandleCore(Command command)
            {
                var user = await db.Users.FindAsync(command.UserId);

                if (user == null) throw new BadRequestException("Usuário com o Id " + command.UserId + " não existe");

                var message = new Domain.Message()
                {
                    Title = command.Title,
                    Content = command.Content,
                    User = user
                };

                await db.Messages.AddAsync(message);

                await db.SaveChangesAsync();

                return new MessageViews.FullResult(message);
            }
        }
    }
}
