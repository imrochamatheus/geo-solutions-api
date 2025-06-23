using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSolucoesAPI.Migrations
{
    /// <inheritdoc />
    public partial class adjustconfrontationfromintentionservice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasConfrontation",
                table: "IntentionServices",
                newName: "UrbanConfrontation");

            migrationBuilder.AddColumn<bool>(
                name: "RuralConfrontation",
                table: "IntentionServices",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RuralConfrontation",
                table: "IntentionServices");

            migrationBuilder.RenameColumn(
                name: "UrbanConfrontation",
                table: "IntentionServices",
                newName: "HasConfrontation");
        }
    }
}
