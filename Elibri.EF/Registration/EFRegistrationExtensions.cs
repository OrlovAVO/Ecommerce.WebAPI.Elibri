using Elibri.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Elibri.EF.Registration
{
    public static class EFRegistrationExtensions
    {
        // Расширение для добавления служб Entity Framework.
        public static void AddEFServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Добавление контекста базы данных с использованием указанной строки подключения и сборки миграции.
            services.AddDbContext<Context>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Elibri.EF")));
        }
    }
}
