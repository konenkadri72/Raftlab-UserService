using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

namespace AcademyManagement.Web
{
    public static class ServiceCollections
    {
        /// <summary>
        /// this method will Add Response Compression to Project
        /// </summary>
        /// <param name="services"></param>
        public static void Configure_Response_Compression(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Fastest);
            builder.Services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = new[]
                {
                    "text/plain",
                    "text/css",
                    "application/javascript",
                    "text/html",
                    "application/xml",
                    "text/xml",
                    "application/json",
                    "text/json",
                    "image/svg+xml",
                    "image/png",
                    "image/jpg"
                };
            });
        }

        /// <summary>
        /// this method will configure Swagger UI on Project
        /// </summary>
        /// <param name="services"></param>
        public static void Configure_Swagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v1", new OpenApiInfo
                {
                    Version = $"v1",
                    Title = "Architecture",
                    Description = "MVC Architecture API",
                    License = new OpenApiLicense { Name = "Use under Konen Kadri." }
                });
            });
        }
        /// <summary>
        /// this method will Add CORS Policy To Project
        /// </summary>
        /// <param name="services"></param>
        public static void Configure_CORS_Policy(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options => options.AddPolicy("Cors", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

    }
}
