using Domain.Common.Exceptions;
using Domain.Common.Validation;
using Domain.Sample.Entity;
using Domain.Sample.Repository;
using Domain.SeedWork;

namespace Domain.Sample.Service
{
	public class ExampleDomainService
	{
		private readonly IExampleRepository _sampleRepository;
		private readonly IUnitOfWork _unitOfWork;

		public ExampleDomainService(IExampleRepository sampleRepository, IUnitOfWork unitOfWork)
		{
			_sampleRepository = sampleRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Example> CreateAsync(Entity.Example example, CancellationToken cancellationToken)
		{
		//	DomainValidation.NotNull(example, nameof(example));
			//await ValidateDuplicitySample(example, cancellationToken);
			await _sampleRepository.Insert(example, cancellationToken);
			//await _unitOfWork.Commit(cancellationToken);

			return example;
		}

		public async Task<Example> UpdateAsync(Entity.Example example, CancellationToken cancellationToken)
		{
			DomainValidation.NotNull(example, nameof(example));
			await ValidateDuplicitySample(example, cancellationToken);

			await _sampleRepository.Update(example, cancellationToken);
			await _unitOfWork.Commit(cancellationToken);
			return example;
		}

		public async Task DeactivateAsync(Entity.Example example, CancellationToken cancellationToken)
		{

			DomainValidation.NotNull(example, nameof(example));

			example.Deactivate();

			await _sampleRepository.Update(example, cancellationToken);
			await _unitOfWork.Commit(cancellationToken);
		}

		private async Task ValidateDuplicitySample(Entity.Example example, CancellationToken cancellationToken)
		{
			var exampleDomain = await _sampleRepository.GetByName(example.Name, cancellationToken);

			if (exampleDomain != null)
				throw new BusinessException($"Example {example.Name} existing");
		}
	}
}
