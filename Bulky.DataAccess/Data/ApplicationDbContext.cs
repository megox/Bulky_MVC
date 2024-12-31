using Bulky.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAcess.Data
{
    public class ApplicationDbContext :IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {//send options to base class DbContext
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }   
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        //Seed some rows in DataBase
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

            modelBuilder.Entity<Company>().HasData(
                 new Company
                 {
                     id = 1,
                     name = "TechCorp Solutions",
                     StreetAddress = "123 Main St",
                     City = "Tech City",
                     State = "CA",
                     PostalCode = "90001",
                     PhoneNumber = "123-456-7890"
                 },
                 new Company
                 {
                     id = 2,
                     name = "Innovatech",
                     StreetAddress = "456 Elm St",
                     City = "Innovation Town",
                     State = "TX",
                     PostalCode = "73301",
                     PhoneNumber = "987-654-3210"
                 },
                 new Company
                 {
                     id = 3,
                     name = "GreenWave Inc",
                     StreetAddress = "789 Pine St",
                     City = "Greenfield",
                     State = "WA",
                     PostalCode = "98001",
                     PhoneNumber = "123-123-1234"
                 },
                 new Company
                 {
                     id = 4,
                     name = "BlueSky Tech",
                     StreetAddress = "101 Oak St",
                     City = "Skyline",
                     State = "NY",
                     PostalCode = "10001",
                     PhoneNumber = "456-456-4567"
                 },
                 new Company
                 {
                     id = 5,
                     name = "NextGen Solutions",
                     StreetAddress = "202 Maple Ave",
                     City = "Future City",
                     State = "IL",
                     PostalCode = "60601",
                     PhoneNumber = "789-789-7890"
                 },
                 new Company
                 {
                     id = 6,
                     name = "EcoTech Innovations",
                     StreetAddress = "303 Birch Ln",
                     City = "EcoVille",
                     State = "OR",
                     PostalCode = "97001",
                     PhoneNumber = "321-321-3210"
                 },
                 new Company
                 {
                     id = 7,
                     name = "CloudSync Ltd",
                     StreetAddress = "404 Cedar Dr",
                     City = "Cloud Town",
                     State = "FL",
                     PostalCode = "33101",
                     PhoneNumber = "654-654-6543"
                 },
                 new Company
                 {
                     id = 8,
                     name = "AlphaNet Corp",
                     StreetAddress = "505 Spruce St",
                     City = "Alpha City",
                     State = "GA",
                     PostalCode = "30301",
                     PhoneNumber = "987-987-9876"
                 },
                 new Company
                 {
                     id = 9,
                     name = "SmartGrid Systems",
                     StreetAddress = "606 Willow Way",
                     City = "Smartville",
                     State = "PA",
                     PostalCode = "19101",
                     PhoneNumber = "111-222-3333"
                 },
                 new Company
                 {
                     id = 10,
                     name = "Pioneer Tech",
                     StreetAddress = "707 Aspen Rd",
                     City = "Pioneer Town",
                     State = "OH",
                     PostalCode = "43001",
                     PhoneNumber = "444-555-6666"
                 }
 );


        }

    }
}
