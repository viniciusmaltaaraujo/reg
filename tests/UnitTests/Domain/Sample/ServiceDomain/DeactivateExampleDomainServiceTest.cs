using Domain.Common.Exceptions;
using FluentAssertions;
using Moq;
using DomainEntity = Domain.Sample.Entity;
using DomainService = Domain.Sample.Service;

namespace UnitTests.Domain.Sample.ServiceDomain
{
	[Collection(nameof(ExampleDomainServiceTestFixture))]
	public class DeactivateExampleDomainServiceTest
	{
		private readonly ExampleDomainServiceTestFixture _fixture;

		public DeactivateExampleDomainServiceTest(ExampleDomainServiceTestFixture fixture)
		{
			_fixture = fixture;
		}

		[Fact(DisplayName = nameof(DeactivateExample))]
		[Trait("Domain", "DeactivateExample - Domain Services")]
		public async Task DeactivateExample()
		{
			var repositoryMock = _fixture.GetRepositoryMock();
			var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
			var exampleDomainService = new DomainService.ExampleDomainService(
				repositoryMock.Object, unitOfWorkMock.Object
			);
			var input = _fixture.GetValidExample();

			repositoryMock.Setup(repository => repository.Update(
				It.IsAny<DomainEntity.Example>(),
				It.IsAny<CancellationToken>()
			)).Returns(Task.CompletedTask);

			await exampleDomainService.DeactivateAsync(input, CancellationToken.None);

			repositoryMock.Verify(repository => repository.Update(input, It.IsAny<CancellationToken>()), Times.Once);
			unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Once);
			input.IsActive.Should().BeFalse();
		}

		[Fact(DisplayName = nameof(DeleteExampleErrorWhenExampleisNull))]
		[Trait("Domain", "DeactivateExample - Domain Services")]
		public async void DeleteExampleErrorWhenExampleisNull()
		{
			var repositoryMock = _fixture.GetRepositoryMock();
			var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
			var exampleDomainService = new DomainService.ExampleDomainService(
				repositoryMock.Object, unitOfWorkMock.Object
			);
			DomainEntity.Example input = null!;

			await FluentActions.Awaiting(() => exampleDomainService.UpdateAsync(input!, CancellationToken.None))
					.Should().ThrowAsync<BusinessException>()
					.WithMessage("example should not be null");

		}
	}
}
