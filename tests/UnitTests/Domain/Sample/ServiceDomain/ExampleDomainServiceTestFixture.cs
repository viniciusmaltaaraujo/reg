using UnitTests.Domain.Sample.Common;

namespace UnitTests.Domain.Sample.ServiceDomain
{
	[CollectionDefinition(nameof(ExampleDomainServiceTestFixture))]
	public class ServiceDomainTestFixtureCollection: ICollectionFixture<ExampleDomainServiceTestFixture>
	{ }
	public class ExampleDomainServiceTestFixture : SampleServiceDomainBaseFixture
	{
	}
}


