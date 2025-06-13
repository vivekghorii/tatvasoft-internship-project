using Mission.Entities.Context;
using Mission.Repositories.Helpers;
using Mission.Repositories.Interface;
using Mission.Repositories.IRepositories;
using Mission.Repositories;
using Mission.Services.IServices;
using Mission.Services;
using Microsoft.EntityFrameworkCore;
using Mission.Repositories.Repositories;
using Mission.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Mission.Service.Services;
using Mission.Service.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// ✅ DbContext registration (ONLY ONCE)
builder.Services.AddDbContext<MissionDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ CORS Policy
builder.Services.AddCors(cors => cors.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

// ✅ JWT Authentication Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "localhost",
        ValidAudience = "localhost",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"]!)
        ),
        ClockSkew = TimeSpan.Zero
    };
});

// ✅ Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Service & Repository DI Registration
builder.Services.AddScoped<JwtService>();

builder.Services.AddScoped<IAdminUserService, AdminUserService>();
builder.Services.AddScoped<IAdminUserRepository, AdminUserRepository>();

builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IMissionThemeRepository, MissionThemeRepository>();
builder.Services.AddScoped<IMissionThemeService, MissionThemeService>();

builder.Services.AddScoped<IMissionSkillService, MissionSkillService>();
builder.Services.AddScoped<IMissionSkillRepository, MissionSkillRepository>();

builder.Services.AddScoped<ICommonService, CommonService>();
builder.Services.AddScoped<ICommonRepository, CommonRepository>();

builder.Services.AddScoped<IMissionRepository, MissionRepository>();
builder.Services.AddScoped<IMissionService, MissionService>();

// ✅ No duplicate service registration
// (You had AddDbContext & AddScoped<ILoginService, LoginService>() twice — removed extra)

var app = builder.Build();

// ✅ Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyPolicy");

app.UseAuthentication(); // 🔐 Important
app.UseAuthorization();

app.MapControllers();

app.Run();
