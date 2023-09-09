using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Road23.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedHeightToCandle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeightCM",
                table: "Candles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeightCM",
                table: "Candles");
        }
    }
}
