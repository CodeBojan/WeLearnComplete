using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeLearn.Data.Migrations
{
    public partial class Add_Content_Most : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Accounts_AuthorId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Contents_ContentId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ContentId",
                table: "Comments",
                newName: "IX_Comments_ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_AuthorId",
                table: "Comments",
                newName: "IX_Comments_AuthorId");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseMaterialUploadRequestId",
                table: "Contents",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Contents",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentCount",
                table: "Contents",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExternalSystemId",
                table: "Contents",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudyMaterialId",
                table: "Contents",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CourseMaterialUploadRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    Remark = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseMaterialUploadRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseMaterialUploadRequests_Accounts_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseMaterialUploadRequests_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    Uri = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentId = table.Column<Guid>(type: "uuid", nullable: true),
                    ContentId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Accounts_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contents_CourseMaterialUploadRequestId",
                table: "Contents",
                column: "CourseMaterialUploadRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_CreatorId",
                table: "Contents",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ExternalSystemId",
                table: "Contents",
                column: "ExternalSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_StudyMaterialId",
                table: "Contents",
                column: "StudyMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseMaterialUploadRequests_CourseId",
                table: "CourseMaterialUploadRequests",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseMaterialUploadRequests_CreatorId",
                table: "CourseMaterialUploadRequests",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CommentId",
                table: "Notifications",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ContentId",
                table: "Notifications",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CourseId",
                table: "Notifications",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ReceiverId",
                table: "Notifications",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Accounts_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Contents_ContentId",
                table: "Comments",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Accounts_CreatorId",
                table: "Contents",
                column: "CreatorId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Contents_StudyMaterialId",
                table: "Contents",
                column: "StudyMaterialId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_CourseMaterialUploadRequests_CourseMaterialUploadR~",
                table: "Contents",
                column: "CourseMaterialUploadRequestId",
                principalTable: "CourseMaterialUploadRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_ExternalSystems_ExternalSystemId",
                table: "Contents",
                column: "ExternalSystemId",
                principalTable: "ExternalSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Accounts_AuthorId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Contents_ContentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Accounts_CreatorId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Contents_StudyMaterialId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_CourseMaterialUploadRequests_CourseMaterialUploadR~",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_ExternalSystems_ExternalSystemId",
                table: "Contents");

            migrationBuilder.DropTable(
                name: "CourseMaterialUploadRequests");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Contents_CourseMaterialUploadRequestId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_CreatorId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_ExternalSystemId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_StudyMaterialId",
                table: "Contents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CourseMaterialUploadRequestId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "DocumentCount",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "ExternalSystemId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "StudyMaterialId",
                table: "Contents");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ContentId",
                table: "Comment",
                newName: "IX_Comment_ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AuthorId",
                table: "Comment",
                newName: "IX_Comment_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Accounts_AuthorId",
                table: "Comment",
                column: "AuthorId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Contents_ContentId",
                table: "Comment",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
