using System.ComponentModel.DataAnnotations;

namespace fb_API.Models
{
	public class Posts
	{
		[Key]
		public int PostId { get; set; }
		[StringLength(255)]
		public required string Content { get; set; }
		public required DateTime CreatedAt { get; set; } = DateTime.Now;
		public required DateTime UpdatedAt { get; set; } = DateTime.Now;
		public int UserId { get; set; }
		public required Users User { get; set; }
		public IEnumerable<Comments>? Comments { get; set; }
	}
}
