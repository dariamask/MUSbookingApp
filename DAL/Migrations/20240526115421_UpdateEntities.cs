using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Orders_OrderId",
                table: "Equipments");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_OrderId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Equipments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Equipments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_OrderId",
                table: "Equipments",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Orders_OrderId",
                table: "Equipments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
