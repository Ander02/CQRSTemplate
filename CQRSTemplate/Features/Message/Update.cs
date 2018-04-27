using CQRSTemplate.Database.Repository.Interface;
using CQRSTemplate.Infraestructure.Exceptions;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Features.Message
{
    public class Update
    {
        public class Command : IRequest<MessageViews.SimpleResult>
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                //Validations

            }
        }

        public class Handler : AsyncRequestHandler<Command, MessageViews.SimpleResult>
        {
            private readonly IMessageRepository messageRepository;

            public Handler(IMessageRepository messageRepository)
            {
                this.messageRepository = messageRepository;
            }

            protected override async Task<MessageViews.SimpleResult> HandleCore(Command command)
            {
                var message = await messageRepository.FindByIdAsync(command.Id);

                if (message == null) throw new NotFoundException("Não foi possível encontrar mensagem com o Id : " + command.Id);

                message.Title = command.Title ?? message.Title;
                message.Content = command.Content ?? message.Content;

                await messageRepository.SaveChangesAsync();

                return new MessageViews.SimpleResult(message);
            }
        }
    }
}
