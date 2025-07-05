using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToyShopWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBrowsingHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BrowsingHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ToyID = table.Column<int>(type: "int", nullable: false),
                    ViewedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrowsingHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrowsingHistories_Toys_ToyID",
                        column: x => x.ToyID,
                        principalTable: "Toys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrowsingHistories_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrowsingHistories_ToyID",
                table: "BrowsingHistories",
                column: "ToyID");

            migrationBuilder.CreateIndex(
                name: "IX_BrowsingHistories_UserID",
                table: "BrowsingHistories",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrowsingHistories");
        }
    }
}
