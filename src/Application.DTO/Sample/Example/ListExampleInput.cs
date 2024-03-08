using Application.DTO.Common;
using Domain.SeedWork.SearchableRepository;

namespace Application.DTO.Sample.Example
{
	public class ListExamplesInput : PaginatedListInput
	{
		public ListExamplesInput(
			int page = 1,
			int perPage = 15,
			string search = "",
			string sort = "",
			SearchOrder dir = SearchOrder.Asc
		) : base(page, perPage, search, sort, dir)
		{ }

		public ListExamplesInput()
			: base(1, 15, "", "", SearchOrder.Asc)
		{ }
	}
}
