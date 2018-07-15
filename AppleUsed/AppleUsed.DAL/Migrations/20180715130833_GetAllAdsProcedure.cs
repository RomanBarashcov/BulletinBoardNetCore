using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;

namespace AppleUsed.DAL.Migrations
{
    public partial class GetAllAdsProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var pathToStoredProceduresFolder = 
                Directory.GetParent(
                        Directory.GetCurrentDirectory()).Parent.FullName 
                        + "\\AppleUsed\\AppleUsed.DAL\\SroredProcedures\\";

            var pathToSqlScript = Path.GetFullPath(Path.Combine(pathToStoredProceduresFolder, "GetAllAds.sql"));
            string script = File.ReadAllText(pathToSqlScript);
            migrationBuilder.Sql(script,true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.GetAllAds", true);
        }
    }
}
