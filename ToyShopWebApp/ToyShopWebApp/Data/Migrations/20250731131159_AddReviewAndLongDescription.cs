using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToyShopWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewAndLongDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "Toys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ToyID = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Toys_ToyID",
                        column: x => x.ToyID,
                        principalTable: "Toys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 1,
                column: "LongDescription",
                value: "This remote control car offers precise steering and powerful acceleration. Built with a durable body, it's perfect for outdoor racing and high-speed adventures.");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 2,
                column: "LongDescription",
                value: "A vibrant set of building blocks designed to spark creativity and improve spatial awareness. Perfect for constructing imaginative worlds and fun learning experiences.");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 3,
                column: "LongDescription",
                value: "This adorable teddy bear features ultra-soft fur and a huggable design. An ideal bedtime companion to provide comfort and security for young children.");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 4,
                column: "LongDescription",
                value: "A lifelike dinosaur figure with detailed textures and vivid colors. Great for prehistoric play, educational storytelling, and dino-loving kids.");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 5,
                column: "LongDescription",
                value: "The classic 3x3 Rubik's Cube offers endless puzzle-solving fun. A timeless toy that sharpens logic, concentration, and memory skills.");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 6,
                column: "LongDescription",
                value: "This complete toy train set includes engines, carriages, and tracks. Kids can build their own railway and enjoy hours of imaginative play.");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 7,
                column: "LongDescription",
                value: "A kid-friendly electronic keyboard with multiple tones and rhythms. Helps children develop a love for music and rhythm while having fun.");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 8,
                column: "LongDescription",
                value: "A wooden puzzle board with various shapes and colors. Ideal for early learners to enhance shape recognition, coordination, and problem-solving skills.");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 9,
                column: "LongDescription",
                value: "A detailed set of toy soldiers in different poses, complete with gear and vehicles. Perfect for military-themed play and battlefield re-enactments.");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 10,
                column: "LongDescription",
                value: "This indoor mini basketball hoop is easy to mount and comes with a soft ball. A fun way to practice shots and enjoy physical activity indoors.");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ToyID",
                table: "Reviews",
                column: "ToyID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserID",
                table: "Reviews",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "Toys");
        }
    }
}
