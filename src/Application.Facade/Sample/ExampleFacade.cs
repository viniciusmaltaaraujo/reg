using Application.DTO.Sample.Example;
using Application.DTO.Sample.Example.Common;
using Domain.Common.Validation;
using Domain.Sample.Repository;
using Domain.Sample.Service;
using DomainEntity = Domain.Sample.Entity;

namespace Application.Facade.Sample
{
    public class ExampleFacade
    {
        private readonly IExampleRepository _exampleRepository;
        private readonly ExampleDomainService _exampleDomainService;

        public ExampleFacade(IExampleRepository exampleRepository, ExampleDomainService exampleDomainService)
        {
            _exampleRepository = exampleRepository;
            _exampleDomainService = exampleDomainService;
        }

        public async Task<ExampleModelOutput> CreateAsync(CreateExampleInput request, CancellationToken cancellationToken)
        {
            DomainValidation.NotNull(request, nameof(request));
            var exampleDomain = new DomainEntity.Example(
                request.Name,
                request.Description,
                request.IsActive
                );

            await _exampleDomainService.CreateAsync(exampleDomain!, cancellationToken!);
            return ExampleModelOutput.FromExample(exampleDomain);

        }

        public async Task<ExampleModelOutput> GetAsync(GetExampleInput request, CancellationToken cancellationToken)
        {
            var exampleDomain = await _exampleRepository.Get(request.Id, cancellationToken);
            return ExampleModelOutput.FromExample(exampleDomain);
        }

        public async Task<ListExamplesOutput> SearchAsync(ListExamplesInput request, CancellationToken cancellationToken)
        {
            var searchOutput = await _exampleRepository.Search(
            new(
                request.Page,
                request.PerPage,
                request.Search,
                request.Sort,
                request.Dir
            ),
            cancellationToken
        );

            return new ListExamplesOutput(
                searchOutput.CurrentPage,
                searchOutput.PerPage,
                searchOutput.Total,
                searchOutput.Items
                    .Select(ExampleModelOutput.FromExample)
                    .ToList()
            );
        }

        public async Task<ExampleModelOutput> UpdateAsync(long id, UpdateExampleInput request, CancellationToken cancellationToken)
        {
            DomainValidation.NotNull(request, nameof(request));

            var exampleDomain = await _exampleRepository.Get(id, cancellationToken);
            exampleDomain.Update(request.Name, request.Description);
            if (
                request.IsActive != null &&
                request.IsActive != exampleDomain.IsActive
            )
                if ((bool)request.IsActive!) exampleDomain.Activate();
                else exampleDomain.Deactivate();

            await _exampleDomainService.UpdateAsync(exampleDomain, cancellationToken!);
            return ExampleModelOutput.FromExample(exampleDomain);
        }

        public async Task<ExampleModelOutput> PatchAsync(long id, PatchExampleInput request, CancellationToken cancellationToken)
        {
            DomainValidation.NotNull(request, nameof(request));

            var exampleDomain = await _exampleRepository.Get(id, cancellationToken);
            DomainValidation.NotFound(request, "Example");


            if (request.Description != null)
                exampleDomain.UpdateDescription(request.Description);

            if (request.IsActive != null 
                && request.IsActive != exampleDomain.IsActive)
                if ((bool)request.IsActive!) exampleDomain.Activate();
                else exampleDomain.Deactivate();


            await _exampleDomainService.UpdateAsync(exampleDomain, cancellationToken!);
            return ExampleModelOutput.FromExample(exampleDomain);
        }

        public async Task DeactivateAsync(DeactivateExampleInput request, CancellationToken cancellationToken)
        {
            DomainValidation.NotNull(request, nameof(request));

            var exampleDomain = await _exampleRepository.Get(request.Id, cancellationToken);
            await _exampleDomainService.DeactivateAsync(exampleDomain, cancellationToken);
        }
    }
}
