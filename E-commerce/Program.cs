using Elibri.Context;
using Elibri.Models;
using Elibri.Repositories.CartRepo;
using Elibri.Repositories.CategoryRepo;
using Elibri.Repositories.GenericRepo;
using Elibri.Repositories.OrderDetailsRepo;
using Elibri.Repositories.OrderRepo;
using Elibri.Repositories.ProductRepo;
using Elibri.Repositories.UserRepo;
using Elibri.Repositories.ReviewRepo;
using Elibri.Services.AuthServices;
using Elibri.Services.TokenServices;
using Elibri.Services.CartServices;
using Elibri.Services.CategoryServices;
using Elibri.Services.OrderDetailsServices;
using Elibri.Services.OrderServices;
using Elibri.Services.ProductServices;
using Elibri.Services.UserServices;
using Elibri.Services.ReviewServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Threading.Tasks;
using Elibri.Services.Mapper;
using Elibri.Services.EmailServices;
using Elibri.Services.ResetServices;
using Microsoft.Extensions.Options;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddDbContext<Context>();

            // Repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<IGenericRepository<Category>, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

            // Services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IResetService, ResetService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();


            // TokenService
            builder.Services.AddTransient<ITokenService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var jwtKey = configuration["jwt:Key"];
                return new TokenService(configuration);
            });

            // EmailService
            builder.Services.AddTransient<IEmailService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var frontendUrl = configuration["FrontendUrl"];
                return new EmailService(frontendUrl, configuration);
            });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();
            builder.Services.AddControllers();

            // Swagger configuration
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // AutoMapper
            builder.Services.AddAutoMapper(op =>
            {
                op.AddProfile(new MapperProfile());
            });

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            // Identity
            builder.Services.AddIdentity<User, IdentityRole>(op =>
            {
                op.SignIn.RequireConfirmedAccount = false;
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequireDigit = false;
                op.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<Context>()
            .AddDefaultTokenProviders(); 

            // Authentication
            builder.Services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(op =>
            {
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:Key"])),
                    ValidateIssuerSigningKey = true,
                    ValidAudience = builder.Configuration["jwt:audience"],
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["jwt:issuer"],
                    ValidateIssuer = true,
                };

                op.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["jwt-cookies"];
                        return Task.CompletedTask;
                    }
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
