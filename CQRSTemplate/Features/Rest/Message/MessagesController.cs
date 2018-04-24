using CQRSTemplate.Features.ResultViews;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Features.Rest.Message
{
    [Route("/[controller]")]
    public class MessagesController : Controller
    {
        private IMediator mediator;

        public MessagesController(IMediator mediator)
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
        public async Task<List<MessageViews.FullResult>> SearchMany([FromQuery] SearchMany.Query query)
        {
            var result = await mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<MessageViews.FullResult> SearchOne([FromRoute] SearchOne.Query query)
        {
            var result = await mediator.Send(query);

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
