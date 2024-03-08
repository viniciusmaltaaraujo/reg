using Domain.Sample.Entity;
using Domain.Sample.Repository;
using Domain.SeedWork.SearchableRepository;
using Infra.Repository.Api;
using Infra.Repository.EF;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text;

namespace Infra.Repository.Repositories
{
    public class ExampleRepository : IExampleRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Example> _examples
            => _context.Set<Example>();

        public ExampleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task Delete(Example aggregate, CancellationToken cancellationToken)
        {
            return Task.FromResult(string.Empty);
        }

        public async Task<Example> Get(long id, CancellationToken cancellationToken)
        {
            var sample = new Example(1, "teste", "teste");


			return sample!;
        }

        public async Task<Example> GetByName(string name, CancellationToken cancellationToken)
        {
			var sample = new Example(1, "teste", "teste");


			return sample!;
		}

        public async Task Insert(Example aggregate, CancellationToken cancellationToken)
        {
			var httpClient = new HttpClient();
			var json = JsonSerializer.Serialize(string.Empty);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			try
			{
				var response = await httpClient.PostAsync(Environment.GetEnvironmentVariable("API_ALLO"), content);
				response.EnsureSuccessStatusCode();
				var responseBody = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Response Body: {responseBody}");
			}
			catch (HttpRequestException e)
			{
				Console.WriteLine($"Error: {e.Message}");
			}
		}

        public Task Update(Example aggregate, CancellationToken cancellationToken)
        {
            return Task.FromResult(string.Empty);
        }

        public async Task<SearchOutput<Example>> Search(SearchInput input, CancellationToken cancellationToken)
        {
            var toSkip = (input.Page - 1) * input.PerPage;
            var query = _examples.AsNoTracking();
            query = AddOrderToQuery(query, input.OrderBy, input.Order);
            if (!String.IsNullOrWhiteSpace(input.Search))
                query = query.Where(x => x.Name.Contains(input.Search));
            var total = await query.CountAsync();
            var items = await query
                .Skip(toSkip)
                .Take(input.PerPage)
                .ToListAsync();
            return new(input.Page, input.PerPage, total, items);
        }

        private IQueryable<Example> AddOrderToQuery(IQueryable<Example> query, string orderProperty, SearchOrder order)
        {
            var orderedQuery = (orderProperty.ToLower(), order) switch
            {
                ("name", SearchOrder.Asc) => query.OrderBy(x => x.Name)
                    .ThenBy(x => x.Id),
                ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Name)
                    .ThenByDescending(x => x.Id),
                ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
                ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),
                ("createdat", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
                ("createdat", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
                _ => query.OrderBy(x => x.Name)
                    .ThenBy(x => x.Id)
            };
            return orderedQuery;
        }

        private async Task<long> GetNextValueForSequence(CancellationToken cancellationToken)
        {
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync(cancellationToken);
            using (var command = connection.CreateCommand())
            {


                var schema = Environment.GetEnvironmentVariable("API_SCHEMA");
                var commandText = string.Empty;

                if (string.IsNullOrEmpty(schema))
                    commandText= "SELECT \"ExampleSequence\".NEXTVAL FROM dual";
                else
                    commandText = $"SELECT {schema}ExampleSequence.NEXTVAL FROM dual";


                command.CommandText = commandText;
                var result = await command.ExecuteScalarAsync(cancellationToken);
                return Convert.ToInt64(result);
            }
        }
    }
}
