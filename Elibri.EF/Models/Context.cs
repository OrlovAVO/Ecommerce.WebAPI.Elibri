using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Elibri.EF.Models
{
    /// <summary>
    /// Контекст базы данных приложения.
    /// </summary>
    public class Context : IdentityDbContext<User>
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Создает новый экземпляр контекста базы данных.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        public Context(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Набор данных для продуктов.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Набор данных для категорий.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Набор данных для заказов.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Набор данных для деталей заказов.
        /// </summary>
        public DbSet<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Набор данных для корзин.
        /// </summary>
        public DbSet<Cart> Carts { get; set; }

        /// <summary>
        /// Набор данных для элементов корзины.
        /// </summary>
        public DbSet<CartItem> CartItems { get; set; }

        /// <summary>
        /// Набор данных для отзывов.
        /// </summary>
        public DbSet<Review> Reviews { get; set; }

        /// <summary>
        /// Конфигурация контекста базы данных.
        /// </summary>
        /// <param name="optionsBuilder">Построитель опций контекста.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly("Elibri.EF"));
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Создание модели базы данных.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .IsRequired();

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithOne(u => u.Cart)
                .HasForeignKey<Cart>(c => c.UserId)
                .IsRequired(false);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasColumnType("varchar(450)");
        }
    }
}
