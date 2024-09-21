using Domain.Repositories;
using Job.Module.Commands;
using Job.Module.Services;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class JobController(IJobService service) : ControllerBase
    {
        private readonly IJobService _jobService = service;

        [HttpPost]
        public async Task<IActionResult> CreateJob([FromForm] CreateJobCommand command)
        {
            return Ok(await _jobService.Post(command));
        }
    }
}