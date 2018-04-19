﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Features.User
{
    [Route("/[controller]")]
    public class UserController : Controller
    {
        private IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] Register.Command value)
        {
            var result = await mediator.Send(value);

            return Created(this.Request.Path.Value + "/" + result.Id, result);
        }


        [HttpGet]
        public async Task<List<UserViews.FullResult>> SearchMany([FromQuery] SearchMany.Query query)
        {
            var result = await mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<UserViews.FullResult> SearchOne([FromRoute] SearchOne.Query query)
        {
            var result = await mediator.Send(query);

            return result;
        }
    }
}