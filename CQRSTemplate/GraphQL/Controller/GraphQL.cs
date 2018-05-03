using CQRSTemplate.Infraestructure.Exceptions;
using FluentValidation;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.GraphQL.Controller
{
    public class GraphQL
    {
        public class GQuery : IRequest<ExecutionResult>
        {
            public string OperationName { get; set; }
            public string NamedQuery { get; set; }
            public string Query { get; set; }
            public string Variables { get; set; }

            public override string ToString() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public class CommandValidator : AbstractValidator<GQuery>
        {
            public CommandValidator()
            {
                //Validations               
            }
        }

        public class Handler : AsyncRequestHandler<GQuery, ExecutionResult>
        {
            private readonly ISchema schema;
            private readonly IDocumentExecuter documentExecuter;

            public Handler(ISchema schema, IDocumentExecuter documentExecuter)
            {
                this.schema = schema;
                this.documentExecuter = documentExecuter;
            }

            protected override async Task<ExecutionResult> HandleCore(GQuery query)
            {
                if (query == null) throw new ArgumentNullException(nameof(query));

                var result = await documentExecuter.ExecuteAsync(new ExecutionOptions
                {
                    Schema = this.schema,
                    Query = query.Query
                }).ConfigureAwait(false);

                if (result.Errors?.Count > 0) throw new BadRequestException(JsonConvert.SerializeObject(result));

                return result;
            }
        }
    }
}
