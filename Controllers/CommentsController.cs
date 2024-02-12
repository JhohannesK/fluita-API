using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fluita_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {

        public CommentsController()
        {
        }


        // GET: api/Comments/5
        //[HttpGet("{postId}")]
        //public async Task<ActionResult<List<Comments>>> GetComments(int postId)
        //{
        //    var comments = await _context.Comments.Where(comment => comment.PostId == postId).ToListAsync();

        //    if (comments == null)
        //    {
        //        return NotFound();
        //    }

        //    return comments;
        //}

        // PUT: api/Comments/5

        /*  [HttpPut("{id}")]
          public async Task<IActionResult> PutComments(int id, Comments comments)
          {
              if (id != comments.CommentId)
              {
                  return BadRequest();
              }

              _context.Entry(comments).State = EntityState.Modified;

              try
              {
                  await _context.SaveChangesAsync();
              }
              catch (DbUpdateConcurrencyException)
              {
                  if (!CommentsExists(id))
                  {
                      return NotFound();
                  }
                  else
                  {
                      throw;
                  }
              }

              return NoContent();
          }*/

        // POST: api/Comments
        //    [HttpPost]
        //    public async Task<ActionResult<Comments>> PostComments([FromBody] MakeComment comments)
        //    {
        //        var postComment = new Comments
        //        {
        //            Content = comments.Content,
        //            PostId = comments.PostId,
        //            UserId = comments.UserId
        //        };
        //        _context.Comments.Add(postComment);
        //        await _context.SaveChangesAsync();

        //        var res = new { message = "Comment created successfully" };

        //        return Ok(res);
        //    }

        //    // DELETE: api/Comments/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteComments(int id)
        //    {
        //        var comments = await _context.Comments.FindAsync(id);
        //        if (comments == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Comments.Remove(comments);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool CommentsExists(int id)
        //    {
        //        return _context.Comments.Any(e => e.CommentId == id);
        //    }
    }
}
