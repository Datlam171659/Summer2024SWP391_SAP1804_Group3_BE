using JewelleryShop.Business.Service;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess;
using JewelleryShop.DataAccess.Repository.Interface;
using JewelleryShop.DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using JewelleryShop.API.Middlewares;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace JewelleryShop.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddScopedService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IWarrantyRepository, WarrantyRepository>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IWarrantyService, WarrantyService>();
            return services;
        }
        public static IServiceCollection AddWebAPIService(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(
                options =>
                {
                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Description = "Standard Authorization header using the Bearer scheme (\"{token}\")",
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT"
                    });
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Jewellery API", Version = "v1" });
                    options.OperationFilter<SecurityRequirementsOperationFilter>();
                }
            );

            services.AddHttpContextAccessor();
            services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<GlobalExceptionMiddleware>();
            services.AddScopedService();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpContextAccessor();

            return services;
        }

        public static WebApplication AddWebApplicationMiddleware(this WebApplication webApplication)
        {
            webApplication.UseMiddleware<GlobalExceptionMiddleware>();

            return webApplication;
        }
    }
}
