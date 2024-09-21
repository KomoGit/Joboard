using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DAL;

namespace Application.Usecase
{
    public class JobRepository(AppDbContext context) : Repository<JobEntity>(context), IJobRepository
    {

    }
}