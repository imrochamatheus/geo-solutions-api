using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSolucoesAPI.Migrations
{
    /// <inheritdoc />
    public partial class adjustareasettingsbudgettable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropertyArea",
                table: "Budgets");

            migrationBuilder.AddColumn<decimal>(
                name: "AreaSize",
                table: "Budgets",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasure",
                table: "Budgets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaSize",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasure",
                table: "Budgets");

            migrationBuilder.AddColumn<string>(
                name: "PropertyArea",
                table: "Budgets",
                type: "text",
                nullable: true);
        }
    }
}
