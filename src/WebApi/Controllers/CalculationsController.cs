namespace WebApi.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Features.Calculations;

    [Route("api/[controller]")]
    public class CalculationsController : Controller
    {
        private readonly IMediator mediator;

        public CalculationsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public CalculateResult Post([FromBody]InputModel model)
        {
            var result = 
                this.mediator.Send(
                    new CalculateCommand { Expression = model.Expression, IsCachable = true });

            return result;
        }
    }
}