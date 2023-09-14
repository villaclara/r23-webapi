using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Road23.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedCommentsAndReceiverToOrdersModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "Orders");
        }
    }
}
