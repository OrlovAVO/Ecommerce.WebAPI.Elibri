using Elibri.Context;
using Elibri.Models;
using Elibri.Repositories.CartRepo;
using Elibri.Repositories.CategoryRepo;
using Elibri.Repositories.GenericRepo;
using Elibri.Repositories.OrderDetailsRepo;
using Elibri.Repositories.OrderRepo;
using Elibri.Repositories.ProductRepo;
using Elibri.Repositories.ReviewRepo;
using Elibri.Repositories.UserRepo;
using Elibri.Services.AuthServices;
using Elibri.Services.CartServices;
using Elibri.Services.CategoryServices;
using Elibri.Services.EmailServices;
using Elibri.Services.Mapper;
using Elibri.Services.OrderDetailsServices;
using Elibri.Services.OrderServices;
using Elibri.Services.ProductServices;
using Elibri.Services.ResetServices;
using Elibri.Services.ReviewServices;
using Elibri.Services.TokenServices;
using Elibri.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Load configuration
            IConfiguration configuration = builder.Configuration;

            // Add services to the container
            builder.Services.AddDbContext<Context>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

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

                // Include XML comments
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
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
                    builder.WithOrigins(configuration["FrontendUrl"], "http://25.49.57.113:3000")
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:Key"])),
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration["jwt:audience"],
                    ValidateAudience = true,
                    ValidIssuer = configuration["jwt:issuer"],
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

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; connect-src 'self' http://25.49.57.113:3000");
                await next();
            });



            app.UseCors();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
