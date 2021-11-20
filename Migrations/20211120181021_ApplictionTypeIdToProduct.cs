using Microsoft.EntityFrameworkCore.Migrations;

namespace Rocy.Migrations
{
    public partial class ApplictionTypeIdToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationTypeId",
                table: "product",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_product_ApplicationTypeId",
                table: "product",
                column: "ApplicationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_ApplicationTypes__ApplicationTypeId",
                table: "product",
                column: "ApplicationTypeId",
                principalTable: "ApplicationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_ApplicationTypes__ApplicationTypeId",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_ApplicationTypeId",
                table: "product");

            migrationBuilder.DropColumn(
                name: "ApplicationTypeId",
                table: "product");
        }
    }
}
