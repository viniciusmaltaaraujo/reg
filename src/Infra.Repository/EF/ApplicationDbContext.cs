using Domain.Sample.Entity;
using Infra.Repository.EF.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Example> ExamplesDb => Set<Example>();

        protected override void OnModelCreating(ModelBuilder builder)
        {

            var schema = Environment.GetEnvironmentVariable("API_SCHEMA");

            if (!string.IsNullOrEmpty(schema))
                builder.HasDefaultSchema(schema);

            builder.ApplyConfiguration(new ExampleConfiguration());
        }
    }
}
