namespace Application.DTO.Sample.Example
{
	public class UpdateExampleInput
	{
		public string Name { get; set; }
		public string? Description { get; set; }
		public bool? IsActive { get; set; }

		public UpdateExampleInput(
			string name,
			string? description = null,
			bool? isActive = null)
		{
			Name = name;
			Description = description;
			IsActive = isActive;
		}
	}
}
