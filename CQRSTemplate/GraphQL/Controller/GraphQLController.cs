﻿using GraphQL;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CQRSTemplate.GraphQL.Controller
{
    [Route("[controller]")]
    public class GraphQLController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator mediator;
          
        public GraphQLController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ExecutionResult> Post([FromBody] GraphQL.GraphQLQuery query)
        {
            var result = await this.mediator.Send(query);

            return result;
        }
    }
}
