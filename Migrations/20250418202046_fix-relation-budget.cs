using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GeoSolucoesAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixrelationbudget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTypes_Budgets_BudgetId",
                table: "ServiceTypes");

            migrationBuilder.DropIndex(
                name: "IX_ServiceTypes_BudgetId",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "ServiceTypes");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Cities",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(2)",
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Cities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "IntentionServiceId",
                table: "Budgets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceTypeId",
                table: "Budgets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_ServiceTypeId",
                table: "Budgets",
                column: "ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_ServiceTypes_ServiceTypeId",
                table: "Budgets",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_ServiceTypes_ServiceTypeId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_ServiceTypeId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "IntentionServiceId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "ServiceTypeId",
                table: "Budgets");

            migrationBuilder.AddColumn<int>(
                name: "BudgetId",
                table: "ServiceTypes",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Cities",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Cities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypes_BudgetId",
                table: "ServiceTypes",
                column: "BudgetId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTypes_Budgets_BudgetId",
                table: "ServiceTypes",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id");
        }
    }
}
