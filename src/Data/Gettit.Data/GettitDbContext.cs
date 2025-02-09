using Gettit.Data.Extensions;
using Gettit.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gettit.Web.Data
{
    public class GettitDbContext : IdentityDbContext<GettitUser>
    {
        public DbSet<Attachment> Attachments { get; set; }

        public DbSet<GettitCommunity> Communities { get; set; }

        public DbSet<GettitThread> Threads { get; set; }

        public DbSet<GettitThread> Tags { get; set; }

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

            builder.ConfigureMetadataEntity<GettitRole>();
            builder.ConfigureMetadataEntity<GettitTag>();

            builder.Entity<GettitCommunity>()
                .HasOne(gc => gc.ThumbnailPhoto)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<GettitCommunity>()
                .HasOne(gc => gc.BannerPhoto)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<GettitCommunity>()
                .HasMany(gc => gc.Tags)
                .WithMany();

            builder.Entity<GettitThread>()
                .HasMany(gt => gt.Tags)
                .WithMany();

            builder.Entity<Comment>()
                .HasMany(c => c.Attachments)
                .WithMany();

            builder.Entity<Comment>()
                .HasMany(c => c.Replies)
                .WithOne(c => c.Parent)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<GettitCommunity>()
                .HasOne(gc => gc.ThumbnailPhoto);

            builder.Entity<GettitCommunity>()
                .HasOne(gc => gc.BannerPhoto);

            builder.Entity<GettitThread>()
                .HasMany(gt => gt.Attachments)
                .WithMany();


            base.OnModelCreating(builder);
        }
    }
}
