using Elibri.Core.Registration;
using Elibri.EF.Registration;
using Elibri.Authorization.Registration;
using Elibri.Api.Registration;

namespace Elibri.Hosting
{
    /// <summary>
    /// ������� ����� ����������.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ����� ����� � ����������.
        /// </summary>
        /// <param name="args">��������� ��������� ������.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Load configuration
            IConfiguration configuration = builder.Configuration;

            // Add services to the container
            builder.Services.AddEFServices(configuration);
            builder.Services.AddCoreServices(configuration);
            builder.Services.AddAuthServices(configuration);
            builder.Services.AddApiServices(configuration);

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3001")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable Swagger for Development environment or for Swagger profile
                if (app.Environment.EnvironmentName == "Development" || app.Environment.EnvironmentName == "swagger")
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
            }

            app.UseCors("AllowSpecificOrigin");

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
