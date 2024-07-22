using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
using JewelleryShop.DataAccess.Models.ViewModel.StaffViewModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace JewelleryShop.API.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var staffService = scope.ServiceProvider.GetRequiredService<IStaffService>();

                    var isAuthenticated = IsAuthorized(context);

                    if (isAuthenticated)
                    {
                        var user = await GetUser(context, staffService);
                        if (user == null) throw new Exception("User does not exist.");
                        if (!IsUserActive(user)) throw new Exception("Account is disabled. Please contact the administrator for further support.");
                    }

                }
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            var result = JsonSerializer.Serialize(new APIResponse<string>{ Message = "Unauthorized", Errors = new List<string> { exception.Message } });
            await context.Response.WriteAsync(result);
        }

        private bool IsAuthorized(HttpContext context)
        {
            return context.Request.Headers.ContainsKey("Authorization") &&
              context.Request.Headers["Authorization"].ToString().StartsWith("Bearer ");
        }

        private string GetToken(HttpContext context)
        {
            return context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        }

        private async Task<StaffCommonDTO?> GetUser(HttpContext context, IStaffService staffService)
        {
            if (IsAuthorized(context))
            {
                var token = GetToken(context);

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken == null)
                {
                    throw new ArgumentException("Invalid JWT token");
                }

                var ID = jwtToken?.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
                if (ID == null) return null;

                var user = await staffService.GetStaffById(ID);
                return user;
            }

            return null;
        }
        private bool IsUserActive(StaffCommonDTO staffData)
        {
            return staffData.Status.ToLower() == "active";
        }
    }
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationMiddleware>();
        }
    }
}
