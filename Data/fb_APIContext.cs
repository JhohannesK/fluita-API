using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using fb_API.Models;

namespace fb_API.Data
{
    public class fb_APIContext : DbContext
    {
        public fb_APIContext (DbContextOptions<fb_APIContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; } = default!;
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Comments> Comments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<Users>()
				.HasIndex(u => u.Username)
				.IsUnique();

			modelBuilder.Entity<Comments>()
				.HasOne(c => c.User)
				.WithMany(u => u.Comments)
				.HasForeignKey(c => c.UserId)
				.OnDelete(DeleteBehavior.ClientNoAction); 


			base.OnModelCreating(modelBuilder);
		}
	}
}
