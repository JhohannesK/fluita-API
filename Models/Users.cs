using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace fb_API.Models
{
	public class Users
	{
		public int Id { get; set; }
		[StringLength(50)]
		public required string Username { get; set; }
		[DataType(DataType.Password)]
		public required string Password { get; set; }
		[EmailAddress]
		public required string Email { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public DateTime UpdatedAt { get; set; } = DateTime.Now;
		public ICollection<Posts>? Posts { get; set; }
		public ICollection<Comments>? Comments { get; set; }
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
		[Required]
		[DataType(DataType.Password)]
		public required string Password { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[Compare("Password")]
		public required string ConfirmPassword { get; set; }
	}
}
