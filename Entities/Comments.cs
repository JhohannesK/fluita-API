using System.ComponentModel.DataAnnotations;

namespace fluita_API.Entities
{
	public class Comments
	{
		[Key]
		public int CommentId { get; set; }
		[StringLength(255)]
		public required string Content { get; set; }
		public  DateTime CreatedAt { get; set; } = DateTime.Now;
		public  DateTime UpdatedAt { get; set; } = DateTime.Now;
		public required int UserId { get; set; }
		public Users? User { get; set; }
		public required int PostId { get; set; }
		public Posts? Post { get; set; }
	}

	
}
