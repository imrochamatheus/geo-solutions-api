using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSolucoesAPI.Migrations
{
    /// <inheritdoc />
    public partial class deletecascadeintentionservice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceTypeId1",
                table: "IntentionServices",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntentionServices_ServiceTypeId1",
                table: "IntentionServices",
                column: "ServiceTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_IntentionServices_ServiceTypes_ServiceTypeId1",
                table: "IntentionServices",
                column: "ServiceTypeId1",
                principalTable: "ServiceTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntentionServices_ServiceTypes_ServiceTypeId1",
                table: "IntentionServices");

            migrationBuilder.DropIndex(
                name: "IX_IntentionServices_ServiceTypeId1",
                table: "IntentionServices");

            migrationBuilder.DropColumn(
                name: "ServiceTypeId1",
                table: "IntentionServices");
        }
    }
}
