using Domain.Entities;
using Domain.Repositories;
using Job.Module.Commands;

namespace Job.Module.Services
{
    public class JobService(IJobRepository repo) : IJobService
    {
        private readonly IJobRepository _repo = repo;

        public async Task<bool> Post(CreateJobCommand command)
        {
            JobEntity entity = new();
            entity.SetDetails(command.Name, command.Description, command.ExpirationDate);
            await _repo.AddAsync(entity, "client_side");
            return true;
        }
    }
}
