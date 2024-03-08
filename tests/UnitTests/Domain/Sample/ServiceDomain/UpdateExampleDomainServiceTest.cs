using Domain.Common.Exceptions;
using FluentAssertions;
using Moq;
using DomainEntity = Domain.Sample.Entity;
using DomainService = Domain.Sample.Service;

namespace UnitTests.Domain.Sample.ServiceDomain
{
	[Collection(nameof(ExampleDomainServiceTestFixture))]
	public class UpdateExampleDomainServiceTest
	{
		private readonly ExampleDomainServiceTestFixture _fixture;

		public UpdateExampleDomainServiceTest(ExampleDomainServiceTestFixture fixture)
		{
			_fixture = fixture;
		}

		[Fact(DisplayName = nameof(UpdateExample))]
		[Trait("Domain", "UpdateExample - Domain Services")]
		public async Task UpdateExample()
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

			var output = await exampleDomainService.UpdateAsync(input, CancellationToken.None);

			repositoryMock.Verify(repository => repository.Update(input, It.IsAny<CancellationToken>()), Times.Once);
			unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Once);
			output.Should().NotBeNull();
			output.Should().BeEquivalentTo(input);
		}


		[Fact(DisplayName = nameof(UpdateDescription))]
		[Trait("Domain", "UpdateExample - Domain Services")]
		public async Task UpdateDescription()
		{
			var repositoryMock = _fixture.GetRepositoryMock();
			var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
			var exampleDomainService = new DomainService.ExampleDomainService(
				repositoryMock.Object, unitOfWorkMock.Object
			);
			var input = _fixture.GetValidExample();
			var newDescription = _fixture.GetValidExampleDescription();

			repositoryMock.Setup(repository => repository.Update(
				It.IsAny<DomainEntity.Example>(),
				It.IsAny<CancellationToken>()
			)).Returns(Task.CompletedTask);

			var output = await exampleDomainService.UpdateAsync(input, CancellationToken.None);
			output.UpdateDescription(newDescription);

			repositoryMock.Verify(repository => repository.Update(input, It.IsAny<CancellationToken>()), Times.Once);
			unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Once);
			output.Should().NotBeNull();
			output.Should().BeEquivalentTo(input);
			output.Description.Should().Be(newDescription);
		}

		[Fact(DisplayName = nameof(UpdateExampleErrorWhenExampleisNull))]
		[Trait("Domain", "Update - Domain Services")]
		public async void UpdateExampleErrorWhenExampleisNull()
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

		[Fact(DisplayName = nameof(UpdateExampleThrowsWhenDuplicate))]
		[Trait("Domain", "UpdateExample - Domain Services")]
		public async Task UpdateExampleThrowsWhenDuplicate()
		{
			var repositoryMock = _fixture.GetRepositoryMock();
			var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
			var exampleDomainService = new DomainService.ExampleDomainService(
				repositoryMock.Object, unitOfWorkMock.Object
			);
			var input = _fixture.GetValidExample();
			repositoryMock.Setup(repository => repository.GetByName(input.Name, CancellationToken.None))
						   .ReturnsAsync(input);

			await FluentActions.Awaiting(() => exampleDomainService.UpdateAsync(input, CancellationToken.None))
							   .Should().ThrowAsync<BusinessException>()
							   .WithMessage($"Example {input.Name} existing");

			repositoryMock.Verify(repository => repository.GetByName(input.Name, CancellationToken.None), Times.Once);
			repositoryMock.Verify(repository => repository.Insert(It.IsAny<DomainEntity.Example>(), It.IsAny<CancellationToken>()), Times.Never);
			unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Never);
		}
	}
}
