using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSolucoesAPI.Migrations
{
    /// <inheritdoc />
    public partial class dbadjust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Budgets_ServiceTypeId",
                table: "Budgets");

            migrationBuilder.AddColumn<int>(
                name: "BudgetId",
                table: "ServiceTypes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ServiceTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "IntentionServices",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_ServiceTypeId",
                table: "Budgets",
                column: "ServiceTypeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Budgets_ServiceTypeId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "IntentionServices");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_ServiceTypeId",
                table: "Budgets",
                column: "ServiceTypeId");
        }
    }
}
