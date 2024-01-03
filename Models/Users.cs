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
		public required DateTime CreatedAt { get; set; } = DateTime.Now;
		public required DateTime UpdatedAt { get; set; } = DateTime.Now;
		public ICollection<Posts>? Posts { get; set; }
		public ICollection<Comments>? Comments { get; set; }
	}
}
