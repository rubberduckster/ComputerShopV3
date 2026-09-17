using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicShopDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitsSoldField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitsSold",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitsSold",
                table: "Products");
        }
    }
}
