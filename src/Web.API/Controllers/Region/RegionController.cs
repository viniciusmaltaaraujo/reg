using Application.DTO.Sample.Example;
using Application.DTO.Sample.Example.Common;
using Application.Facade.Sample;
using Microsoft.AspNetCore.Mvc;
using Web.API.ApiModels.Response;
using Asp.Versioning;

namespace Web.API.Controllers.Region
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class RegionController : ControllerBase
    {
        private readonly ExampleFacade _exampleFacade;

        public RegionController(ExampleFacade exampleFacade)
        {
            _exampleFacade = exampleFacade;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<ExampleModelOutput>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(
                        CancellationToken cancellationToken
                        )
        {
            var request = new CreateExampleInput("UpdateDev", "Inserindo Registro", true);
            var response = await _exampleFacade.CreateAsync(request, cancellationToken);
            return CreatedAtAction(
                nameof(Create),
                new { response.Id },
                new ApiResponse<ExampleModelOutput>(response)
            );
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<ExampleModelOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(
                        CancellationToken cancellationToken
                    )
        {
            var output = new ExampleModelOutput(1, "UpdateDev", "Inserindo Registro", true, DateTime.Now);
            return Ok(new ApiResponse<ExampleModelOutput>(output));
        }



    }
}
