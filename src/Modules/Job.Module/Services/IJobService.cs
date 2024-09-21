using Job.Module.Commands;

namespace Job.Module.Services
{
    public interface IJobService
    {
        Task<bool> Post(CreateJobCommand command);
    }
}
