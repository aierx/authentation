using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using webapi.Middleware;
using webapi.provider;

namespace webapi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("aaa", builder =>
            {
                builder.WithOrigins("http://aierx.com:5173", "http://192.168.3.169:5173", "http://localhost:5173")
                    .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                ;
            });
        });
        // builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
        // builder.Services.AddSingleton<IAuthorizationPolicyProvider, MinimumAgePolicyProvider>();


        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "cookie"; // 设置Cookie名称
                options.ExpireTimeSpan = TimeSpan.FromMinutes(120); // 有效期1小时
                options.Cookie.MaxAge = TimeSpan.FromMinutes(24 * 60); // 有效期1小时
                // options.Cookie.Domain = "127.0.0.1"; // 设置Cookie域名
                options.Cookie.HttpOnly = true;
                // options.Cookie.SameSite = SameSiteMode.None;
                // options.Cookie.SecurePolicy = CookieSecurePolicy.None;
            });

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql("Server=localhost;Database=webapi;Uid=root;Pwd=123456;",
                MySqlServerVersion.LatestSupportedServerVersion);
        });


        builder.Services.AddScoped<MyAuthorizationMiddleware>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<MyAuthorizationMiddleware>();

        app.MapControllers();

        app.Run();
    }
}