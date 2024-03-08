using Domain.Sample.Entity;
using Domain.SeedWork;
using Domain.SeedWork.SearchableRepository;

namespace Domain.Sample.Repository
{
	public interface IExampleRepository : IGenericRepository<Example>, ISearchableRepository<Example>
	{
		Task<Example> GetByName(string name, CancellationToken cancellationToken);
	}
}
