using Domain.Common.Validation;
using Domain.SeedWork;

namespace UnitTests.Domain.SeedWork
{
	public class AggregateRootFake : AggregateRoot
	{
		public override void ValidateId(long id)
		{
			DomainValidation.ValidateId(id);
		}
	}
}
