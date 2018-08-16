using Microsoft.EntityFrameworkCore.Migrations;

namespace AppleUsed.DAL.Migrations
{
    public partial class Minor_Changes_In_Purchases_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Purchases_PurchasedId",
                table: "Ads");

            migrationBuilder.DropIndex(
                name: "IX_Ads_PurchasedId",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "PurchasedId",
                table: "Ads");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Purchases",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_AdId",
                table: "Purchases",
                column: "AdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Ads_AdId",
                table: "Purchases",
                column: "AdId",
                principalTable: "Ads",
                principalColumn: "AdId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Ads_AdId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_AdId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Purchases");

            migrationBuilder.AddColumn<int>(
                name: "PurchasedId",
                table: "Ads",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ads_PurchasedId",
                table: "Ads",
                column: "PurchasedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Purchases_PurchasedId",
                table: "Ads",
                column: "PurchasedId",
                principalTable: "Purchases",
                principalColumn: "PurchaseId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
