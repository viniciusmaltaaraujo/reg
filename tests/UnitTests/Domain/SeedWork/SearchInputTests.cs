using Domain.SeedWork.SearchableRepository;

namespace UnitTests.Domain.SeedWork
{
	public class SearchInputTests
	{
		[Fact(DisplayName = nameof(Constructor_AssignsValuesCorrectly))]
		[Trait("Domain", "SeedWork - SearchInput")]
		public void Constructor_AssignsValuesCorrectly()
		{

			// Arrange  
			int expectedPage = 1;
			int expectedPerPage = 10;
			string expectedSearch = "test";
			string expectedOrderBy = "name";
			SearchOrder expectedOrder = SearchOrder.Asc;

			// Act
			var searchInput = new SearchInput(expectedPage, expectedPerPage, expectedSearch, expectedOrderBy, expectedOrder);

			// Assert
			Assert.Equal(expectedPage, searchInput.Page);
			Assert.Equal(expectedPerPage, searchInput.PerPage);
			Assert.Equal(expectedSearch, searchInput.Search);
			Assert.Equal(expectedOrderBy, searchInput.OrderBy);
			Assert.Equal(expectedOrder, searchInput.Order);
		}
	}
}
