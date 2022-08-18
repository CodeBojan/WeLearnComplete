using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeLearn.Data.Migrations
{
    public partial class Add_StudyYear_Indices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StudyYears_FullName",
                table: "StudyYears",
                column: "FullName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudyYears_ShortName",
                table: "StudyYears",
                column: "ShortName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudyYears_FullName",
                table: "StudyYears");

            migrationBuilder.DropIndex(
                name: "IX_StudyYears_ShortName",
                table: "StudyYears");
        }
    }
}
