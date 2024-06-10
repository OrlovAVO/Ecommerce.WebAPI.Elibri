using Elibri.Core.Registration;
using Elibri.EF.Registration;
using Elibri.Authorization.Registration;
using Elibri.Api.Registration;
using Microsoft.OpenApi.Models;

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

            // �������� ������������
            IConfiguration configuration = builder.Configuration;

            // ���������� �������� � ���������
            builder.Services.AddEFServices(configuration);
            builder.Services.AddCoreServices(configuration);
            builder.Services.AddAuthServices(configuration);
            builder.Services.AddApiServices(configuration);

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
            });

            // ���������� �������� CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://25.32.59.44:3000", "http://localhost:3000")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            // ��������� ��������� HTTP-��������
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // ��������� Swagger ��� ����� ���������� ��� ��� ������� Swagger
                if (app.Environment.EnvironmentName == "Development" || app.Environment.EnvironmentName == "swagger")
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Elibri API V1");
                    });
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
