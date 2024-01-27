using Microsoft.AspNetCore.Mvc;
using TaskService.Domain.Interfaces.IServices;

namespace TaskService.Controllers
{
    [Route("api/auth")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [Route("generate-auth-token")]
        public IActionResult GenerateMockAuthToken()
        {
            var token = _identityService.GenerateMockToken();

            return Ok(token);
        }
    }
}
