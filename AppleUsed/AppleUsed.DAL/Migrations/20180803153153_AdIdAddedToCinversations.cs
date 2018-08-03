using Microsoft.EntityFrameworkCore.Migrations;

namespace AppleUsed.DAL.Migrations
{
    public partial class AdIdAddedToCinversations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdId",
                table: "ConversationMessages",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdId",
                table: "ConversationMessages");
        }
    }
}
