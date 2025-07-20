using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToyShopWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedToyData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Toys",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Fast and fun", "/images/rc_car.jpg", "Remote Control Car", 59.99m },
                    { 2, "Colorful and creative", "/images/blocks.jpg", "Building Blocks", 39.99m },
                    { 3, "Soft and cuddly", "/images/teddy.jpg", "Teddy Bear", 24.99m },
                    { 4, "Realistic toy dino", "/images/dino.jpg", "Dinosaur Figure", 19.99m },
                    { 5, "Brain teaser puzzle", "/images/cube.jpg", "Rubik's Cube", 14.99m },
                    { 6, "Complete railway set", "/images/train.jpg", "Toy Train Set", 49.99m },
                    { 7, "Small piano for kids", "/images/keyboard.jpg", "Musical Keyboard", 69.99m },
                    { 8, "Educational shape puzzle", "/images/puzzle.jpg", "Puzzle Board", 19.99m },
                    { 9, "Army action figures", "/images/soldiers.jpg", "Toy Soldier Set", 29.99m },
                    { 10, "Indoor mini hoop", "/images/hoop.jpg", "Basketball Hoop", 34.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
