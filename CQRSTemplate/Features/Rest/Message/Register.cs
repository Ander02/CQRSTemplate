using FluentValidation;
using MediatR;
using CQRSTemplate.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSTemplate.Database.Repository.Interface;
using CQRSTemplate.Features.ResultViews;

namespace CQRSTemplate.Features.Rest.Message
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
            private readonly IMessageRepository messageRepository;
            private readonly IUserRepository userRepository;

            public Handler(IMessageRepository messageRepository, IUserRepository userRepository)
            {
                this.messageRepository = messageRepository;
                this.userRepository = userRepository;
            }

            protected override async Task<MessageViews.FullResult> HandleCore(Command command)
            {
                var message = new Domain.Message()
                {
                    Title = command.Title,
                    Content = command.Content,
                    User = await userRepository.FindByIdAsync(command.UserId)
                };

                await messageRepository.AddAsync(message);

                await messageRepository.SaveChangesAsync();

                return new MessageViews.FullResult(message);
            }
        }
    }
}
