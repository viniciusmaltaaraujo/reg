using Domain.Sample.Entity;
using Domain.SeedWork.SearchableRepository;

namespace UnitTests.Domain.SeedWork
{
	public class SearchOutputTests
	{
		[Fact(DisplayName = nameof(Constructor_AssignsValuesCorrectly_ForExampleAggregate))]
		[Trait("Domain", "SeedWork - SearchOutput")]
		public void Constructor_AssignsValuesCorrectly_ForExampleAggregate()
		{
			// Arrange
			int expectedCurrentPage = 1;
			int expectedPerPage = 10;
			int expectedTotal = 100;
			var expectedItems = new List<Example> { new Example(1, "teste", "teste") };

			// Act
			var searchOutput = new SearchOutput<Example>(expectedCurrentPage, expectedPerPage, expectedTotal, expectedItems);

			// Assert
			Assert.Equal(expectedCurrentPage, searchOutput.CurrentPage);
			Assert.Equal(expectedPerPage, searchOutput.PerPage);
			Assert.Equal(expectedTotal, searchOutput.Total);
			Assert.Equal(expectedItems, searchOutput.Items);
		}
	}
}
