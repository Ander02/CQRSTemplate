﻿using CQRSTemplate.GraphQL.Query;
using CQRSTemplate.Infraestructure.Exceptions;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.GraphQL.Controllers
{
    [Route("[controller]")]
    public class GraphQLController : Controller
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
