using Microsoft.EntityFrameworkCore.Migrations;

namespace AppleUsed.DAL.Migrations
{
    public partial class Cahged_In_Services_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Ads_AdId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Services_ServicesId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_AdId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ServicesId",
                table: "Purchases");

            migrationBuilder.AlterColumn<int>(
                name: "ServicesId",
                table: "Purchases",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdId",
                table: "Purchases",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ServicesId",
                table: "Purchases",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AdId",
                table: "Purchases",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_AdId",
                table: "Purchases",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ServicesId",
                table: "Purchases",
                column: "ServicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Ads_AdId",
                table: "Purchases",
                column: "AdId",
                principalTable: "Ads",
                principalColumn: "AdId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Services_ServicesId",
                table: "Purchases",
                column: "ServicesId",
                principalTable: "Services",
                principalColumn: "ServicesId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
