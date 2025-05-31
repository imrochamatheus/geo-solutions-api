using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSolucoesAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixaddresstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Budgets_BudgetId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Users_UserId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_BudgetId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_UserId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "IX_Addresses_BudgetId",
                table: "Addresses",
                column: "BudgetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Budgets_BudgetId",
                table: "Addresses",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Users_UserId",
                table: "Addresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
