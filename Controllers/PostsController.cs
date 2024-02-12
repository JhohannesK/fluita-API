using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fluita_API.Models;

namespace fluita_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        public PostsController()
        {
        }

        // GET: api/Posts
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Posts>>> GetPosts()
        //{
        //    return await _context.Posts.ToListAsync();
        //}

        //// GET: api/Posts/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Posts>> GetPosts(int id)
        //{
        //    var posts = await _context.Posts.FindAsync(id);

        //    if (posts == null)
        //    {
        //        return NotFound();
        //    }

        //    return posts;
        //}

        //// PUT: api/Posts/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPosts(int id, Posts posts)
        //{
        //    if (id != posts.PostId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(posts).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PostsExists(id))
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

        //// POST: api/Posts
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Posts>> PostPosts([FromBody] MakePost post)
        //{
        //    var payload = new Posts
        //    {
        //        UserId = post.UserId,
        //        Content = post.Content,
        //    };
        //    _context.Posts.Add(payload);
        //    await _context.SaveChangesAsync();

        //    var res = new { message = "Post was successful." };

        //    return Ok(res);
        //}

        //// DELETE: api/Posts/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePosts(int id)
        //{
        //    var posts = await _context.Posts.FindAsync(id);
        //    if (posts == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Posts.Remove(posts);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool PostsExists(int id)
        //{
        //    return _context.Posts.Any(e => e.PostId == id);
        //}
    }
}
