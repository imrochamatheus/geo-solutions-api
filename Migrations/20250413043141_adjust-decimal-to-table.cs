using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSolucoesAPI.Migrations
{
    /// <inheritdoc />
    public partial class adjustdecimaltotable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Limit_Area",
                table: "IntentionServices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Limit_Area",
                table: "IntentionServices",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
