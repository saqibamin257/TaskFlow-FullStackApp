using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObservabilityController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("Telemetry working");
        }

        [HttpGet("slow")]
        public async Task<IActionResult> Slow()
        {
            await Task.Delay(5000);

            return Ok("Slow endpoint completed");
        }

        [HttpGet("error")]
        public IActionResult Error()
        {
            throw new Exception("Test exception");
        }

        [HttpGet("cpu")]
        public IActionResult Cpu()
        {
            long result = 0;

            for (long i = 0; i < 500000000; i++)
            {
                result += i;
            }

            return Ok(result);
        }
    }
}
