using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gettit.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixGettitTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GettitTag_Communities_GettitCommunityId",
                table: "GettitTag");

            migrationBuilder.DropForeignKey(
                name: "FK_GettitTag_GettitThread_GettitThreadId",
                table: "GettitTag");

            migrationBuilder.DropIndex(
                name: "IX_GettitTag_GettitCommunityId",
                table: "GettitTag");

            migrationBuilder.DropIndex(
                name: "IX_GettitTag_GettitThreadId",
                table: "GettitTag");

            migrationBuilder.DropColumn(
                name: "GettitCommunityId",
                table: "GettitTag");

            migrationBuilder.DropColumn(
                name: "GettitThreadId",
                table: "GettitTag");

            migrationBuilder.CreateTable(
                name: "GettitCommunityGettitTag",
                columns: table => new
                {
                    GettitCommunityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GettitCommunityGettitTag", x => new { x.GettitCommunityId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_GettitCommunityGettitTag_Communities_GettitCommunityId",
                        column: x => x.GettitCommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GettitCommunityGettitTag_GettitTag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "GettitTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GettitTagGettitThread",
                columns: table => new
                {
                    GettitThreadId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GettitTagGettitThread", x => new { x.GettitThreadId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_GettitTagGettitThread_GettitTag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "GettitTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GettitTagGettitThread_GettitThread_GettitThreadId",
                        column: x => x.GettitThreadId,
                        principalTable: "GettitThread",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GettitCommunityGettitTag_TagsId",
                table: "GettitCommunityGettitTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_GettitTagGettitThread_TagsId",
                table: "GettitTagGettitThread",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GettitCommunityGettitTag");

            migrationBuilder.DropTable(
                name: "GettitTagGettitThread");

            migrationBuilder.AddColumn<string>(
                name: "GettitCommunityId",
                table: "GettitTag",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GettitThreadId",
                table: "GettitTag",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GettitTag_GettitCommunityId",
                table: "GettitTag",
                column: "GettitCommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_GettitTag_GettitThreadId",
                table: "GettitTag",
                column: "GettitThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_GettitTag_Communities_GettitCommunityId",
                table: "GettitTag",
                column: "GettitCommunityId",
                principalTable: "Communities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GettitTag_GettitThread_GettitThreadId",
                table: "GettitTag",
                column: "GettitThreadId",
                principalTable: "GettitThread",
                principalColumn: "Id");
        }
    }
}
