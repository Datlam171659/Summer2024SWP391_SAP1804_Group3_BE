using JewelleryShop.API;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.Business.Service;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Repository.Interface;
using JewelleryShop.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using JewelleryShop.API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
using System;

namespace JewelleryShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddWebAPIService();
            builder.Services.AddDbContext<JewelleryDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DB"))
                .LogTo(Console.WriteLine, LogLevel.Information);
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            var secretKey = builder.Configuration["Jwt:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                        ClockSkew = TimeSpan.Zero
                    };

                    opt.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();

                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";
                            var response = new APIResponse<string>
                            {
                                Message = "Unauthorized",
                                Errors = { "Authentication token is missing or invalid." }
                            };
                            var jsonResponse = JsonSerializer.Serialize(response);
                            return context.Response.WriteAsync(jsonResponse);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            context.Response.ContentType = "application/json";
                            var response = new APIResponse<string>
                            {
                                Message = "Forbidden",
                                Errors = { "You do not have permission to access this resource." }
                            };
                            var jsonResponse = JsonSerializer.Serialize(response);
                            return context.Response.WriteAsync(jsonResponse);
                        }
                    };
                });

            var app = builder.Build();
            app.AddWebApplicationMiddleware();
            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<AuthorizationMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
