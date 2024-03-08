using Domain.SeedWork;

namespace Infra.Repository.Interfaces
{
    public interface IHealthRepository : IRepository
    {
        void IsReady();
    }
}
