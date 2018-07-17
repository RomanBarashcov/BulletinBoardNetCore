using Microsoft.EntityFrameworkCore.Migrations;

namespace AppleUsed.DAL.Migrations
{
    public partial class NewCharacteristicsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_ProductColors_ProductColorsId",
                table: "Characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_ProductMemories_ProductMemoriesId",
                table: "Characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_ProductModels_ProductModelsId",
                table: "Characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_ProductStates_ProductStatesId",
                table: "Characteristics");

            migrationBuilder.DropIndex(
                name: "IX_Characteristics_ProductColorsId",
                table: "Characteristics");

            migrationBuilder.DropIndex(
                name: "IX_Characteristics_ProductMemoriesId",
                table: "Characteristics");

            migrationBuilder.DropIndex(
                name: "IX_Characteristics_ProductModelsId",
                table: "Characteristics");

            migrationBuilder.DropIndex(
                name: "IX_Characteristics_ProductStatesId",
                table: "Characteristics");

            migrationBuilder.AlterColumn<int>(
                name: "ProductStatesId",
                table: "Characteristics",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductModelsId",
                table: "Characteristics",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductMemoriesId",
                table: "Characteristics",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductColorsId",
                table: "Characteristics",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypesId",
                table: "Characteristics",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductTypesId",
                table: "Characteristics");

            migrationBuilder.AlterColumn<int>(
                name: "ProductStatesId",
                table: "Characteristics",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProductModelsId",
                table: "Characteristics",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProductMemoriesId",
                table: "Characteristics",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProductColorsId",
                table: "Characteristics",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_ProductColorsId",
                table: "Characteristics",
                column: "ProductColorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_ProductMemoriesId",
                table: "Characteristics",
                column: "ProductMemoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_ProductModelsId",
                table: "Characteristics",
                column: "ProductModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_ProductStatesId",
                table: "Characteristics",
                column: "ProductStatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_ProductColors_ProductColorsId",
                table: "Characteristics",
                column: "ProductColorsId",
                principalTable: "ProductColors",
                principalColumn: "ProductColorsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_ProductMemories_ProductMemoriesId",
                table: "Characteristics",
                column: "ProductMemoriesId",
                principalTable: "ProductMemories",
                principalColumn: "ProductMemoriesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_ProductModels_ProductModelsId",
                table: "Characteristics",
                column: "ProductModelsId",
                principalTable: "ProductModels",
                principalColumn: "ProductModelsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_ProductStates_ProductStatesId",
                table: "Characteristics",
                column: "ProductStatesId",
                principalTable: "ProductStates",
                principalColumn: "ProductStatesId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
