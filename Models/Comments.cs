using System.ComponentModel.DataAnnotations;

namespace fb_API.Models
{
	public class Comments
	{
		[Key]
		public int CommentId { get; set; }
		[StringLength(255)]
		public required string Content { get; set; }
		public required DateTime CreatedAt { get; set; } = DateTime.Now;
		public required DateTime UpdatedAt { get; set; } = DateTime.Now;
		public int UserId { get; set; }
		public required Users User { get; set; }
		public int PostId { get; set; }
		public required Posts Post { get; set; }
	}
}
