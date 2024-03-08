using Domain.Common.Validation;
using Domain.SeedWork;

namespace Domain.Sample.Entity
{
	public class Example : AggregateRoot
	{
		public string Name { get; private set; }
		public string Description { get; private set; }
		public bool IsActive { get; private set; }
		public DateTime CreatedAt { get; private set; }

		public Example(string name, string description, bool isActive = true)
			: base()
		{
			Name = name;
			Description = description;
			IsActive = isActive;
			CreatedAt = DateTime.Now;

			Validate();
		}

		public Example(long id, string name, string description, bool isActive = true)
			: base()
		{
			Id = id;
			Name = name;
			Description = description;
			IsActive = isActive;
			CreatedAt = DateTime.Now;

			ValidateId(id);
			Validate();
		}

		public void Activate()
		{
			IsActive = true;
			Validate();
		}
		public void Deactivate()
		{
			IsActive = false;
			Validate();
		}

		public void Update(string name, string? description = null)
		{
			Name = name;
			Description = description ?? Description;

			Validate();
		}

		public void UpdateDescription(string? description)
		{
			Description = description ?? Description;
			Validate();
		}

	

		private void Validate()
		{
			DomainValidation.NotNullOrEmpty(Name, nameof(Name));
			DomainValidation.MinLength(Name, 3, nameof(Name));
			DomainValidation.MaxLength(Name, 255, nameof(Name));

			DomainValidation.NotNull(Description, nameof(Description));
			DomainValidation.MaxLength(Description, 10_000, nameof(Description));
		}

		public override void ValidateId(long id)
		{
			DomainValidation.ValidateId(id);
		}
	}
}
