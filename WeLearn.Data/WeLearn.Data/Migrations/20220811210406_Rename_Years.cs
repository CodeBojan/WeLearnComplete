using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeLearn.Data.Migrations
{
    public partial class Rename_Years : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_Years_StudyYearId",
                table: "AccountRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Years_StudyYearId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Years",
                table: "Years");

            migrationBuilder.RenameTable(
                name: "Years",
                newName: "StudyYears");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyYears",
                table: "StudyYears",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_StudyYears_StudyYearId",
                table: "AccountRoles",
                column: "StudyYearId",
                principalTable: "StudyYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_StudyYears_StudyYearId",
                table: "Courses",
                column: "StudyYearId",
                principalTable: "StudyYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_StudyYears_StudyYearId",
                table: "AccountRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_StudyYears_StudyYearId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyYears",
                table: "StudyYears");

            migrationBuilder.RenameTable(
                name: "StudyYears",
                newName: "Years");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Years",
                table: "Years",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_Years_StudyYearId",
                table: "AccountRoles",
                column: "StudyYearId",
                principalTable: "Years",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Years_StudyYearId",
                table: "Courses",
                column: "StudyYearId",
                principalTable: "Years",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
