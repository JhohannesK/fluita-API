using System.ComponentModel.DataAnnotations;

namespace fluita_API.Entities
{
	public class Posts
	{
		[Key]
		public int PostId { get; set; }
		[StringLength(255)]
		public required string Content { get; set; }
		public  DateTime CreatedAt { get; set; } = DateTime.Now;
		public  DateTime UpdatedAt { get; set; } = DateTime.Now;
		public int UserId { get; set; }
		public Users? User { get; set; }
		public IEnumerable<Comments>? Comments { get; set; }
	}

	
}
