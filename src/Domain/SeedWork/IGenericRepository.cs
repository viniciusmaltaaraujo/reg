namespace Domain.SeedWork
{
	public interface IGenericRepository<TAggregate> : IRepository
	  where TAggregate : AggregateRoot
	{
		public Task Insert(TAggregate aggregate, CancellationToken cancellationToken);
		public Task<TAggregate> Get(long id, CancellationToken cancellationToken);
		public Task Delete(TAggregate aggregate, CancellationToken cancellationToken);
		public Task Update(TAggregate aggregate, CancellationToken cancellationToken);
	}
}
