using Infra.Repository.Interfaces;

namespace Application.Facade.Health
{
    public class HealthFacade
	{
		private readonly IHealthRepository _healthRepository;

		public HealthFacade(IHealthRepository healthRepository) => _healthRepository = healthRepository;

		public void IsReady() => _healthRepository.IsReady();
	}
}
