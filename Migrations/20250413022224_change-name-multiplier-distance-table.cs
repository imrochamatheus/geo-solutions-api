using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSolucoesAPI.Migrations
{
    /// <inheritdoc />
    public partial class changenamemultiplierdistancetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Distances",
                newName: "Multiplier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Multiplier",
                table: "Distances",
                newName: "Price");
        }
    }
}
