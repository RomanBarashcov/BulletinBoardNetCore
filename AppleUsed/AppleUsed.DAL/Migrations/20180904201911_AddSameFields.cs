using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppleUsed.DAL.Migrations
{
    public partial class AddSameFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "DaysOfActiveService",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "ServiceActiveTimeId",
                table: "Purchases",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ServiceActiveTimes",
                columns: table => new
                {
                    ServiceActiveTimeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cost = table.Column<decimal>(nullable: false),
                    DaysOfActiveService = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false),
                    ServicesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceActiveTimes", x => x.ServiceActiveTimeId);
                    table.ForeignKey(
                        name: "FK_ServiceActiveTimes_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "ServicesId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceActiveTimes_ServicesId",
                table: "ServiceActiveTimes",
                column: "ServicesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceActiveTimes");

            migrationBuilder.DropColumn(
                name: "ServiceActiveTimeId",
                table: "Purchases");

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Services",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "DaysOfActiveService",
                table: "Services",
                nullable: false,
                defaultValue: 0);
        }
    }
}
