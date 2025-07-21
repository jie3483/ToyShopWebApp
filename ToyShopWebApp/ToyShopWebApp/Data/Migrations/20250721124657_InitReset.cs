using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToyShopWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitReset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "rc_car.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "blocks.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "teddy.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "dino.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "cube.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "train.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "keyboard.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "puzzle.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "soldiers.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "hoop.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/images/rc_car.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/images/blocks.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "/images/teddy.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "/images/dino.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "/images/cube.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "/images/train.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "/images/keyboard.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "/images/puzzle.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "/images/soldiers.jpg");

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "/images/hoop.jpg");
        }
    }
}
