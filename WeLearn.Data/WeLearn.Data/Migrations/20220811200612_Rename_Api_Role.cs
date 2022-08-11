using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeLearn.Data.Migrations
{
    public partial class Rename_Api_Role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_ApiRole_RoleId",
                table: "AccountRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiRole",
                table: "ApiRole");

            migrationBuilder.RenameTable(
                name: "ApiRole",
                newName: "ApiRoles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiRoles",
                table: "ApiRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_ApiRoles_RoleId",
                table: "AccountRoles",
                column: "RoleId",
                principalTable: "ApiRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_ApiRoles_RoleId",
                table: "AccountRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiRoles",
                table: "ApiRoles");

            migrationBuilder.RenameTable(
                name: "ApiRoles",
                newName: "ApiRole");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiRole",
                table: "ApiRole",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_ApiRole_RoleId",
                table: "AccountRoles",
                column: "RoleId",
                principalTable: "ApiRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
