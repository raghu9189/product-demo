using Microsoft.EntityFrameworkCore;
using ProductDemo.Middleware;
using ProductDemo.Models.Entities;
using ProductDemo.Repositories;
using ProductDemo.Repositories.Interfaces;
using ProductDemo.Services;
using ProductDemo.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddOpenApi();

// swagger setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB Connect
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString =
        builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    );
});


// Dependency Injection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// DB Seeding if --seed flag is passed
if (args.Contains("--seed"))
{
    Console.WriteLine("=== Database Seeding Process ===");
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // 1. Check existing counts
        int initialUsersCount = db.Users.Count();
        int initialProductsCount = db.Products.Count();
        int initialOrdersCount = db.Orders.Count();

        Console.WriteLine($"[Existing Data Check]");
        Console.WriteLine($"Users count: {initialUsersCount}");
        Console.WriteLine($"Products count: {initialProductsCount}");
        Console.WriteLine($"Orders count: {initialOrdersCount}");

        // Seed Users if count is less than 10
        if (initialUsersCount < 10)
        {
            Console.WriteLine("Users count is less than 10. Seeding users...");
            var userNames = new[] {
                "Alice Smith", "Bob Jones", "Charlie Brown", "Diana Prince", "Ethan Hunt",
                "Fiona Gallagher", "George Clark", "Hannah Abbott", "Ian Malcolm", "Julia Roberts",
                "Kevin Bacon", "Laura Croft"
            };

            foreach (var name in userNames)
            {
                if (!db.Users.Any(u => u.Name == name))
                {
                    db.Users.Add(new User { Name = name });
                }
            }
            db.SaveChanges();
            Console.WriteLine($"Users seeded. Total users now: {db.Users.Count()}");
        }

        // Seed Products if count is less than 15
        if (initialProductsCount < 15)
        {
            Console.WriteLine("Products count is less than 15. Seeding products...");
            var sampleProducts = new List<Product>
            {
                new Product { Title = "Laptop Pro 15", Description = "High performance laptop with 16GB RAM, 512GB SSD", Price = 1299.99 },
                new Product { Title = "Smart Phone X", Description = "5G enabled smartphone with OLED display", Price = 799.99 },
                new Product { Title = "Wireless Headphones", Description = "Active noise cancelling over-ear headphones", Price = 199.99 },
                new Product { Title = "Mechanical Keyboard", Description = "RGB backlit mechanical keyboard with blue switches", Price = 89.99 },
                new Product { Title = "Wireless Mouse", Description = "Ergonomic wireless optical mouse", Price = 49.99 },
                new Product { Title = "UltraWide Monitor 34", Description = "Curved IPS ultrawide monitor for gaming and productivity", Price = 449.99 },
                new Product { Title = "Fitness Smartwatch", Description = "Waterproof smartwatch with heart rate and GPS tracking", Price = 149.99 },
                new Product { Title = "Bluetooth Speaker", Description = "Portable outdoor waterproof bluetooth speaker", Price = 69.99 },
                new Product { Title = "Ergonomic Desk Chair", Description = "Adjustable mesh office chair with lumbar support", Price = 249.99 },
                new Product { Title = "Coffee Maker", Description = "12-cup programmable drip coffee maker", Price = 79.99 },
                new Product { Title = "Backpack", Description = "Durable travel laptop backpack with USB charging port", Price = 39.99 },
                new Product { Title = "Electric Kettle", Description = "1.7L stainless steel fast boiling kettle", Price = 29.99 },
                new Product { Title = "Desk Lamp", Description = "LED desk lamp with USB charging port and 5 brightness levels", Price = 24.99 },
                new Product { Title = "External SSD 1TB", Description = "High-speed portable solid state drive", Price = 119.99 },
                new Product { Title = "Power Bank 20000mAh", Description = "High capacity portable charger with fast charging", Price = 34.99 }
            };

            foreach (var p in sampleProducts)
            {
                if (!db.Products.Any(prod => prod.Title == p.Title))
                {
                    db.Products.Add(p);
                }
            }
            db.SaveChanges();
            Console.WriteLine($"Products seeded. Total products now: {db.Products.Count()}");
        }

        // Seed Orders if count is less than 100
        int currentOrdersCount = db.Orders.Count();
        if (currentOrdersCount < 100)
        {
            var allUsers = db.Users.ToList();
            var allProducts = db.Products.ToList();

            if (allUsers.Any() && allProducts.Any())
            {
                var rand = new Random();
                int targetCount = 110;
                int ordersNeeded = targetCount - currentOrdersCount;
                Console.WriteLine($"Orders count ({currentOrdersCount}) is less than 100. Seeding {ordersNeeded} more orders to reach {targetCount}...");

                for (int i = 0; i < ordersNeeded; i++)
                {
                    var randomUser = allUsers[rand.Next(allUsers.Count)];
                    var randomProduct = allProducts[rand.Next(allProducts.Count)];
                    var quantity = rand.Next(1, 5); // 1 to 4 quantity

                    db.Orders.Add(new Order
                    {
                        UserId = randomUser.Id,
                        ProductId = randomProduct.Id,
                        Quantity = quantity
                    });
                }
                db.SaveChanges();
                Console.WriteLine($"Orders seeded successfully! Total orders now: {db.Orders.Count()}");
            }
            else
            {
                Console.WriteLine("Cannot seed orders because users or products tables are empty!");
            }
        }
        else
        {
            Console.WriteLine($"Orders count is already {currentOrdersCount}, which is 100+. No order seeding needed.");
        }

        Console.WriteLine("=== Database Seeding Completed Successfully ===");
    }
    return;
}

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Add Middlewares here
app.UseMiddleware<LoggerMiddleware>();

app.MapControllers();

app.Run();