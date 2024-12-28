using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "ISBN", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "John Doe", "An introduction to C# programming language.", "9781234567890", 29.989999999999998, 22.989999999999998, 25.989999999999998, "C# Programming Basics" },
                    { 2, "Jane Smith", "A deep dive into the .NET framework.", "9781234567891", 39.990000000000002, 30.989999999999998, 35.990000000000002, "Advanced .NET" },
                    { 3, "Emily Davis", "Mastering EF Core for database management.", "9781234567892", 34.990000000000002, 27.989999999999998, 30.989999999999998, "Entity Framework Core" },
                    { 4, "Michael Brown", "Comprehensive guide to building web apps with ASP.NET MVC.", "9781234567893", 44.990000000000002, 37.990000000000002, 40.990000000000002, "ASP.NET MVC Guide" },
                    { 5, "Sarah Wilson", "Learn Blazor to build modern web applications.", "9781234567894", 24.989999999999998, 18.989999999999998, 20.989999999999998, "Blazor for Beginners" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
