using JewelleryShop.DataAccess.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Utils
{
    public static class GenerateJsonWebTokenString
    {
        public static string GenerateJsonWebToken(this Employee employee, string secretKey, DateTime now)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, employee.EmployeeId),
                new Claim(ClaimTypes.Role, employee.RoleId.ToString() ?? string.Empty),
                new Claim(ClaimTypes.Name, employee.FullName ?? string.Empty),
                new Claim(ClaimTypes.Email, employee.Email ?? string.Empty),
                new Claim("UserName", employee.UserName ?? string.Empty),
                new Claim("Status", employee.Status ?? string.Empty)
            };
            var token = new JwtSecurityToken(
                claims: claims,
                expires: now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
