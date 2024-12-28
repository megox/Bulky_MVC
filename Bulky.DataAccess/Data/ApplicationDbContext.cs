using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAcess.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {//send toptions to base class DbContext



        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        //seed some rows in DataBase
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "C# Programming Basics",
                    ISBN = "9781234567890",
                    Description = "An introduction to C# programming language.",
                    Author = "John Doe",
                    Price = 29.99,
                    Price50 = 25.99,
                    Price100 = 22.99,
                    CategoryId = 2,
                    ImageUrl = ""

                },
                new Product
                {
                    Id = 2,
                    Title = "Advanced .NET",
                    ISBN = "9781234567891",
                    Description = "A deep dive into the .NET framework.",
                    Author = "Jane Smith",
                    Price = 39.99,
                    Price50 = 35.99,
                    Price100 = 30.99,
                    CategoryId = 3,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "Entity Framework Core",
                    ISBN = "9781234567892",
                    Description = "Mastering EF Core for database management.",
                    Author = "Emily Davis",
                    Price = 34.99,
                    Price50 = 30.99,
                    Price100 = 27.99,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 4,
                    Title = "ASP.NET MVC Guide",
                    ISBN = "9781234567893",
                    Description = "Comprehensive guide to building web apps with ASP.NET MVC.",
                    Author = "Michael Brown",
                    Price = 44.99,
                    Price50 = 40.99,
                    Price100 = 37.99,
                    CategoryId = 3,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 5,
                    Title = "Blazor for Beginners",
                    ISBN = "9781234567894",
                    Description = "Learn Blazor to build modern web applications.",
                    Author = "Sarah Wilson",
                    Price = 24.99,
                    Price50 = 20.99,
                    Price100 = 18.99,
                    CategoryId = 3,
                    ImageUrl = ""
                }
            );




        }

    }
}
