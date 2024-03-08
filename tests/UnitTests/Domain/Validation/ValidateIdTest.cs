using Bogus;
using Domain.Common.Exceptions;
using Domain.Common.Validation;
using FluentAssertions;

namespace UnitTests.Domain.Validation
{
	public class ValidateIdTest
	{
		private Faker Faker { get; set; } = new Faker();


		[Fact(DisplayName = nameof(ValidateIdNotNullOrEmptyStringOk))]
		[Trait("Domain", "DomainValidation - Validation")]
		public void ValidateIdNotNullOrEmptyStringOk()
		{
			string id = Faker.Random.AlphaNumeric(10);
			Action action = () => DomainValidation.ValidateId(id);
			action.Should().NotThrow();
		}

		[Fact(DisplayName = nameof(ValidateIdThrowWhenStringIsEmpty))]
		[Trait("Domain", "DomainValidation - Validation")]
		public void ValidateIdThrowWhenStringIsEmpty()
		{
			string id = "";
			Action action = () => DomainValidation.ValidateId(id);
			action.Should().Throw<NotFoundException>().WithMessage("Id should not be empty or null");
		}

		[Fact(DisplayName = nameof(ValidateIdNotNullOrEmptyGuidOk))]
		[Trait("Domain", "DomainValidation - Validation")]
		public void ValidateIdNotNullOrEmptyGuidOk()
		{
			Guid id = Guid.NewGuid();
			Action action = () => DomainValidation.ValidateId(id);
			action.Should().NotThrow();
		}

		[Fact(DisplayName = nameof(ValidateIdNull))]
		[Trait("Domain", "DomainValidation - Validation")]
		public void ValidateIdNull()
		{
			object id = null!;
			Action action = () => DomainValidation.ValidateId(id);
			action.Should().Throw<NotFoundException>().WithMessage("Id should not be null");
		}

		[Fact(DisplayName = nameof(ValidateIdThrowWhenGuidIsEmpty))]
		[Trait("Domain", "DomainValidation - Validation")]
		public void ValidateIdThrowWhenGuidIsEmpty()
		{
			Guid id = Guid.Empty;
			Action action = () => DomainValidation.ValidateId(id);
			action.Should().Throw<NotFoundException>().WithMessage("Id should not be empty");
		}

		[Theory(DisplayName = nameof(ValidateIdThrowWhenNumericIdIsZero))]
		[Trait("Domain", "DomainValidation - Validation")]
		[InlineData(0)]
		[InlineData(0L)]
		[InlineData(0.0)]
		public void ValidateIdThrowWhenObject(object id)
		{
			Action action = () => DomainValidation.ValidateId(id);
			action.Should().Throw<NotFoundException>().WithMessage("Id should not a object");
		}

		[Theory(DisplayName = nameof(ValidateIdThrowWhenNumericIdIsZero))]
		[Trait("Domain", "DomainValidation - Validation")]
		[InlineData(0)]
		[InlineData(0L)]
		[InlineData(0.0)]
		public void ValidateIdThrowWhenNumericIdIsZero(long id)
		{
			Action action = () => DomainValidation.ValidateId(id);
			action.Should().Throw<NotFoundException>().WithMessage($"Id should not be zero");
		}

		[Theory(DisplayName = nameof(ValidateIdNumericIdOk))]
		[Trait("Domain", "DomainValidation - Validation")]
		[InlineData(1)]
		[InlineData(1)]
		[InlineData(1.1)]
		public void ValidateIdNumericIdOk(long id)
		{
			Action action = () => DomainValidation.ValidateId(id);
			action.Should().NotThrow();
		}
	}
}
