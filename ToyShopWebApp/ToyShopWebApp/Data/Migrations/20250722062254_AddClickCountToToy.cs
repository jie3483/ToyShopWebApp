using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToyShopWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddClickCountToToy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClickCount",
                table: "Toys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 1,
                column: "ClickCount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 2,
                column: "ClickCount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 3,
                column: "ClickCount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 4,
                column: "ClickCount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 5,
                column: "ClickCount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 6,
                column: "ClickCount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 7,
                column: "ClickCount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 8,
                column: "ClickCount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 9,
                column: "ClickCount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Toys",
                keyColumn: "Id",
                keyValue: 10,
                column: "ClickCount",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClickCount",
                table: "Toys");
        }
    }
}
