using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gettit.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ForumRoles_ForumRoleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumRoles_AspNetUsers_CreatedById",
                table: "ForumRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumRoles_AspNetUsers_Id",
                table: "ForumRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumRoles_AspNetUsers_UpdatedById",
                table: "ForumRoles");

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "ForumRoles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ForumRoles_DeletedById",
                table: "ForumRoles",
                column: "DeletedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ForumRoles_ForumRoleId",
                table: "AspNetUsers",
                column: "ForumRoleId",
                principalTable: "ForumRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumRoles_AspNetUsers_CreatedById",
                table: "ForumRoles",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumRoles_AspNetUsers_DeletedById",
                table: "ForumRoles",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumRoles_AspNetUsers_UpdatedById",
                table: "ForumRoles",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ForumRoles_ForumRoleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumRoles_AspNetUsers_CreatedById",
                table: "ForumRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumRoles_AspNetUsers_DeletedById",
                table: "ForumRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumRoles_AspNetUsers_UpdatedById",
                table: "ForumRoles");

            migrationBuilder.DropIndex(
                name: "IX_ForumRoles_DeletedById",
                table: "ForumRoles");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "ForumRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ForumRoles_ForumRoleId",
                table: "AspNetUsers",
                column: "ForumRoleId",
                principalTable: "ForumRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumRoles_AspNetUsers_CreatedById",
                table: "ForumRoles",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumRoles_AspNetUsers_Id",
                table: "ForumRoles",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumRoles_AspNetUsers_UpdatedById",
                table: "ForumRoles",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
