using fluita_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace fluita_API.utils
{
	public class _DbQuery
	{
		private readonly IConfiguration _config;
		private string _connectionString;

		public _DbQuery(IConfiguration config)
		{
			_config = config;
			_connectionString = _config["ConnectionStrings:fb_APIContext"];

		}
		public Users GetUserById(int id)
		{
			string query = "SELECT * FROM Users WHERE Id = @Id";
			Users user = null;

			using(SqlConnection conn = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@Id", id);

				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					user = new Users
					{
						Id = reader.GetInt32(0),
						Username = reader.GetString(1),
						Email = reader.GetString(2),
						CreatedAt = reader.GetDateTime(3),
						Password = reader.GetString(4),
						
					};
				}
			}
			return user;
		}
	}
}
