using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSolucoesAPI.Migrations
{
    /// <inheritdoc />
    public partial class adjustrelationservicetypebudget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Budgets_ServiceTypeId",
                table: "Budgets");

            migrationBuilder.AddColumn<int>(
                name: "BudgetId",
                table: "Budgets",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_BudgetId",
                table: "Budgets",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_ServiceTypeId",
                table: "Budgets",
                column: "ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_ServiceTypes_BudgetId",
                table: "Budgets",
                column: "BudgetId",
                principalTable: "ServiceTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_ServiceTypes_BudgetId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_BudgetId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_ServiceTypeId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "Budgets");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_ServiceTypeId",
                table: "Budgets",
                column: "ServiceTypeId",
                unique: true);
        }
    }
}
