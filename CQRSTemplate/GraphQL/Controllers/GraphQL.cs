﻿using CQRSTemplate.Database;
using CQRSTemplate.Infraestructure.Exceptions;
using FluentValidation;
using GraphQL;
using GraphQL.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSTemplate.GraphQL.Controllers
{
    public class GraphQL
    {
        public class GraphQLQuery : IRequest<ExecutionResult>
        {
            public string OperationName { get; set; }
            public string NamedQuery { get; set; }
            public string Query { get; set; }
            public string Variables { get; set; }

            public override string ToString() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public class CommandValidator : AbstractValidator<GraphQLQuery>
        {
            public CommandValidator()
            {
                //Validations               
            }
        }

        public class Handler : AsyncRequestHandler<GraphQLQuery, ExecutionResult>
        {
            private readonly ISchema schema;
            private readonly IDocumentExecuter documentExecuter;

            public Handler(ISchema schema, IDocumentExecuter documentExecuter)
            {
                this.schema = schema;
                this.documentExecuter = documentExecuter;
            }

            protected override async Task<ExecutionResult> HandleCore(GraphQLQuery query)
            {
                if (query == null) throw new ArgumentNullException(nameof(query));

                var result = await documentExecuter.ExecuteAsync(new ExecutionOptions
                {
                    Schema = this.schema,
                    Query = query.Query
                }).ConfigureAwait(false);

                if (result.Errors?.Count > 0) throw new BadRequestException(result.Errors.FirstOrDefault().Message);

                return result;
            }
        }
    }
}