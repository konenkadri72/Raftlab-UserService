using Microsoft.AspNetCore.Diagnostics;
using Raftlab.GlobleService.ProjectRegister;
using Raftlab.ReqResPlugin.ProjectRegistration;
using Raftlab.Service.ProjectRegister;
using System.Net;

namespace AcademyManagement.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            InitServices(builder);

            var app = builder.Build();

            InitApplication(app);

            app.Run();  // ✅ Run the application here
        }

        private static void InitServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            builder.Configure_Response_Compression();

            builder.Services.RegisterServices();
            builder.Services.RegisterGlobleService();
            builder.Services.RegisterReqResServices();

            builder.Configure_CORS_Policy();
            builder.Configure_Swagger();

        }

        private static void InitApplication(WebApplication application)
        {
            application.UseCors("Cors");

            if (application.Environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }

            // ✅ Ensure Swagger is accessible even in non-dev environments
            application.UseSwagger();
            application.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AcademyManagement.Web v1");
            });

            application.UseHttpsRedirection();
            application.UseDefaultFiles();
            application.UseStaticFiles();
            application.UseRouting();
            application.UseAuthentication();
            application.UseAuthorization();

            // ✅ Corrected for .NET 8 (UseEndpoints is removed)
            application.MapControllers();

            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                    }
                });
            });
        }
    }
}
