using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSolucoesAPI.Migrations
{
    /// <inheritdoc />
    public partial class configuredeletecascadebudget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Addresses_AddressId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_AddressId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Budgets");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_IntentionServiceId",
                table: "Budgets",
                column: "IntentionServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_BudgetId",
                table: "Addresses",
                column: "BudgetId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Budgets_BudgetId",
                table: "Addresses",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_IntentionServices_IntentionServiceId",
                table: "Budgets",
                column: "IntentionServiceId",
                principalTable: "IntentionServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Budgets_BudgetId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_IntentionServices_IntentionServiceId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_IntentionServiceId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_BudgetId",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Budgets",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_AddressId",
                table: "Budgets",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Addresses_AddressId",
                table: "Budgets",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
