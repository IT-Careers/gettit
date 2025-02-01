using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gettit.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class GenericMetadataConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Threads_AspNetUsers_CreatedById",
                table: "Threads");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_AspNetUsers_DeletedById",
                table: "Threads");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_AspNetUsers_UpdatedById",
                table: "Threads");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Categories_CategoryId",
                table: "Threads");

            migrationBuilder.DropForeignKey(
                name: "FK_UserThreadComment_Threads_ThreadId",
                table: "UserThreadComment");

            migrationBuilder.DropForeignKey(
                name: "FK_UserThreadReaction_Threads_ThreadId",
                table: "UserThreadReaction");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Threads",
                table: "Threads");

            migrationBuilder.RenameTable(
                name: "Threads",
                newName: "GettitThread");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "GettitThread",
                newName: "CommunityId");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_UpdatedById",
                table: "GettitThread",
                newName: "IX_GettitThread_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_DeletedById",
                table: "GettitThread",
                newName: "IX_GettitThread_DeletedById");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_CreatedById",
                table: "GettitThread",
                newName: "IX_GettitThread_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_CategoryId",
                table: "GettitThread",
                newName: "IX_GettitThread_CommunityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GettitThread",
                table: "GettitThread",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Communities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailPhotoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BannerPhotoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Communities_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Communities_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Communities_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Communities_Attachments_BannerPhotoId",
                        column: x => x.BannerPhotoId,
                        principalTable: "Attachments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Communities_Attachments_ThumbnailPhotoId",
                        column: x => x.ThumbnailPhotoId,
                        principalTable: "Attachments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GettitTag",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GettitCommunityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GettitThreadId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GettitTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GettitTag_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GettitTag_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GettitTag_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GettitTag_Communities_GettitCommunityId",
                        column: x => x.GettitCommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GettitTag_GettitThread_GettitThreadId",
                        column: x => x.GettitThreadId,
                        principalTable: "GettitThread",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Communities_BannerPhotoId",
                table: "Communities",
                column: "BannerPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Communities_CreatedById",
                table: "Communities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Communities_DeletedById",
                table: "Communities",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Communities_ThumbnailPhotoId",
                table: "Communities",
                column: "ThumbnailPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Communities_UpdatedById",
                table: "Communities",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_GettitTag_CreatedById",
                table: "GettitTag",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_GettitTag_DeletedById",
                table: "GettitTag",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_GettitTag_GettitCommunityId",
                table: "GettitTag",
                column: "GettitCommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_GettitTag_GettitThreadId",
                table: "GettitTag",
                column: "GettitThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_GettitTag_UpdatedById",
                table: "GettitTag",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_GettitThread_AspNetUsers_CreatedById",
                table: "GettitThread",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GettitThread_AspNetUsers_DeletedById",
                table: "GettitThread",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GettitThread_AspNetUsers_UpdatedById",
                table: "GettitThread",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GettitThread_Communities_CommunityId",
                table: "GettitThread",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserThreadComment_GettitThread_ThreadId",
                table: "UserThreadComment",
                column: "ThreadId",
                principalTable: "GettitThread",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserThreadReaction_GettitThread_ThreadId",
                table: "UserThreadReaction",
                column: "ThreadId",
                principalTable: "GettitThread",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GettitThread_AspNetUsers_CreatedById",
                table: "GettitThread");

            migrationBuilder.DropForeignKey(
                name: "FK_GettitThread_AspNetUsers_DeletedById",
                table: "GettitThread");

            migrationBuilder.DropForeignKey(
                name: "FK_GettitThread_AspNetUsers_UpdatedById",
                table: "GettitThread");

            migrationBuilder.DropForeignKey(
                name: "FK_GettitThread_Communities_CommunityId",
                table: "GettitThread");

            migrationBuilder.DropForeignKey(
                name: "FK_UserThreadComment_GettitThread_ThreadId",
                table: "UserThreadComment");

            migrationBuilder.DropForeignKey(
                name: "FK_UserThreadReaction_GettitThread_ThreadId",
                table: "UserThreadReaction");

            migrationBuilder.DropTable(
                name: "GettitTag");

            migrationBuilder.DropTable(
                name: "Communities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GettitThread",
                table: "GettitThread");

            migrationBuilder.RenameTable(
                name: "GettitThread",
                newName: "Threads");

            migrationBuilder.RenameColumn(
                name: "CommunityId",
                table: "Threads",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_GettitThread_UpdatedById",
                table: "Threads",
                newName: "IX_Threads_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_GettitThread_DeletedById",
                table: "Threads",
                newName: "IX_Threads_DeletedById");

            migrationBuilder.RenameIndex(
                name: "IX_GettitThread_CreatedById",
                table: "Threads",
                newName: "IX_Threads_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_GettitThread_CommunityId",
                table: "Threads",
                newName: "IX_Threads_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Threads",
                table: "Threads",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CoverPhotoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeletedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categories_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categories_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categories_Attachments_CoverPhotoId",
                        column: x => x.CoverPhotoId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CoverPhotoId",
                table: "Categories",
                column: "CoverPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedById",
                table: "Categories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DeletedById",
                table: "Categories",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UpdatedById",
                table: "Categories",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_AspNetUsers_CreatedById",
                table: "Threads",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_AspNetUsers_DeletedById",
                table: "Threads",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_AspNetUsers_UpdatedById",
                table: "Threads",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Categories_CategoryId",
                table: "Threads",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserThreadComment_Threads_ThreadId",
                table: "UserThreadComment",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserThreadReaction_Threads_ThreadId",
                table: "UserThreadReaction",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
