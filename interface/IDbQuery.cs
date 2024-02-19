using fluita_API.Entities;
using static fluita_API.Models.UserModels;

namespace fluita_API.@interface
{
	public interface IDbQuery
	{
		GetUsersModel GetUserById(int id);
		ICollection<GetUsersModel> GetAllUsers();
		int ChangeUserDetail(int id, PutUserModel user);
		int CreateUser(SignUpPayload user);
		Users GetUserByUsername(string username);
		bool isUsernameExist(string username);
		int UpdateUserToken(int id, string token, DateTime tokenExp);
		Users GetUsersByToken(string token);

		bool UpdateResetPassword(string password, int Id);
	}
}
