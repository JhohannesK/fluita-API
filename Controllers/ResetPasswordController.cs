using fb_API.Services;
using fluita_API.@interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static fluita_API.Models.UserModels;

namespace fluita_API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]

	public class ResetPasswordController : Controller
	{
		private readonly IEmailSender _emailSender;
		private readonly IDbQuery _dbQuery;

		public ResetPasswordController( IEmailSender emailSender, IDbQuery dbQuery)
		{
			_emailSender = emailSender;
			_dbQuery = dbQuery;
		}

		[HttpPost]
		[Route("forgot")]
		public IActionResult ForgotPassword([FromBody] ForgotPasswordPayload data)
		{

			var user = _dbQuery.GetUserByUsername(data.Username);

			if(user == null)
			{
				return NotFound("User does not exist!!");
			}
			else
			{
				string token = GenerateToken();
				DateTime expiration = DateTime.Now.AddHours(1);

				if(!token.IsNullOrEmpty() && expiration > DateTime.Now)
				{
					_dbQuery.UpdateUserToken(user.Id, token, expiration);
				}
				else
				{
					return StatusCode(500, "Token generation failed!");
				}

				_emailSender.SendEmailAsync(user.Email, "Reset Password", $"Click the link to reset your password: https://localhost:5001/api/resetpassword/{token}");
			}
			return Ok($"Email sent to {user.Email}");
		}

		[HttpPut]
		public IActionResult ResetPassword([FromBody] ResetPasswordPayload data)
		{
			var user = _dbQuery.GetUsersByToken(data.ResetToken);

			if (user == null)
			{
				return StatusCode(400, "Token must have expired or might be wrong");
			}
			else
			{
				var token_active = VerifyToken(user.ResetTokenExpiration.ToString());
				if (token_active == false)
				{
					return StatusCode(400, "Token has expired!");
				}

				if (user != null && token_active)
				{
					var hashPassword = PasswordHasher.Hash(data.Password);

					bool isQuerySuccesfull = _dbQuery.UpdateResetPassword(hashPassword, user.Id);

					if (isQuerySuccesfull == false)
					{
						return StatusCode(500, "Could not update password");
					}
					return Ok("Password reset successfully!");
				}
			}

			return StatusCode(400);
		}

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
