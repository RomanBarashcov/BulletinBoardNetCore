using Microsoft.EntityFrameworkCore.Migrations;

namespace AppleUsed.DAL.Migrations
{
    public partial class addedToConversationNewFields2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerId",
                table: "Conversations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "Conversations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Conversations");
        }
    }
}
