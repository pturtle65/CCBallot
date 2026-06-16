using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ccballot.Data;
using ccballot.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var storageMode = builder.Configuration.GetValue<string>("StorageMode") ?? "LocalStorage";

if (storageMode == "SqlServer")
{
    builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>();

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    });

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<IDbStorageService, SSStorageService>();
}
else
{
    builder.Services.AddScoped<IDbStorageService, LocalStorageService>();
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

if (storageMode == "SqlServer")
{
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages();
}

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

if (storageMode == "SqlServer")
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        if (!await roleManager.RoleExistsAsync("Clerk"))
            await roleManager.CreateAsync(new IdentityRole("Clerk"));

        var adminEmail = builder.Configuration.GetValue<string>("Admin:Email") ?? "admin@ccballot.com";
        var adminPassword = builder.Configuration.GetValue<string>("Admin:Password") ?? "Admin123!";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

app.Run();
