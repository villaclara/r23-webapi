using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Road23.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class CustomerDetailsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Receiver_ReceiverId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "CustomerDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Receiver",
                table: "Receiver");

            migrationBuilder.RenameTable(
                name: "Receiver",
                newName: "Receivers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Receivers",
                table: "Receivers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Receivers_ReceiverId",
                table: "Orders",
                column: "ReceiverId",
                principalTable: "Receivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Receivers_ReceiverId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Receivers",
                table: "Receivers");

            migrationBuilder.RenameTable(
                name: "Receivers",
                newName: "Receiver");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Receiver",
                table: "Receiver",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CustomerDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerDetails_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
    }
}
