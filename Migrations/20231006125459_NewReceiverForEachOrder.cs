using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Road23.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewReceiverForEachOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Receivers_ReceiverId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ReceiverId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Receivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Receivers_OrderId",
                table: "Receivers",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Receivers_Orders_OrderId",
                table: "Receivers",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receivers_Orders_OrderId",
                table: "Receivers");

            migrationBuilder.DropIndex(
                name: "IX_Receivers_OrderId",
                table: "Receivers");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Receivers");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ReceiverId",
                table: "Orders",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Receivers_ReceiverId",
                table: "Orders",
                column: "ReceiverId",
                principalTable: "Receivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
