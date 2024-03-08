using Bogus;
using Domain.Sample.Repository;
using Domain.SeedWork;
using Moq;
using UnitTests.Common.Fixtures;
using DomainEntity = Domain.Sample.Entity;

namespace UnitTests.Domain.Sample.Common
{
	public class SampleServiceDomainBaseFixture : BaseFixture
	{
		public Mock<IExampleRepository> GetRepositoryMock()
			=> new();
		public Mock<IUnitOfWork> GetUnitOfWorkMock()
			=> new();

		public string GetValidExampleName()
		{
			var sampleName = "";
			while (sampleName.Length < 3)
				sampleName = Faker.Commerce.Categories(1)[0];
			if (sampleName.Length > 255)
				sampleName = sampleName[..255];
			return sampleName;
		}

		public string GetValidExampleDescription()
		{
			var sampleDescription =
				Faker.Commerce.ProductDescription();
			if (sampleDescription.Length > 10_000)
				sampleDescription =
					sampleDescription[..10_000];
			return sampleDescription;
		}

		public DomainEntity.Example GetValidExample()
			=> new(
				GetRandomId(),
				GetValidExampleName(),
				GetValidExampleDescription()
			);
	}
}