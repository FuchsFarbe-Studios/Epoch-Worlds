using EpochApp.Server.Data;
using EpochApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///    Controller for managing blogs and blog posts.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly EpochDataDbContext _context;

        /// <summary>
        ///   Constructor for BlogsController.
        /// </summary>
        /// <param name="context"> The database context. </param>
        public BlogsController(EpochDataDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///    Get all blogs.
        /// </summary>
        /// <returns> A list of <see cref="BlogDTO"/>. </returns>
        [HttpGet]
        [Authorize(Roles = "ADMIN,INTERNAL")]
        public async Task<ActionResult<IEnumerable<BlogDTO>>> GetBlogs()
        {
            return await _context.Blogs.Select(x => new BlogDTO
                                                    {
                                                        BlogID = x.BlogID,
                                                        BlogType = x.BlogType,
                                                        Name = x.Name,
                                                        CreatedOn = x.CreatedOn,
                                                        CreatedBy = x.CreatedBy,
                                                        ModifiedOn = x.ModifiedOn,
                                                        ModifiedBy = x.ModifiedBy
                                                    })
                                 .ToListAsync();
        }

        /// <summary>
        ///   Get all blogs of a specific type.
        /// </summary>
        /// <param name="blogType"> The blog type. </param>
        /// <returns> A list of <see cref="BlogDTO"/>. </returns>
        [HttpGet("Type/{blogType}")]
        public async Task<ActionResult<IEnumerable<BlogDTO>>> GetBlogsByType(BlogType blogType)
        {
            return await _context.Blogs
                                 .Where(x => x.BlogType == blogType)
                                 .Select(x => new BlogDTO
                                              {
                                                  BlogID = x.BlogID,
                                                  BlogType = x.BlogType,
                                                  Name = x.Name,
                                                  CreatedOn = x.CreatedOn,
                                                  CreatedBy = x.CreatedBy,
                                                  ModifiedOn = x.ModifiedOn,
                                                  ModifiedBy = x.ModifiedBy
                                              })
                                 .ToListAsync();
        }

        // Get blog posts
        /// <summary>
        ///  Get all blog posts for a specific blog.
        /// </summary>
        /// <param name="blogId"> The blog id. </param>
        /// <returns> A list of <see cref="PostDTO"/>. </returns>
        [HttpGet("BlogPosts/{blogId:int}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetBlogPosts(int blogId)
        {
            var blogPosts = await _context.Blogs.Where(x => x.BlogID == blogId)
                                          .SelectMany(x => x.BlogPosts)
                                          .Include(x => x.Post)
                                          .OrderByDescending(x => x.PostedOn)
                                          .ToListAsync();
            var posts = new List<PostDTO>();
            foreach (var blogPost in blogPosts)
            {
                var post = blogPost.Post;
                var postDto = new PostDTO
                              {
                                  BlogId = blogPost.BlogID,
                                  PostID = post.PostID,
                                  PostType = post.PostType,
                                  Author = post.Author,
                                  Title = post.Title,
                                  Content = post.Content,
                                  OutsideLink = post.Content,
                                  ScheduledTime = post.ScheduledTime,
                                  PostedOn = post.PostedOn,
                                  ModifiedOn = post.ModifiedOn,
                                  ModifiedBy = post.ModifiedBy
                              };
                posts.Add(postDto);
            }
            return Ok(posts);
        }

        /// <summary>
        ///  Get all blog posts of a specific type.
        /// </summary>
        /// <param name="type"> The blog type. </param>
        /// <returns> A list of <see cref="PostDTO"/>. </returns>
        [HttpGet("BlogPosts/Type/{type}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetBlogPostsByType(int type)
        {
            var blogPosts = await _context.Blogs.Where(x => (int)x.BlogType == type)
                                          .SelectMany(x => x.BlogPosts)
                                          .Include(x => x.Post)
                                          .OrderByDescending(x => x.PostedOn)
                                          .ToListAsync();
            var posts = new List<PostDTO>();
            foreach (var blogPost in blogPosts)
            {
                var post = blogPost.Post;
                var postDto = new PostDTO
                              {
                                  BlogId = blogPost.BlogID,
                                  PostID = post.PostID,
                                  PostType = post.PostType,
                                  Author = post.Author,
                                  Title = post.Title,
                                  Content = post.Content,
                                  OutsideLink = post.Content,
                                  ScheduledTime = post.ScheduledTime,
                                  PostedOn = post.PostedOn,
                                  ModifiedOn = post.ModifiedOn,
                                  ModifiedBy = post.ModifiedBy
                              };
                posts.Add(postDto);
            }
            return Ok(posts);
        }

        // Create blog post
        /// <summary>
        /// Create a new blog post.
        /// </summary>
        /// <param name="blogId"> The blog id. </param>
        /// <param name="postDto"> The post data. </param>
        /// <returns> A <see cref="Task{T}"/> where TResult is <see cref="ActionResult{T}"/> where TValue is <see cref="PostDTO"/>. </returns>
        [HttpPost("BlogPosts/{blogId}")]
        [Authorize(Roles = "ADMIN,INTERNAL")]
        public async Task<ActionResult<PostDTO>> CreateBlogPost(int blogId, PostDTO postDto)
        {
            var blog = await _context.Blogs.FindAsync(blogId);
            if (blog == null)
            {
                return NotFound();
            }
            var post = new Post
                       {
                           PostID = postDto.PostID,
                           PostType = postDto.PostType,
                           Author = postDto.Author,
                           Title = postDto.Title,
                           Content = postDto.Content,
                           OutsideLink = postDto.OutsideLink,
                           ScheduledTime = postDto.ScheduledTime.Value,
                           PostedOn = postDto.PostedOn,
                           ModifiedOn = postDto.ModifiedOn,
                           ModifiedBy = postDto.ModifiedBy
                       };
            var blogPost = new BlogPost
                           {
                               BlogID = blogId,
                               Post = post,
                               PostedOn = DateTime.Now
                           };
            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBlogPost", new { id = post.PostID }, postDto);
        }

        /// <summary> Get a blog by id. </summary>
        /// <param name="id"> The blog id. </param>
        /// <returns>
        ///     <see cref="Task{T}" /> where TResult is <see cref="ActionResult{T}" /> where TValue is <see cref="BlogDTO" />.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDTO>> GetBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }
            var data = new BlogDTO
                       {
                           BlogID = blog.BlogID,
                           BlogType = blog.BlogType,
                           Name = blog.Name,
                           CreatedOn = blog.CreatedOn,
                           CreatedBy = blog.CreatedBy,
                           ModifiedOn = blog.ModifiedOn,
                           ModifiedBy = blog.ModifiedBy
                       };

            return Ok(data);
        }

        /// <summary> Update a blog. </summary>
        /// <param name="id"> The blog id. </param>
        /// <param name="blogData"> The blog data. </param>
        /// <returns>
        ///     <see cref="IActionResult" />
        /// </returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,INTERNAL")]
        public async Task<IActionResult> PutBlog(int id, BlogDTO blogData)
        {
            if (id != blogData.BlogID)
            {
                return BadRequest();
            }

            var blog = await _context.Blogs.FindAsync(id);
            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Blogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        ///   Create a new blog.
        /// </summary>
        /// <param name="blogData"> The blog data. </param>
        /// <returns> A <see cref="Task{T}"/> where TResult is <see cref="ActionResult{T}"/> where TValue is <see cref="Blog"/>. </returns>
        [HttpPost]
        [Authorize(Roles = "ADMIN,INTERNAL")]
        public async Task<ActionResult<Blog>> PostBlog(BlogDTO blogData)
        {
            var blog = new Blog
                       {
                           BlogType = blogData.BlogType,
                           Name = blogData.Name,
                           CreatedOn = DateTime.Now,
                           CreatedBy = blogData.CreatedBy,
                           ModifiedOn = null,
                           ModifiedBy = null,
                           BlogPosts = null
                       };
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlog", new { id = blog.BlogID }, blog);
        }

        // DELETE: api/Blogs/5
        /// <summary>
        /// Delete a blog.
        /// </summary>
        /// <param name="id"> The blog id. </param>
        /// <returns> A <see cref="Task{T}"/> where TResult is <see cref="ActionResult{T}"/> where TValue is <see cref="Blog"/>. </returns>
        [Authorize(Roles = "ADMIN,INTERNAL")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.BlogID == id);
        }

        /// <summary>
        /// Get a blog post by id.
        /// </summary>
        /// <param name="id"> The post id. </param>
        /// <returns> A <see cref="Task{T}"/> where TResult is <see cref="PostDTO"/>. </returns>
        [AllowAnonymous]
        [HttpGet("BlogPosts/Post/{id:guid}")]
        public async Task<PostDTO> GetBlogPost(Guid id)
        {
            return await _context.Posts
                                 .Where(x => x.PostID == id)
                                 .Select(x => new PostDTO
                                              {
                                                  // ReSharper disable once VariableHidesOuterVariable
                                                  BlogId = x.BlogPosts.Select(x => x.BlogID).FirstOrDefault(),
                                                  PostID = x.PostID,
                                                  PostType = x.PostType,
                                                  Author = x.Author,
                                                  Title = x.Title,
                                                  Content = x.Content,
                                                  OutsideLink = x.OutsideLink,
                                                  ScheduledTime = x.ScheduledTime,
                                                  PostedOn = x.PostedOn,
                                                  ModifiedOn = x.ModifiedOn,
                                                  ModifiedBy = x.ModifiedBy
                                              })
                                 .FirstOrDefaultAsync();
        }
    }
}