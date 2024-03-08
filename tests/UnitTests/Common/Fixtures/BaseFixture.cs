using Bogus;

namespace UnitTests.Common.Fixtures
{
	public abstract class BaseFixture
	{
		private Random _random = new();
		private readonly HashSet<long> _generatedIds = new HashSet<long>();

		public Faker Faker { get; set; }

		protected BaseFixture()
			=> Faker = new Faker("pt_BR");

		public bool GetRandomBoolean()
			=> new Random().NextDouble() < 0.5;

		public long GetRandomId()
		{
			long newId;
			do
			{
				newId = _random.Next(1, 100);
			} while (_generatedIds.Contains(newId));

			_generatedIds.Add(newId);
			return newId;
		}
	}
}
