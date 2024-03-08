using Domain.Common.Exceptions;
using FluentAssertions;
using Moq;
using DomainEntity = Domain.Sample.Entity;
using DomainService = Domain.Sample.Service;

namespace UnitTests.Domain.Sample.ServiceDomain
{
	[Collection(nameof(ExampleDomainServiceTestFixture))]
	public class CreateExampleDomainServiceTest
	{
		private readonly ExampleDomainServiceTestFixture _fixture;

		public CreateExampleDomainServiceTest(ExampleDomainServiceTestFixture fixture)
		{
			_fixture = fixture;
		}

		//[Fact(DisplayName = nameof(CreateExampleSuccessfully))]
		//[Trait("Domain", "CreateExample - Domain Services")]
		//public async Task CreateExampleSuccessfully()
		//{
		//	var fixture = new ExampleDomainServiceTestFixture();
		//	var repositoryMock = fixture.GetRepositoryMock();
		//	var unitOfWorkMock = fixture.GetUnitOfWorkMock();
		//	var input = fixture.GetValidExample();
		//	repositoryMock.Setup(repository => repository.GetByName(input.Name, CancellationToken.None)).ReturnsAsync((DomainEntity.Example)null!);
		//	var exampleDomainService = new DomainService.ExampleDomainService(repositoryMock.Object, unitOfWorkMock.Object);


		//	repositoryMock.Setup(repository => repository.Insert(It.IsAny<DomainEntity.Example>(), It.IsAny<CancellationToken>()))
		//		.Returns(Task.CompletedTask);

		//	var result = await exampleDomainService.CreateAsync(input, CancellationToken.None);

		//	repositoryMock.Verify(repository => repository.GetByName(input.Name, CancellationToken.None), Times.Once, "Should check for duplicates by name.");
		//	repositoryMock.Verify(repository => repository.Insert(input, It.IsAny<CancellationToken>()), Times.Once, "Should insert the new example once.");
		//	unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Once, "Should commit the transaction once.");
		//	result.Should().NotBeNull();
		//	result.Should().BeEquivalentTo(input, options => options.Excluding(e => e.Id), "The result should match the input except for Id which is set by the repository.");
		//}

		//[Fact(DisplayName = nameof(CreateExampleErrorWhenExampleisNull))]
		//[Trait("Domain", "CreateExample - Domain Services")]
		//public async void CreateExampleErrorWhenExampleisNull()
		//{
		//	var repositoryMock = _fixture.GetRepositoryMock();
		//	var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
		//	var exampleDomainService = new DomainService.ExampleDomainService(
		//		repositoryMock.Object, unitOfWorkMock.Object
		//	);
		//	DomainEntity.Example input = null!;

		//	await FluentActions.Awaiting(() => exampleDomainService.CreateAsync(input!, CancellationToken.None))
		//			.Should().ThrowAsync<BusinessException>()
		//			.WithMessage("example should not be null");

		//}

	//	[Fact(DisplayName = nameof(CreateExampleThrowsWhenDuplicate))]
	//	[Trait("Domain", "CreateExample - Domain Services")]
	//	public async Task CreateExampleThrowsWhenDuplicate()
	//	{
	//		var repositoryMock = _fixture.GetRepositoryMock();
	//		var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
	//		var exampleDomainService = new DomainService.ExampleDomainService(
	//			repositoryMock.Object, unitOfWorkMock.Object
	//		);
	//		var input = _fixture.GetValidExample();
	//		repositoryMock.Setup(repository => repository.GetByName(input.Name, CancellationToken.None))
	//					   .ReturnsAsync(input);

	//		await FluentActions.Awaiting(() => exampleDomainService.CreateAsync(input, CancellationToken.None))
	//						   .Should().ThrowAsync<BusinessException>()
	//						   .WithMessage($"Example {input.Name} existing");

	//		repositoryMock.Verify(repository => repository.GetByName(input.Name, CancellationToken.None), Times.Once);
	//		repositoryMock.Verify(repository => repository.Insert(It.IsAny<DomainEntity.Example>(), It.IsAny<CancellationToken>()), Times.Never);
	//		unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Never);
	//	}
	}
}
