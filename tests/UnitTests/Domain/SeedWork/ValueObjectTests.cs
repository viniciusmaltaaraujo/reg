using Bogus.DataSets;
using Domain.SeedWork;

namespace UnitTests.Domain.SeedWork
{

	public class ValueObjectTests
	{

		[Fact(DisplayName = nameof(Equals_WithEqualObjects_ReturnsTrue))]
		[Trait("Domain", "SeedWork - ValueObject")]
		public void Equals_WithEqualObjects_ReturnsTrue()
		{
			// Arrange
			var address1 = new Address("123 Main St", "Townsville");
			var address2 = new Address("123 Main St", "Townsville");

			// Act & Assert
			Assert.True(address1.Equals(address2));
			Assert.Equal(address1, address2);
		}

		[Fact(DisplayName = nameof(Equals_WithDifferentObjects_ReturnsFalse))]
		[Trait("Domain", "SeedWork - ValueObject")]
		public void Equals_WithDifferentObjects_ReturnsFalse()
		{
			// Arrange
			var address1 = new Address("123 Main St", "Townsville");
			var address2 = new Address("456 Main St", "Townsville");

			// Act & Assert
			Assert.False(address1.Equals(address2));
		}

		[Fact(DisplayName = nameof(GetHashCode_WithEqualObjects_HaveSameHashCode))]
		[Trait("Domain", "SeedWork - ValueObject")]
		public void GetHashCode_WithEqualObjects_HaveSameHashCode()
		{
			// Arrange
			var address1 = new Address("123 Main St", "Townsville");
			var address2 = new Address("123 Main St", "Townsville");

			// Act & Assert
			Assert.Equal(address1.GetHashCode(), address2.GetHashCode());
		}

		[Fact(DisplayName = nameof(EqualityOperator_WithEqualObjects_ReturnsTrue))]
		[Trait("Domain", "SeedWork - ValueObject")]
		public void EqualityOperator_WithEqualObjects_ReturnsTrue()
		{
			// Arrange
			var address1 = new Address("123 Main St", "Townsville");
			var address2 = new Address("123 Main St", "Townsville");

			// Act & Assert
			Assert.True(address1 == address2);
		}


		[Fact(DisplayName = nameof(InequalityOperator_WithDifferentObjects_ReturnsTrue))]
		[Trait("Domain", "SeedWork - ValueObject")]
		public void InequalityOperator_WithDifferentObjects_ReturnsTrue()
		{
			// Arrange
			var address1 = new Address("123 Main St", "Townsville");
			var address2 = new Address("456 Main St", "Townsville");

			// Act & Assert
			Assert.True(address1 != address2);
		}
	}

	public class Address : ValueObject
	{
		public string Street { get; }
		public string City { get; }

		public Address(string street, string city)
		{
			Street = street;
			City = city;
		}

		public override bool Equals(ValueObject? other)
		{
			if (other is not Address otherAddress) return false;

			return Street == otherAddress.Street && City == otherAddress.City;
		}

		protected override int GetCustomHashCode() => HashCode.Combine(Street, City);
	}
}

