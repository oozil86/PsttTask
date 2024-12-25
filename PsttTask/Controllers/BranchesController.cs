using MediatR;
using Microsoft.AspNetCore.Mvc;
using PsttTask.ApplicationService.Features.Branch;
using PsttTask.Domain.Contracts;
using PsttTask.Domain.Models.Branch;

namespace PsttTask.Branch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BranchesController : ControllerBase
    {

        private readonly ILogger<BranchesController> _logger;
        private readonly IMediator _mediator;

        public BranchesController(ILogger<BranchesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _mediator.Send(new GetBranchesQuery()));

        [HttpGet("get-Branch")]
        public async Task<IActionResult> Get(Guid BranchReference)
            => Ok(await _mediator.Send(new GetBranchQuery(BranchReference)));

        [HttpGet("get-Paged-Branches")]
        public async Task<IActionResult> Get([FromQuery] PageFilter filter)
            => Ok(await _mediator.Send(new GetPagedBranchesQuery(filter)));

        [HttpPost]
        public async Task<IActionResult> Post(SaveBranchModel Branch)
            => Ok(await _mediator.Send(new SaveBranchCommand(Branch)));

        [HttpPut]
        public async Task<IActionResult> Put(UpdateBranchModel Branch)
           => Ok(await _mediator.Send(new EditBranchCommand(Branch)));

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid BranchReference)
         => Ok(await _mediator.Send(new DeleteBranchCommand(BranchReference)));

    }
}
