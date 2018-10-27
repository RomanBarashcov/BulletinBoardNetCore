using Microsoft.EntityFrameworkCore.Migrations;

namespace AppleUsed.DAL.Migrations
{
    public partial class MinorUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Cities_CityId",
                table: "Ads");

            migrationBuilder.DropIndex(
                name: "IX_Ads_CityId",
                table: "Ads");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Ads",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Ads",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Ads_CityId",
                table: "Ads",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Cities_CityId",
                table: "Ads",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
