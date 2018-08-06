using Microsoft.EntityFrameworkCore.Migrations;

namespace AppleUsed.DAL.Migrations
{
    public partial class addedToConversationNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerName",
                table: "Conversations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerName",
                table: "Conversations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerName",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "SellerName",
                table: "Conversations");
        }
    }
}
