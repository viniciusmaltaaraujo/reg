using Domain.Sample.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Repository.EF.Configurations
{
    internal class ExampleConfiguration : IEntityTypeConfiguration<Example>
    {
        public void Configure(EntityTypeBuilder<Example> builder)
        {
            builder.ToTable("EXAMPLESDB");
            builder.HasKey(category => category.Id);
            builder.Property(category => category.Id).HasColumnName("ID");
            builder.Property(category => category.Name).HasColumnName("NAME")
                .HasMaxLength(255);
            builder.Property(category => category.Description).HasColumnName("DESCRIPTION")
                .HasMaxLength(10_000);
            builder.Property(category => category.CreatedAt).HasColumnName("CREATEDAT");
            builder.Property(category => category.IsActive).HasColumnName("ISACTIVE");
            builder.Ignore(category => category.Events);
        }
    }
}
