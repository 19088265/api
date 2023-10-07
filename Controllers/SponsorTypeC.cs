using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Architecture.Models;

namespace Architecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SponsorTypeC : ControllerBase
    {
        private readonly SponsorTypeIRepository _sponsorTypeIRepository;

        public SponsorTypeC(SponsorTypeIRepository sponsorTypeIRepository)
        {
            _sponsorTypeIRepository = sponsorTypeIRepository;
        }

        [HttpGet]
        [Route("{TypeId:Guid}")]
        public async Task<IActionResult> GetTypeX([FromRoute] Guid TypeId)
        {
            var Type = await _sponsorTypeIRepository.GetSponsorType(TypeId);
            return Ok(Type);
        }
    }
}
