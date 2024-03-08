using Bogus;
using UnitTests.Common.Fixtures;
using UnitTests.Domain.Sample.Common;
using DomainEntity = Domain.Sample.Entity;

namespace UnitTests.Domain.Sample.Entity
{
	public class ExampleTestFixture : SampleServiceDomainBaseFixture
	{
		public ExampleTestFixture()
			: base() { }

	}

	[CollectionDefinition(nameof(ExampleTestFixture))]
	public class SampleTestFixtureCollection
	: ICollectionFixture<ExampleTestFixture>
	{ }
}
