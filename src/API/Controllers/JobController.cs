using Job.Module.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController(IJobQueries jobQueries) : ControllerBase
    {
        private readonly IJobQueries _jobQueries = jobQueries;

        [HttpGet]
        public async Task<IActionResult> GetAllJobs([FromQuery] int page, int size)
        {
            return Ok(await _jobQueries.GetAllJobsAsync(page, size));
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetAllJobs([FromBody] int id)
        {
            return Ok(await _jobQueries.GetJobById(id));
        }
    }
}