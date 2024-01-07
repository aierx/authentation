using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

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
                builder.WithOrigins(new[] {"http://127.0.0.1:5101","http://127.0.0.1:5173","http://localhost:5173"}).AllowAnyHeader().AllowAnyMethod().AllowCredentials();;
            });
        });

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "cookie"; // 设置Cookie名称
                // options.Cookie.Expiration  = new TimeSpan(1, 0, 0); // 有效期1小时
                // options.Cookie.Domain = ".91suke.com"; // 设置Cookie域名
            });

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql("Server=localhost;Database=webapi;Uid=root;Pwd=123456;",
                MySqlServerVersion.LatestSupportedServerVersion);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}