using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using fb_API.Models;
using fb_API.Services;
using Microsoft.Data.SqlClient;
using fb_API.utils;

namespace fb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly _DbQuery _DbQuery;
        public string connectionString;

        public UsersController( IEmailSender emailSender, IConfiguration configuration, _DbQuery dbQuery)
        {
            _DbQuery = dbQuery;
            _emailSender = emailSender;
            _configuration = configuration;
            connectionString = _configuration["ConnectionStrings:fb_APIContext"];
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<GetUsersModel>> GetUsers()
        {
            var query = "SELECT * FROM Users";

            using(SqlConnection conn = new SqlConnection(connectionString)) {
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                var users = new List<GetUsersModel>();
                while (reader.Read())
                {
                   users.Add(new GetUsersModel
                   {
					   Id = reader.GetInt32(0),
					   Username = reader.GetString(1),
					   Email = reader.GetString(2),
				   });
                }
					return Ok(users);
            }
            
        }

        // GET: api/Users/5
        //[HttpGet("{id}")]
        //public ActionResult<UserResponse> GetAllUsers(int id)
        //{
        //    var users = _DbQuery.GetUserById(id);

        //    var response = new UserResponse
        //    {
        //        Id = users.Id,
        //        Username = users.Username,
        //        Email = users.Email,
        //    };

        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    return response;
        //}

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUsers(int id, Users users)
        //{
        //    if (id != users.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(users).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UsersExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost("signup")]
        //public async Task<ActionResult<Users>> CreateUser([FromBody] SignUpPayload users)
        //{

        //    var HashPassword = PasswordHasher.Hash(users.Password);
        //    var user = new Users
        //    {
        //        Username = users.Username,
        //        Password = HashPassword,
        //        Email = users.Email,
        //    };
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    var response = new
        //    {
        //        UserId = user.Id,
        //        Username = user.Username,
        //        Email = user.Email,
        //    };

        //    var receiver = user.Email;
        //    var subject = "Welcome to Facebook vClone";
        //    var message = "<h1>Thank you for signing up to Facebook</h1>";
        //    var res = new { message = "User Created"};

        //    await _emailSender.SendEmailAsync(receiver, subject, message);
        //    //return CreatedAtAction("SignUpUser", new { id = user.Id }, response);
        //    return Ok(res);
        //}

        // POST: api/Users/login
        //      [HttpPost("login")]
        //      public async Task<ActionResult<Users>> LoginUser([FromBody] LoginPayload users)
        //      {
        //	var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == users.Username);
        //	if (user == null)
        //          {
        //              var message = new { message = "User does not exist!!" };
        //		return NotFound(message);
        //	}

        //	var isValid = PasswordHasher.Verify(users.Password, user.Password);
        //	if (!isValid)
        //          {
        //		return BadRequest(new { message = "Invalid Credentials" });
        //	}

        //	var response = new
        //          {
        //		UserId = user.Id,
        //		Username = user.Username,
        //		Email = user.Email,
        //	};

        //	return Ok(response);
        //}

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
