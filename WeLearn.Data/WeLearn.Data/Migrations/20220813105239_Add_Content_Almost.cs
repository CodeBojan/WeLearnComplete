using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeLearn.Data.Migrations
{
    public partial class Add_Content_Almost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CourseId",
                table: "Contents",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "StudyYearId",
                table: "Contents",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FollowedStudyYear",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudyYearId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowedStudyYear", x => new { x.AccountId, x.StudyYearId });
                    table.ForeignKey(
                        name: "FK_FollowedStudyYear_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowedStudyYear_StudyYears_StudyYearId",
                        column: x => x.StudyYearId,
                        principalTable: "StudyYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contents_StudyYearId",
                table: "Contents",
                column: "StudyYearId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowedStudyYear_StudyYearId",
                table: "FollowedStudyYear",
                column: "StudyYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_StudyYears_StudyYearId",
                table: "Contents",
                column: "StudyYearId",
                principalTable: "StudyYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_StudyYears_StudyYearId",
                table: "Contents");

            migrationBuilder.DropTable(
                name: "FollowedStudyYear");

            migrationBuilder.DropIndex(
                name: "IX_Contents_StudyYearId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "StudyYearId",
                table: "Contents");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseId",
                table: "Contents",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }
    }
}
