using BackendAPI.Application.DependencyInjection;
using BackendAPI.Persistence.Identity;
using BackendAPI.Persistence.Data;
using BackendAPI.Persistence.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("UserDbContextConnection") ?? throw new InvalidOperationException("Connection string 'UserDbContextConnection' not found.");
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddApplication();
builder.Services.AddPersistence(connectionString);

builder.Services.AddDefaultIdentity<UserIdentity>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(_ => { }, AppDomain.CurrentDomain.GetAssemblies());
//add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins(
                                      "http://localhost:3000",
                                      "https://localhost:7179",
                                      "http://localhost:5279")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod();
                          });
    //options.AddPolicy("Admin", authBuilder => { authBuilder.RequireRole("Admin"); });
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(o =>
{
    var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured.");
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        NameClaimType = ClaimTypes.NameIdentifier,
        RoleClaimType = ClaimTypes.Role
    };
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;
    // Default SignIn settings.
    options.SignIn.RequireConfirmedEmail = false;
    options.User.RequireUniqueEmail = true;
});
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

await SeedAdminUserAsync(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
/*app.UseStaticFiles();*/
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
    RequestPath = new PathString("/wwwroot")
});

app.UseAuthorization();
app.MapControllers();

app.Run();

async Task SeedAdminUserAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserIdentity>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    if (await userManager.Users.AnyAsync())
    {
        return;
    }

    const string adminRole = "Admin";
    const string adminEmail = "admin@gmail.com";
    const string adminPassword = "Admin@123";

    if (!await roleManager.RoleExistsAsync(adminRole))
    {
        await roleManager.CreateAsync(new IdentityRole(adminRole));
    }

    var adminUser = new UserIdentity
    {
        UserName = adminEmail,
        Email = adminEmail,
        EmailConfirmed = true
    };

    var createResult = await userManager.CreateAsync(adminUser, adminPassword);
    if (!createResult.Succeeded)
    {
        throw new InvalidOperationException($"Failed to seed admin user: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
    }

    var roleResult = await userManager.AddToRoleAsync(adminUser, adminRole);
    if (!roleResult.Succeeded)
    {
        throw new InvalidOperationException($"Failed to assign admin role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
    }
}
