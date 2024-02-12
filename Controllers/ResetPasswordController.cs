using Microsoft.AspNetCore.Mvc;
using fb_API.Models;
using fb_API.Services;

namespace fb_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class ResetPasswordController : Controller
	{
		private readonly IEmailSender _emailSender;

		public ResetPasswordController( IEmailSender emailSender)
		{
			_emailSender = emailSender;
		}

		//[HttpPost]
		//[Route("forgot")]
		//public IActionResult ForgotPassword([FromBody] ForgotPasswordPayload data)
		//{
		//	var user = _context.Users.FirstOrDefault(u => u.Username == data.Username);
		//	if (user == null)
		//	{
		//		return NotFound("User does not exist!!");
		//	}
		//	else
		//	{
		//		var token = GenerateToken();
		//		DateTime expiration = DateTime.Now.AddHours(1);
		//		user.ResetToken = token;
		//		user.ResetTokenExpiration = expiration;
		//		_context.SaveChanges();

		//		_emailSender.SendEmailAsync(user.Email, "Reset Password", $"Click the link to reset your password: https://localhost:5001/api/resetpassword/{token}");
		//	}
		//	return Ok($"Email sent to {user.Email}");
		//}

		//[HttpPut]
		//public IActionResult ResetPassword([FromBody] ResetPasswordPayload data)
		//{
		//	var user = _context.Users.FirstOrDefault(u => u.ResetToken == data.ResetToken);

		//	var token_active = VerifyToken(user.ResetTokenExpiration.ToString());
		//	if (token_active == false)
		//	{
		//		return StatusCode(400, "Token has expired!");
		//	}
		//	if (user != null && token_active)
		//	{
		//		var hashPassword = PasswordHasher.Hash(data.Password);
		//		user.Password = hashPassword;
		//		user.ResetToken = null;
		//		_context.SaveChanges();
		//		return Ok("Password reset successfully!");
		//	}
		//	return StatusCode(400);
		//}

		public static string GenerateToken()
		{
			byte[] RandomBytes = new byte[32];

			using (var generator = System.Security.Cryptography.RandomNumberGenerator.Create())
			{
				generator.GetBytes(RandomBytes);
				return Convert.ToBase64String(RandomBytes);
			}
		}

		public static bool VerifyToken(string expiryDate)
		{
			DateTime expiration = DateTime.Parse(expiryDate);
			if (expiration > DateTime.Now)
			{
				return true;
			}
			return false;
		}
	}
}
