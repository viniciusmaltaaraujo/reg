namespace Application.DTO.Sample.Example
{
	public class GetExampleInput
	{
		public long Id { get; set; }
		public GetExampleInput(long id)
			=> Id = id;
	}
}
