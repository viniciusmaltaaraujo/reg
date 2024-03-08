using Domain.Sample.Entity;
using Infra.Repository.EF;
using Infra.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository.Repositories
{
    public class HealthRepository : IHealthRepository
	{
		private readonly ApplicationDbContext _context;
		private DbSet<Example> _examples
			=> _context.Set<Example>();

		public HealthRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public void IsReady()
		{
			var canConnect = _context.Database.CanConnect();

			if (!canConnect)
			{
				throw new Exception("Unable to connect to database.");
			}
		}
	}
}
