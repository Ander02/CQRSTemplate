using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Features.Message
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

        [HttpPut("{id}")]
        public async Task<MessageViews.SimpleResult> Delete([FromRoute] Guid id, [FromBody] Update.Command command)
        {
            command.Id = id;
            var result = await this.mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Delete.Command command)
        {
            await mediator.Send(command);

            return NoContent();
        }
    }
}
