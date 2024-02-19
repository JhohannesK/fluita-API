using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace fluita_API.Entities
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
		public string? ResetToken { get; set; }
		public DateTime? ResetTokenExpiration { get; set; }
	}
}
