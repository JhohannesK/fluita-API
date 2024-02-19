using fluita_API.Entities;
using fluita_API.@interface;
using fluita_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using static fluita_API.Models.UserModels;

namespace fluita_API.utils.Queries
{
    public class UserDbQuery : IDbQuery
    {
        private readonly IConfiguration _config;
        private string _connectionString;

        public UserDbQuery(IConfiguration config)
        {
            _config = config;
            _connectionString = _config["ConnectionStrings:fluitaContext"];

        }
        public ICollection<GetUsersModel> GetAllUsers()
        {
            var query = "SELECT * FROM Users";
            List<GetUsersModel> users = new List<GetUsersModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var user = new GetUsersModel
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Email = reader["Email"].ToString(),
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                    };
                    users.Add(user);
                }
            }
            return users;
            throw new NotImplementedException();
        }

        public GetUsersModel GetUserById(int id)
        {
            string query = "SELECT * FROM Users WHERE Id = @Id";
            GetUsersModel user = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user = new GetUsersModel
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Email = reader.GetString(2),
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                    };
                }
            }
            return user;
            throw new NotImplementedException();
        }

        public int ChangeUserDetail(int id, PutUserModel user)
        {
            bool isUser = isUserExist(id);
            const string query = "UPDATE Users SET Username = @Username, Email = @Email WHERE Id = @Id";
            if (isUser)
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Email", user.Email);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            return -1;
        }

        public int UpdateUserToken(int id, string token, DateTime tokenExp)
        {
			const string query = "UPDATE Users SET ResetToken = @ResetToken, ResetTokenExpiration = @ResetTokenExpiration WHERE Id = @Id";
			using (SqlConnection conn = new SqlConnection(_connectionString))
            {
				SqlCommand cmd = new SqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@Id", id);
				cmd.Parameters.AddWithValue("@ResetToken", token);
				cmd.Parameters.AddWithValue("@ResetTokenExpiration", tokenExp);

				conn.Open();
				int rowsAffected = cmd.ExecuteNonQuery();
				if (rowsAffected > 0)
                {
					return 1;
				}
				return -1;
			}
		}

        public int CreateUser(SignUpPayload user)
        {
			const string query = "INSERT INTO Users (Username, Password, Email, CreatedAt, UpdatedAt) VALUES (@Username, @Password, @Email, @CreatedAt, @UpdatedAt)";

            if (isUsernameExist(user.Username))
            {
				return -1;
			}
			using (SqlConnection conn = new SqlConnection(_connectionString))
            {
				SqlCommand cmd = new SqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@Username", user.Username);
				cmd.Parameters.AddWithValue("@Password", user.Password);
				cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

				conn.Open();
				int rowsAffected = cmd.ExecuteNonQuery();
				return rowsAffected;
			}
		}   

        public Users GetUserByUsername(string username)
        {
			const string query = "SELECT * FROM Users WHERE Username = @Username";
			using (SqlConnection conn = new SqlConnection(_connectionString))
            {
				SqlCommand cmd = new SqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@Username", username);

				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
                {
					var user = new Users
                    {
						Id = reader.GetInt32(0),
						Username = reader.GetString(1),
						Password = reader.GetString(2),
						Email = reader.GetString(3),
						CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
						UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
					};
					return user;
				}
			}
			return null;
		}

        public Users GetUsersByToken(string token)
        {
            const string query = "SELECT * FROM Users WHERE ResetToken = @resetToken";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@resetToken", token);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    var user = new Users
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Username = (reader["Username"].ToString()),
                        Email = reader["Email"].ToString(),
                        Password = reader.GetString(2),
                       ResetToken = reader["ResetToken"].ToString(),
                       ResetTokenExpiration = DateTime.Parse(reader["ResetTokenExpiration"].ToString())
                    };

                    return user;
                }
            }
            return null;
        }

        public bool UpdateResetPassword(string newPassword, int Id)
        {
            const string query = "UPDATE USERS SET Password = @Password, UpdateAt = @UpdatedAt WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Password", newPassword);
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                command.Parameters.AddWithValue("@Id", Id);
                
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }

		private bool isUserExist(int id)
        {
            const string query = "SELECT * FROM Users WHERE Id = @Id";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
				SqlCommand cmd = new SqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@Id", id);

				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
                {
					return true;
				}
				return false;
			}
        }
        public bool isUsernameExist(string username)
        {
			const string query = "SELECT * FROM Users WHERE Username = @Username";
			using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
            }
               return false;
        }

    }
}
