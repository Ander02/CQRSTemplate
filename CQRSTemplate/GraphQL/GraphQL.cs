using FluentValidation;
using GraphQL;
using GraphQL.Types;
using MediatR;
using System;
using System.Threading.Tasks;

namespace CQRSTemplate.GraphQL
{
    public class GraphQL
    {
        public class GraphQuery : IRequest<ExecutionResult>
        {
            public string OperationName { get; set; }
            public string NamedQuery { get; set; }
            public string Query { get; set; }
            public string Variables { get; set; }

            public override string ToString() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public class CommandValidator : AbstractValidator<GraphQuery>
        {
            public CommandValidator()
            {
                //Validations               
            }
        }

        public class Handler : AsyncRequestHandler<GraphQuery, ExecutionResult>
        {
            private readonly ISchema schema;
            private readonly IDocumentExecuter documentExecuter;

            public Handler(ISchema schema, IDocumentExecuter documentExecuter)
            {
                this.schema = schema;
                this.documentExecuter = documentExecuter;
            }

            protected override async Task<ExecutionResult> HandleCore(GraphQuery graphQuery)
            {
                if (graphQuery == null) throw new ArgumentNullException(nameof(graphQuery));

                var result = await documentExecuter.ExecuteAsync(new ExecutionOptions
                {
                    Schema = this.schema,
                    Query = graphQuery.Query,
                    Inputs = graphQuery.Variables.ToInputs()
                }).ConfigureAwait(false);

                //if (result.Errors?.Count > 0) throw new BadRequestException(JsonConvert.SerializeObject(result));

                return result;
            }
        }
    }
}
