using Application.Facade.Health;
using Microsoft.AspNetCore.Mvc;
using Web.API.ApiModels.Response;

namespace Web.API.Controllers.Health
{
    [ApiController]
	[Route("[controller]")]
	public class HealthController : Controller
    {
        private readonly HealthFacade _healthFacade;

        public HealthController(HealthFacade healthFacade)
        {
            _healthFacade = healthFacade;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        public ActionResult Get()
        {
         //   _healthFacade.IsReady();
            return Ok("Healthy");
        }

        [HttpGet("Ready")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        public ActionResult Ready()
        {
            return Ok("UP");
        }
    }
}
