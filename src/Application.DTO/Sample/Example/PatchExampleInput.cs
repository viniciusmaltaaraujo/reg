namespace Application.DTO.Sample.Example
{
	public class PatchExampleInput
    {
		public string? Description { get; set; }
		public bool? IsActive { get; set; }

		public PatchExampleInput(
			string? description = null,
			bool? isActive = null)
		{
			Description = description;
			IsActive = isActive;
		}
	}
}
