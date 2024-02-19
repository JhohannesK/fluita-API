using Microsoft.AspNetCore.Mvc;
using fluita_API.@interface;
using static fluita_API.Models.UserModels;
using fb_API.Services;

namespace fluita_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IDbQuery _DbQuery;
        public string connectionString;

        public UsersController( IEmailSender emailSender, IConfiguration configuration, IDbQuery dbQuery)
        {
            _DbQuery = dbQuery;
            _emailSender = emailSender;
            _configuration = configuration;
            connectionString = _configuration["ConnectionStrings:fluitaContext"];
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<GetUsersResType<GetUsersModel>> GetUsers()
        {
           var users = _DbQuery.GetAllUsers();
            var response = new
            {
				message = "Users retrieved successfully",
				data = users
			};
			return Ok(response);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<GetUsersResType<GetUsersModel>> GetUserById(int id)
        {
            var users = _DbQuery.GetUserById(id);

            var response = new
            {
                message = "User retrieved successfully",
                data = users
            };

            if (users == null)
            {
                return NotFound("User with such ID not found!!");
            }

            return Ok(response);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public ActionResult PutUsers(int id, PutUserModel users)
        {
             var r = _DbQuery.ChangeUserDetail(id, users);

            if (r == 0)
            {
				return NotFound("User with such ID not found!!");
			}

            var response = new
            {
				message = "User updated successfully"
			};
            return Ok(response);
        }

        // POST: api/Users
        [HttpPost("signup")]
        public async Task<ActionResult> CreateUser([FromBody] SignUpPayload user)
        {

            var HashPassword = PasswordHasher.Hash(user.Password);

            var response = _DbQuery.CreateUser(new SignUpPayload
			{
				Username = user.Username,
				Password = HashPassword,
				Email = user.Email,
			});

            var receiver = user.Email;
            var subject = "Welcome to FLUITA";
            var message = "<h1>Thank you for signing up to Fluita</h1>";
            var res = new { message = "User Created" };

            if (response == -1)
            {
				return BadRequest("User already exists!!");
			}
            await _emailSender.SendEmailAsync(receiver, subject, message);
            //return CreatedAtAction("SignUpUser", new { id = user.Id }, response);
            return Ok(res);
        }

        //POST: api/Users/login
        [HttpPost("login")]
		public ActionResult<GetUsersResType<GetUsersModel>> LoginUser([FromBody] LoginPayload users)
        {
            var user = _DbQuery.GetUserByUsername(users.Username);
            if (user == null)
            {
                var message = new { message = "User does not exist!!" };
                return NotFound(message);
            }

            var isValid = PasswordHasher.Verify(users.Password, user.Password);
            if (!isValid)
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            var a = new GetUsersModel
            {
                Username = user.Username,
                Id = user.Id,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };

            var response = new
            {
				message = "User logged in successfully",
				data = a
			};

            return Ok(response);
        }

        // DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUsers(int id)
        //{
        //    var users = await _context.Users.FindAsync(id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(users);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool UsersExists(int id)
        //{
        //    return _context.Users.Any(e => e.Id == id);
        //}

        //      [HttpGet("user/find-username")]
        //      public async Task<ActionResult> UsernameExists([FromBody] string username)
        //      {
        //	var isExisting =  _context.Users.Any(e => e.Username == username);
        //          if (isExisting)
        //          {
        //		return Ok("User exist");
        //	}
        //	else
        //          {
        //		return NotFound($"No user with username: {username}");
        //	}
        //}
    }
}
