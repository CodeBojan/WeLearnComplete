using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeLearn.Data.Migrations
{
    public partial class Add_DocumentVersion_Fix_Notification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Courses_CourseId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_CourseId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "Contents",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Contents");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "Notifications",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CourseId",
                table: "Notifications",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Courses_CourseId",
                table: "Notifications",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
