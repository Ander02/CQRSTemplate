using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQRSTemplate.Features.Sample
{
    [Route("/[controller]")]
    public class SampleController : Controller
    {
        private IMediator mediator;

        public SampleController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> RegisterEquipament([FromBody] RegisterSample.Command value)
        {
            var result = await mediator.Send(value);

            return Created(this.Request.Path.Value + "/" + result.Id, result);
        }


        [HttpGet]
        public async Task<List<SearchManySamples.Result>> SearchManyEquipments([FromQuery] SearchManySamples.Query query)
        {
            var result = await mediator.Send(query);

            return result;
        }
    }
}
