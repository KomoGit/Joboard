using Domain.Entities;
using Domain.Repositories;
using SharedKernel.Domain.Seedwork;

namespace Job.Module.Queries
{
    public class JobQueries(IJobRepository jobRepo) : IJobQueries
    {
        private readonly IJobRepository _jobRepo = jobRepo;

        public async Task<Pagination<JobEntity>> GetAllJobsAsync(int page, int size)
        {
            var data = await _jobRepo.GetAllAsync();
            var result = new Pagination<JobEntity>(data, page, size);
            return result;
        }

        public async Task<JobEntity> GetJobById(int id)
        {
            return await _jobRepo.GetByIdAsync(id);
        }
    }
}
