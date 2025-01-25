using Gettit.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gettit.Web.Data
{
    public class GettitDbContext : IdentityDbContext<GettitUser>
    {
        public DbSet<Attachment> Attachments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<GettitThread> Threads { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Reaction> Reactions { get; set; }

        public DbSet<GettitRole> ForumRoles { get; set; }

        public GettitDbContext(DbContextOptions<GettitDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserThreadReaction>()
                .HasOne(utr => utr.Thread)
                .WithMany(t => t.Reactions)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<GettitUser>()
                .HasOne(u => u.ForumRole)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<GettitRole>()
                .HasOne(u => u.CreatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<GettitRole>()
                .HasOne(u => u.UpdatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<GettitRole>()
                .HasOne(u => u.DeletedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }
    }
}
