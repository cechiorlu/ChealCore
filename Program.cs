using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ChealCore.Data;
using ChealCore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration["ConnectionStrings:ChealCore"];
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

//  check db for default user/roles. Else, seed required user/roles.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        //await ContextSeed.SeedRolesAsync(userManager, roleManager);
        //await ContextSeed.SeedSuperAdminAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//// Add services to the container.



//// Email Service config
//var emailConfig = builder.Configuration
//        .GetSection("EmailConfiguration")
//        .Get<EmailConfiguration>();
//builder.Services.AddSingleton(emailConfig);
//builder.Services.AddScoped<IEmailSender, EmailSender>();


//var app = builder.Build();

////  check db for default user/roles. Else, seed required user/roles.
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
//    try
//    {
//        var context = services.GetRequiredService<ApplicationDbContext>();
//        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
//        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
//        await ContextSeed.SeedRolesAsync(userManager, roleManager);
//        await ContextSeed.SeedSuperAdminAsync(userManager, roleManager);
//    }
//    catch (Exception ex)
//    {
//        var logger = loggerFactory.CreateLogger<Program>();
//        logger.LogError(ex, "An error occurred seeding the DB.");
//    }
//}

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseMigrationsEndPoint();
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapRazorPages();

//app.Run();