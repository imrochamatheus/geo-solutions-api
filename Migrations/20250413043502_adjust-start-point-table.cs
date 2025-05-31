using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSolucoesAPI.Migrations
{
    /// <inheritdoc />
    public partial class adjuststartpointtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Zipcode",
                table: "StartPoints",
                newName: "Street");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "StartPoints",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "StartPoints",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "StartPoints",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "StartPoints");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "StartPoints");

            migrationBuilder.DropColumn(
                name: "State",
                table: "StartPoints");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "StartPoints",
                newName: "Zipcode");
        }
    }
}
