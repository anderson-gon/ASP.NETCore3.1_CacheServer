using Microsoft.AspNetCore.Mvc;

namespace CacheServer.API.Controllers
{
    public abstract class ApiController : ControllerBase
    {
        protected new IActionResult Response(object result = null, string errorMessage = "")
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                return BadRequest(new
                {
                    success = false,
                    errorMessage = errorMessage
                });
            }

            return Ok(new
            {
                success = true,
                data = result
            });
        }
    }
}
