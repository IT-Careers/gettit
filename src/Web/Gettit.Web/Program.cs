using Gettit.Data.Models;
using Gettit.Data.Repositories;
using Gettit.Service.Cloud;
using Gettit.Service.Comment;
using Gettit.Service.Community;
using Gettit.Service.Reaction;
using Gettit.Service.Tag;
using Gettit.Service.Thread;
using Gettit.Service.User;
using Gettit.Web.Data;
using Gettit.Web.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<GettitDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Application Repositories
        builder.Services.AddTransient<GettitCommunityRepository>();
        builder.Services.AddTransient<GettitTagRepository>();
        builder.Services.AddTransient<GettitThreadRepository>();
        builder.Services.AddTransient<CommentRepository>();
        builder.Services.AddTransient<ReactionRepository>();
        builder.Services.AddTransient<UserThreadReactionRepository>();
        builder.Services.AddTransient<UserCommentReactionRepository>();

        // Application Services
        builder.Services.AddTransient<IGettitCommunityService, GettitCommunityService>();
        builder.Services.AddTransient<IGettitTagService, GettitTagService>();
        builder.Services.AddTransient<IReactionService, ReactionService>();
        builder.Services.AddTransient<IGettitThreadService, GettitThreadService>();
        builder.Services.AddTransient<ICommentService, CommentService>();
        builder.Services.AddTransient<IUserContextService, UserContextService>();
        builder.Services.AddTransient<ICloudinaryService, CloudinaryService>();

        builder.Services
            .AddDefaultIdentity<GettitUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<GettitDbContext>();

        builder.Services.AddControllersWithViews();

        builder.Services.AddRouting();
    }

    public static void ConfigureApp(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseDatabaseSeed();

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllerRoute(
            name: "Administration",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages()
           .WithStaticAssets();
    }

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder);
        
        var app = builder.Build();
        ConfigureApp(app);

        app.Run();
    }
}