using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Elibri.Core.Repository.CartRepo;
using Elibri.Core.Repository.CategoryRepo;
using Elibri.Core.Repository.GenericRepo;
using Elibri.Core.Repository.OrderDetailsRepo;
using Elibri.Core.Repository.OrderRepo;
using Elibri.Core.Repository.ProductRepo;
using Elibri.Core.Repository.ReviewRepo;
using Elibri.Core.Repository.UserRepo;
using Elibri.EF.Models;
using Elibri.Core.Features.Mapper;
using Elibri.Core.Features.UserServices;
using Elibri.Core.Features.CartServices;
using Elibri.Core.Features.CategoryServices;
using Elibri.Core.Features.ProductServices;
using Elibri.Core.Features.ReviewServices;
using Elibri.Core.Features.OrderServices;
using Elibri.Core.Features.OrderDetailsServices;

namespace Elibri.Core.Registration
{
    public static class CoreRegistrationExtensions
    {
        public static void AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            // AutoMapper
            services.AddAutoMapper(op =>
            {
                op.AddProfile(new MapperProfile());
            });

            // CORS
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(configuration["FrontendUrl"], "http://25.49.57.113:3000")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            // HttpContextAccessor
            services.AddHttpContextAccessor();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IGenericRepository<Category>, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();

            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IReviewService, ReviewService>();



        }
    }
}
