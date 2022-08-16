using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeLearn.Data.Migrations
{
    public partial class Added_CourseCredentials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Courses_CourseId",
                table: "Credentials");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowedStudyYear_Accounts_AccountId",
                table: "FollowedStudyYear");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowedStudyYear_StudyYears_StudyYearId",
                table: "FollowedStudyYear");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FollowedStudyYear",
                table: "FollowedStudyYear");

            migrationBuilder.RenameTable(
                name: "FollowedStudyYear",
                newName: "FollowedStudyYears");

            migrationBuilder.RenameIndex(
                name: "IX_FollowedStudyYear_StudyYearId",
                table: "FollowedStudyYears",
                newName: "IX_FollowedStudyYears_StudyYearId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FollowedStudyYears",
                table: "FollowedStudyYears",
                columns: new[] { "AccountId", "StudyYearId" });

            migrationBuilder.CreateTable(
                name: "CourseCredentials",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CredentialsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCredentials", x => new { x.CourseId, x.CredentialsId });
                    table.ForeignKey(
                        name: "FK_CourseCredentials_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCredentials_Credentials_CredentialsId",
                        column: x => x.CredentialsId,
                        principalTable: "Credentials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCredentials_CredentialsId",
                table: "CourseCredentials",
                column: "CredentialsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Courses_CourseId",
                table: "Credentials",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowedStudyYears_Accounts_AccountId",
                table: "FollowedStudyYears",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowedStudyYears_StudyYears_StudyYearId",
                table: "FollowedStudyYears",
                column: "StudyYearId",
                principalTable: "StudyYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Courses_CourseId",
                table: "Credentials");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowedStudyYears_Accounts_AccountId",
                table: "FollowedStudyYears");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowedStudyYears_StudyYears_StudyYearId",
                table: "FollowedStudyYears");

            migrationBuilder.DropTable(
                name: "CourseCredentials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FollowedStudyYears",
                table: "FollowedStudyYears");

            migrationBuilder.RenameTable(
                name: "FollowedStudyYears",
                newName: "FollowedStudyYear");

            migrationBuilder.RenameIndex(
                name: "IX_FollowedStudyYears_StudyYearId",
                table: "FollowedStudyYear",
                newName: "IX_FollowedStudyYear_StudyYearId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FollowedStudyYear",
                table: "FollowedStudyYear",
                columns: new[] { "AccountId", "StudyYearId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Courses_CourseId",
                table: "Credentials",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowedStudyYear_Accounts_AccountId",
                table: "FollowedStudyYear",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowedStudyYear_StudyYears_StudyYearId",
                table: "FollowedStudyYear",
                column: "StudyYearId",
                principalTable: "StudyYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
