using System.ComponentModel.DataAnnotations;

namespace fluita_API.Models
{
	public class UserModels
	{

		public class GetUsersModel
		{
			public int Id { get; set; }
			public string Username { get; set; }
			public string Email { get; set; }
			public DateTime CreatedAt { get; set; }

		}

		public class PutUserModel
		{
			public string Username { get; set; }
			public string Email { get; set; }
		}

		public class GetUsersResType<T>
		{
			public string message { get; set; }
			public ICollection<T> data { get; set; }
		}

		public class SignUpPayload
		{
			[Required]
			[StringLength(50)]
			public required string Username { get; set; }
			[Required]
			[DataType(DataType.Password)]
			public required string Password { get; set; }
			[Required]
			[EmailAddress]
			public required string Email { get; set; }
		}

		public class LoginPayload
		{
			[Required]
			[StringLength(50)]
			public required string Username { get; set; }
			[Required]
			[DataType(DataType.Password)]
			public required string Password { get; set; }
		}

		public class UserResponse
		{
			public int Id { get; set; }
			public string Username { get; set; }
			public string Email { get; set; }
		}

		public class ResetPasswordPayload
		{
			public required string ResetToken { get; set; }
			[Required]
			[DataType(DataType.Password)]
			public required string Password { get; set; }
			[Required]
			[DataType(DataType.Password)]
			[Compare("Password")]
			public required string ConfirmPassword { get; set; }
		}

		public class ForgotPasswordPayload
		{
			[Required]
			public required string Username { get; set; }
		}
	}
}
