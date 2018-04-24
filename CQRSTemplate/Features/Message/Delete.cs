using CQRSTemplate.Database.Repository.Interface;
using CQRSTemplate.Infraestructure.Exceptions;
using FluentValidation;
using MediatR;
using System;
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
            private readonly IMessageRepository messageRepository;

            public Handler(IMessageRepository messageRepository)
            {
                this.messageRepository = messageRepository;
            }

            protected override async Task HandleCore(Command command)
            {
                var message = await messageRepository.FindByIdAsync(command.Id);

                if (message == null) throw new NotFoundException("Não foi possível encontrar a mensagem com o Id : " + command.Id);

                messageRepository.Remove(message);
                await messageRepository.SaveChangesAsync();
            }
        }
    }
}
