namespace Application.DTO.Sample.Example
{
    public class CreateExampleInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public CreateExampleInput(
            string name,
            string? description = null,
            bool isActive = true)
        {
            Name = name;
            Description = description ?? "";
            IsActive = isActive;
        }
    }
}
