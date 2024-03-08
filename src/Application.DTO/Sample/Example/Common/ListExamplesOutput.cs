using Application.DTO.Common;

namespace Application.DTO.Sample.Example.Common
{
	public class ListExamplesOutput : PaginatedListOutput<ExampleModelOutput>
	{
		public ListExamplesOutput(
			int page,
			int perPage,
			int total,
			IReadOnlyList<ExampleModelOutput> items)
			: base(page, perPage, total, items)
		{
		}
	}
}
