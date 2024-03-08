using Domain.Common.Exceptions;
using FluentAssertions;
using DomainEntity = Domain.Sample.Entity;

namespace UnitTests.Domain.Sample.Entity
{

	[Collection(nameof(ExampleTestFixture))]
	public class ExampleTest
	{
		private readonly ExampleTestFixture _exampleTestFixture;

		public ExampleTest(ExampleTestFixture exampleTestFixture)
			=> _exampleTestFixture = exampleTestFixture;

		[Fact(DisplayName = nameof(Instantiate))]
		[Trait("Domain", "Example - Aggregates")]
		public void Instantiate()
		{
			var validSample = _exampleTestFixture.GetValidExample();
			var datetimeBefore = DateTime.Now;

			var example = new DomainEntity.Example(validSample.Name, validSample.Description);
			var datetimeAfter = DateTime.Now.AddSeconds(1);

			example.Should().NotBeNull();
			example.Name.Should().Be(validSample.Name);
			example.Description.Should().Be(validSample.Description);
			example.Id.Should().Be(0);
			example.CreatedAt.Should().NotBeSameDateAs(default);
			(example.CreatedAt >= datetimeBefore).Should().BeTrue();
			(example.CreatedAt <= datetimeAfter).Should().BeTrue();
			example.IsActive.Should().BeTrue();
		}


		[Fact(DisplayName = nameof(InstantiateWithId))]
		[Trait("Domain", "Example - Aggregates")]
		public void InstantiateWithId()
		{

			var id = _exampleTestFixture.GetRandomId();
			var validExample = _exampleTestFixture.GetValidExample();
			var datetimeBefore = DateTime.Now;

			var example = new DomainEntity.Example(id, validExample.Name, validExample.Description);
			var datetimeAfter = DateTime.Now.AddSeconds(1);

			example.Should().NotBeNull();
			example.Name.Should().Be(validExample.Name);
			example.Description.Should().Be(validExample.Description);
			example.Id.Should().NotBe(0);
			example.CreatedAt.Should().NotBeSameDateAs(default);
			(example.CreatedAt >= datetimeBefore).Should().BeTrue();
			(example.CreatedAt <= datetimeAfter).Should().BeTrue();
			example.IsActive.Should().BeTrue();
		}

		[Fact(DisplayName = nameof(InstantiateWithSetId))]
		[Trait("Domain", "Example - Aggregates")]
		public void InstantiateWithSetId()
		{

			var id = _exampleTestFixture.GetRandomId();
			var validExample = _exampleTestFixture.GetValidExample();
			var datetimeBefore = DateTime.Now;

			var example = new DomainEntity.Example(validExample.Name, validExample.Description);
			var datetimeAfter = DateTime.Now.AddSeconds(1);
			example.SetId(1);

			example.Should().NotBeNull();
			example.Name.Should().Be(validExample.Name);
			example.Description.Should().Be(validExample.Description);
			example.Id.Should().NotBe(0);
			example.CreatedAt.Should().NotBeSameDateAs(default);
			(example.CreatedAt >= datetimeBefore).Should().BeTrue();
			(example.CreatedAt <= datetimeAfter).Should().BeTrue();
			example.IsActive.Should().BeTrue();
		}

		[Theory(DisplayName = nameof(InstantiateWithIsActive))]
		[Trait("Domain", "Example - Aggregates")]
		[InlineData(true)]
		[InlineData(false)]
		public void InstantiateWithIsActive(bool isActive)
		{
			var id = _exampleTestFixture.GetRandomId();
			var validExample = _exampleTestFixture.GetValidExample();
			var datetimeBefore = DateTime.Now;

			var example = new DomainEntity.Example(id, validExample.Name, validExample.Description, isActive);
			var datetimeAfter = DateTime.Now.AddSeconds(1);

			example.Should().NotBeNull();
			example.Name.Should().Be(validExample.Name);
			example.Description.Should().Be(validExample.Description);
			example.Id.Should().NotBe(0);
			example.CreatedAt.Should().NotBeSameDateAs(default);
			(example.CreatedAt >= datetimeBefore).Should().BeTrue();
			(example.CreatedAt <= datetimeAfter).Should().BeTrue();
			example.IsActive.Should().Be(isActive);
		}

        [Fact(DisplayName = nameof(InstantiateErrorWhenIdExist))]
        [Trait("Domain", "Example - Aggregates")]
        public void InstantiateErrorWhenIdExist()
		{
			var validExample = _exampleTestFixture.GetValidExample();
			var example = new DomainEntity.Example(1, validExample.Name, validExample.Description);

			Action action =
				() => example.SetId(2);

			action.Should()
				.Throw<Exception>()
				.WithMessage("Existing entity, you can only set the Id when the object is new");
		}

		[Theory(DisplayName = nameof(InstantiateErrorWhenwWithoutId))]
		[Trait("Domain", "Example - Aggregates")]
		[InlineData(0)]
		public void InstantiateErrorWhenwWithoutId(long id)
		{
			var validExample = _exampleTestFixture.GetValidExample();
			var datetimeBefore = DateTime.Now;

			Action action =
				() => new DomainEntity.Example(id, validExample.Name, validExample.Description);

			action.Should()
				.Throw<NotFoundException>()
				.WithMessage("Id should not be zero");
		}

		[Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
		[Trait("Domain", "Example - Aggregates")]
		[InlineData("")]
		[InlineData(null)]
		[InlineData("   ")]
		public void InstantiateErrorWhenNameIsEmpty(string? name)
		{
			var validExample = _exampleTestFixture.GetValidExample();

			Action action =
				() => new DomainEntity.Example(name!, validExample.Description);

			action.Should()
				.Throw<BusinessException>()
				.WithMessage("Name should not be empty or null");
		}

		[Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
		[Trait("Domain", "Example - Aggregates")]
		public void InstantiateErrorWhenDescriptionIsNull()
		{
			var validExample = _exampleTestFixture.GetValidExample();

			Action action =
				() => new DomainEntity.Example(validExample.Name, null!);

			action.Should()
				.Throw<BusinessException>()
				.WithMessage("Description should not be null");
		}

		[Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
		[Trait("Domain", "Example - Aggregates")]
		[MemberData(nameof(GetNamesWithLessThan3Characters), parameters: 10)]
		public void InstantiateErrorWhenNameIsLessThan3Characters(string invalidName)
		{
			var validExample = _exampleTestFixture.GetValidExample();

			Action action =
				() => new DomainEntity.Example(invalidName, validExample.Description);

			action.Should()
				.Throw<BusinessException>()
				.WithMessage("Name should be at least 3 characters long");
		}

		public static IEnumerable<object[]> GetNamesWithLessThan3Characters(int numberOfTests = 6)
		{
			var fixture = new ExampleTestFixture();
			for (int i = 0; i < numberOfTests; i++)
			{
				var isOdd = i % 2 == 1;
				yield return new object[] {
			 fixture.GetValidExampleName()[..(isOdd ? 1 : 2)]
		 };
			}
		}

		[Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
		[Trait("Domain", "Example - Aggregates")]
		public void InstantiateErrorWhenNameIsGreaterThan255Characters()
		{
			var validExample = _exampleTestFixture.GetValidExample();
			var invalidName = string.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());

			Action action =
				() => new DomainEntity.Example(invalidName, validExample.Description);

			action.Should()
				.Throw<BusinessException>()
				.WithMessage("Name should be less or equal 255 characters long");
		}

		[Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters))]
		[Trait("Domain", "Example - Aggregates")]
		public void InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters()
		{
			var invalidDescription = string.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a").ToArray());
			var validExample = _exampleTestFixture.GetValidExample();

			Action action =
				() => new DomainEntity.Example(validExample.Name, invalidDescription);

			action.Should()
				.Throw<BusinessException>()
				.WithMessage("Description should be less or equal 10000 characters long");
		}

		[Fact(DisplayName = nameof(Activate))]
		[Trait("Domain", "Example - Aggregates")]
		public void Activate()
		{
			var validExample = _exampleTestFixture.GetValidExample();

			var sample = new DomainEntity.Example(validExample.Name, validExample.Description, false);
			sample.Activate();

			sample.IsActive.Should().BeTrue();
		}

		[Fact(DisplayName = nameof(Deactivate))]
		[Trait("Domain", "Example - Aggregates")]
		public void Deactivate()
		{
			var validExample = _exampleTestFixture.GetValidExample();

			var example = new DomainEntity.Example(validExample.Name, validExample.Description, true);
			example.Deactivate();

			example.IsActive.Should().BeFalse();
		}

		[Fact(DisplayName = nameof(Update))]
		[Trait("Domain", "Example - Aggregates")]
		public void Update()
		{
			var example = _exampleTestFixture.GetValidExample();
			var exampleWithNewValues = _exampleTestFixture.GetValidExample();

			example.Update(exampleWithNewValues.Name, exampleWithNewValues.Description);

			example.Name.Should().Be(exampleWithNewValues.Name);
			example.Description.Should().Be(exampleWithNewValues.Description);
		}

		[Fact(DisplayName = nameof(UpdateOnlyName))]
		[Trait("Domain", "Example - Aggregates")]
		public void UpdateOnlyName()
		{
			var example = _exampleTestFixture.GetValidExample();
			var newName = _exampleTestFixture.GetValidExampleName();
			var currentDescription = example.Description;

			example.Update(newName);

			example.Name.Should().Be(newName);
			example.Description.Should().Be(currentDescription);
		}

		[Theory(DisplayName = nameof(UpdateErrorWhenNameIsEmpty))]
		[Trait("Domain", "Example - Aggregates")]
		[InlineData("")]
		[InlineData(null)]
		[InlineData("   ")]
		public void UpdateErrorWhenNameIsEmpty(string? name)
		{
			var example = _exampleTestFixture.GetValidExample();
			Action action =
				() => example.Update(name!);

			action.Should().Throw<BusinessException>()
				.WithMessage("Name should not be empty or null");
		}

		[Theory(DisplayName = nameof(UpdateErrorWhenNameIsLessThan3Characters))]
		[Trait("Domain", "Example - Aggregates")]
		[InlineData("1")]
		[InlineData("12")]
		[InlineData("a")]
		[InlineData("ca")]
		public void UpdateErrorWhenNameIsLessThan3Characters(string invalidName)
		{
			var example = _exampleTestFixture.GetValidExample();

			Action action =
				() => example.Update(invalidName);

			action.Should()
				.Throw<BusinessException>()
				.WithMessage("Name should be at least 3 characters long");
		}

		[Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreaterThan255Characters))]
		[Trait("Domain", "Example - Aggregates")]
		public void UpdateErrorWhenNameIsGreaterThan255Characters()
		{
			var example = _exampleTestFixture.GetValidExample();
			var invalidName = _exampleTestFixture.Faker.Lorem.Letter(256);

			Action action =
				() => example.Update(invalidName);

			action.Should()
				.Throw<BusinessException>()
				.WithMessage("Name should be less or equal 255 characters long");
		}

		[Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreaterThan10_000Characters))]
		[Trait("Domain", "Example - Aggregates")]
		public void UpdateErrorWhenDescriptionIsGreaterThan10_000Characters()
		{
			var example = _exampleTestFixture.GetValidExample();
			var invalidDescription = _exampleTestFixture.Faker.Commerce.ProductDescription();
			while (invalidDescription.Length <= 10_000)
				invalidDescription = $"{invalidDescription} {_exampleTestFixture.Faker.Commerce.ProductDescription()}";

			Action action =
				() => example.Update(_exampleTestFixture.GetValidExampleName(), invalidDescription);

			action.Should()
				.Throw<BusinessException>()
				.WithMessage("Description should be less or equal 10000 characters long");
		}
	}
}
