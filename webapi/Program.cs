using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webapi.authentication;
using webapi.Middleware;

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
        builder.Services.AddSwaggerGen(e => { e.CustomSchemaIds(type => type.FullName); });

        // builder.Services.AddCors(options =>
        // {
        //     options.AddPolicy("aaa", builder =>
        //     {
        //         builder.WithOrigins("http://127.0.0.1", "http://localhost")
        //             .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        //         ;
        //     });
        // });
        // builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
        // builder.Services.AddSingleton<IAuthorizationPolicyProvider, MinimumAgePolicyProvider>();
        // This method gets called by the runtime. Use this method to add services to the container.
        builder.Services.AddAuthentication(
            option =>
            {
                option.DefaultScheme = "auth";
                option.AddScheme<MyAuthenticationHandler<AuthenticationProperties>>("auth", "auth");
            });


        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        }).AddCookie(options =>
        {
            options.ForwardForbid = "auth";
            options.ForwardChallenge = "auth";
        });

        builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

        // builder.Services.AddSession();

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql("Server=localhost;Database=webapi;Uid=root;Pwd=13638437650.lL;",
                new MySqlServerVersion("5.7.44"));
        });
        // builder.Services.AddScoped<MyAuthorizationMiddleware>();

        var app = builder.Build();
        // Configure the HTTP request pipeline.
        app.UsePathBase(new PathString("/api"));
        app.UseRouting();
        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI();

        // app.UseHttpsRedirection();

        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        // app.UseMiddleware<MyAuthorizationMiddleware>();
        app.MapControllers();

        app.Run();
    }
}

public class MyHandler : IAuthenticationHandler, IAuthenticationSignInHandler, IAuthenticationSignOutHandler
{
    public AuthenticationScheme Scheme { get; private set; }
    protected HttpContext Context { get; private set; }

    public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
    {
        Scheme = scheme;
        Context = context;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 认证
    /// </summary>
    /// <returns></returns>
    public async Task<AuthenticateResult> AuthenticateAsync()
    {
        var cookie = Context.Request.Cookies["myCookie"];
        if (string.IsNullOrEmpty(cookie))
        {
            return AuthenticateResult.NoResult();
        }

        return AuthenticateResult.Success(this.Deserialize(cookie));
    }

    /// <summary>
    /// 没有登录 要求 登录 
    /// </summary>
    /// <param name="properties"></param>
    /// <returns></returns>
    public Task ChallengeAsync(AuthenticationProperties properties)
    {
        Context.Response.StatusCode = 200;
        Context.Response.WriteAsJsonAsync(Results.Ok("请登入"));
        return Task.CompletedTask;
    }

    /// <summary>
    /// 没权限
    /// </summary>
    /// <param name="properties"></param>
    /// <returns></returns>
    public Task ForbidAsync(AuthenticationProperties properties)
    {
        Context.Response.StatusCode = 200;
        Context.Response.WriteAsJsonAsync(Results.Ok("没有权限"));
        return Task.CompletedTask;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="user"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    public Task SignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
    {
        var ticket = new AuthenticationTicket(user, properties, Scheme.Name);
        Context.Response.Cookies.Append("myCookie", this.Serialize(ticket));
        return Task.CompletedTask;
    }

    /// <summary>
    /// 退出
    /// </summary>
    /// <param name="properties"></param>
    /// <returns></returns>
    public Task SignOutAsync(AuthenticationProperties properties)
    {
        Context.Response.Cookies.Delete("myCookie");
        return Task.CompletedTask;
    }

    private AuthenticationTicket Deserialize(string content)
    {
        byte[] byteTicket = System.Text.Encoding.Default.GetBytes(content);
        return TicketSerializer.Default.Deserialize(byteTicket);
    }

    private string Serialize(AuthenticationTicket ticket)
    {
        //需要引入  Microsoft.AspNetCore.Authentication

        byte[] byteTicket = TicketSerializer.Default.Serialize(ticket);
        return Encoding.Default.GetString(byteTicket);
    }
}