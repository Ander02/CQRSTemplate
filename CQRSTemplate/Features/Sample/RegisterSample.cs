using FluentValidation;
using MediatR;
using SQRSEmptyTemplate.Infraestructure;
using SQRSEmptyTemplate.Infraestructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQRSEmptyTemplate.Features.Sample
{
    public class RegisterSample
    {
        public class Command : IRequest<Result>
        {
            public string Content { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                //Validations
            }
        }

        public class Result
        {
            public Guid Id { get; set; }
            public string Content { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, Result>
        {
            private readonly Db db;

            public Handler(Db db)
            {
                this.db = db;
            }

            protected override async Task<Result> HandleCore(Command command)
            {
                var sample = new Domain.Sample()
                {
                    Content = command.Content
                };

                await db.AddAsync(sample);

                await db.SaveChangesAsync();

                return new Result()
                {
                    Id = sample.Id,
                    Content = sample.Content
                };
            }
        }
    }
}
