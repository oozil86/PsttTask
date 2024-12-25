using MediatR;
using Microsoft.AspNetCore.Mvc;
using PsttTask.ApplicationService.Features.Company;
using PsttTask.Domain.Contracts;
using PsttTask.Domain.Models.Company;

namespace PsttTask.Company.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompaniesController : ControllerBase
    {

        private readonly ILogger<CompaniesController> _logger;
        private readonly IMediator _mediator;

        public CompaniesController(ILogger<CompaniesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _mediator.Send(new GetCompaniesQuery()));

        [HttpGet("get-Company")]
        public async Task<IActionResult> Get(Guid CompanyReference)
            => Ok(await _mediator.Send(new GetCompanyQuery(CompanyReference)));

        [HttpGet("get-Paged-Companies")]
        public async Task<IActionResult> Get([FromQuery] PageFilter filter)
            => Ok(await _mediator.Send(new GetPagedCompaniesQuery(filter)));

        [HttpPost]
        public async Task<IActionResult> Post(SaveCompanyModel Company)
            => Ok(await _mediator.Send(new SaveCompanyCommand(Company)));

        [HttpPut]
        public async Task<IActionResult> Put(UpdateCompanyModel Company)
           => Ok(await _mediator.Send(new EditCompanyCommand(Company)));

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid CompanyReference)
         => Ok(await _mediator.Send(new DeleteCompanyCommand(CompanyReference)));

    }
}
