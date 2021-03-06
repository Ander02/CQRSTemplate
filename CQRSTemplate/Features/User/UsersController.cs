﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Features.User
{
    [Route("/[controller]")]
    public class UsersController : Controller
    {
        private IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] Register.Command value)
        {
            var result = await this.mediator.Send(value);

            return Created(this.Request.Path.Value + "/" + result.Id, result);
        }

        [HttpGet]
        public async Task<List<UserViews.FullResult>> SearchMany([FromQuery] SearchMany.Query query)
        {
            var result = await this.mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<UserViews.FullResult> SearchOne([FromRoute] SearchOne.Query query)
        {
            var result = await this.mediator.Send(query);

            return result;
        }

        [HttpPut("{id}")]
        public async Task<UserViews.SimpleResult> Delete([FromRoute] Guid id, [FromBody] Update.Command command)
        {
            command.Id = id;
            var result = await this.mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Delete.Command command)
        {
            await mediator.Send(command);

            return this.StatusCode(204);
        }
    }
}
