using JWT_Authentication_ASP.NET_Core_Web_API.Helpers;
using JWT_Authentication_ASP.NET_Core_Web_API.Middleware;
using JWT_Authentication_ASP.NET_Core_Web_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JWT_Authentication_ASP.NET_Core_Web_API
{
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

            // Lets add cors
            builder.Services.AddCors();

            // configure strongly typed settings object
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

            // configure dependency Injection for app services
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            // configure JWT Bearer authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var key = Encoding.ASCII.GetBytes(builder.Configuration["AppSettings:Secret"]);
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key), // Replace with your security key
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseHttpsRedirection();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>(); // <- this is custom auth middleware, it should be placed before UseAuthorization() middleware
            app.UseAuthorization();
            
            

            app.MapControllers();

            app.Run();
        }
    }
}