using Microsoft.AspNetCore.Mvc;
using fb_API.Models;
using fb_API.Data;

namespace fb_API.Controllers
{
		[Route("api/[controller]")]
		[ApiController]

	public class ResetPasswordController : Controller
	{
		private readonly fb_APIContext _context;

		public ResetPasswordController(fb_APIContext context)
		{
			_context = context;
		}

		[HttpPut]
		public IActionResult ResetPassword([FromBody] ResetPasswordPayload data)
		{
			//_context.Update();
			return StatusCode(400);
		}
	}
}
