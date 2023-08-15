using Microsoft.AspNetCore.Mvc;

namespace MachineInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    class MachineController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMachine()
        {
            return Ok();
        }
    }
}