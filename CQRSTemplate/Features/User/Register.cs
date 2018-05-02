using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSTemplate.Infraestructure.Database;

namespace CQRSTemplate.Features.User
{
    public class Register
    {
        public class Command : IRequest<UserViews.SimpleResult>
        {
            public int Age { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                //Validations
                RuleFor(q => q.Age).NotNull().GreaterThanOrEqualTo(0);
                RuleFor(q => q.Name).NotNull().NotEmpty();
                RuleFor(q => q.Email).NotNull().NotEmpty().EmailAddress();
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
                var user = new Domain.User()
                {
                    Age = command.Age,
                    Name = command.Name,
                    Email = command.Email
                };

                await db.Users.AddAsync(user);

                await db.SaveChangesAsync();

                return new UserViews.SimpleResult(user);
            }
        }
    }
}
