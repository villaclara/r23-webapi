using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Road23.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedFloatToHeightInCandleItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "HeightCM",
                table: "Candles",
                type: "REAL",
                precision: 8,
                scale: 1,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HeightCM",
                table: "Candles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL",
                oldPrecision: 8,
                oldScale: 1);
        }
    }
}
