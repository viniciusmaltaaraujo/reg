using DomainEntity = Domain.Sample.Entity;

namespace Application.DTO.Sample.Example.Common
{
	public class ExampleModelOutput
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedAt { get; set; }

		public ExampleModelOutput(
			long id,
			string name,
			string description,
			bool isActive,
			DateTime createdAt
		)
		{
			Id = id;
			Name = name;
			Description = description;
			IsActive = isActive;
			CreatedAt = createdAt;
		}

		public static ExampleModelOutput FromExample(DomainEntity.Example example)
			=> new(
				example.Id,
				example.Name,
				example.Description,
				example.IsActive,
				example.CreatedAt);
	}
}
