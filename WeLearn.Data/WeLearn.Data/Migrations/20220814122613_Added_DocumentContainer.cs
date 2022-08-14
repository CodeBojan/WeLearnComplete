using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeLearn.Data.Migrations
{
    public partial class Added_DocumentContainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Contents_StudyMaterialId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Accounts_AccountId",
                table: "Credentials");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Credentials",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Credentials_AccountId",
                table: "Credentials",
                newName: "IX_Credentials_CreatorId");

            migrationBuilder.RenameColumn(
                name: "StudyMaterialId",
                table: "Contents",
                newName: "DocumentContainerId");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_StudyMaterialId",
                table: "Contents",
                newName: "IX_Contents_DocumentContainerId");

            migrationBuilder.RenameColumn(
                name: "FacultyId",
                table: "Accounts",
                newName: "StudentId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "FollowedStudyYear",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "FollowedStudyYear",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Contents_DocumentContainerId",
                table: "Contents",
                column: "DocumentContainerId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Accounts_CreatorId",
                table: "Credentials",
                column: "CreatorId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Contents_DocumentContainerId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Accounts_CreatorId",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "FollowedStudyYear");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "FollowedStudyYear");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Credentials",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Credentials_CreatorId",
                table: "Credentials",
                newName: "IX_Credentials_AccountId");

            migrationBuilder.RenameColumn(
                name: "DocumentContainerId",
                table: "Contents",
                newName: "StudyMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_DocumentContainerId",
                table: "Contents",
                newName: "IX_Contents_StudyMaterialId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Accounts",
                newName: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Contents_StudyMaterialId",
                table: "Contents",
                column: "StudyMaterialId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Accounts_AccountId",
                table: "Credentials",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
