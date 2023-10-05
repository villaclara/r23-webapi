using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Road23.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedReceiverAndDeletedCustomerDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerDetails_CustomerId",
                table: "CustomerDetails");

            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReceiverRepeat",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Receiver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FathersName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAdress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receiver", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ReceiverId",
                table: "Orders",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDetails_CustomerId",
                table: "CustomerDetails",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Receiver_ReceiverId",
                table: "Orders",
                column: "ReceiverId",
                principalTable: "Receiver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Receiver_ReceiverId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Receiver");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ReceiverId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_CustomerDetails_CustomerId",
                table: "CustomerDetails");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ReceiverRepeat",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDetails_CustomerId",
                table: "CustomerDetails",
                column: "CustomerId",
                unique: true);
        }
    }
}
