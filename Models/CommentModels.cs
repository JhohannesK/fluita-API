namespace fluita_API.Models
{
	public class CommentModels
	{
		public class MakeComment
		{
			public required int PostId;
			public required string Content;
			public required int UserId;
		}
	}
}
