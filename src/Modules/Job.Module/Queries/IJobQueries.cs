using Domain.Entities;
using SharedKernel.Domain.Seedwork;

namespace Job.Module.Queries
{
    public interface IJobQueries
    {
        Task<Pagination<JobEntity>> GetAllJobsAsync(int page, int size);
        Task<JobEntity> GetJobById(int id);
    }
}