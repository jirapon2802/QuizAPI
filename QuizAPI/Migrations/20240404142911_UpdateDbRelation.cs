using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_UserGroupId",
                table: "Users",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_UserGroupId",
                table: "Quizzes",
                column: "UserGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_UserGroups_UserGroupId",
                table: "Quizzes",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "UserGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserGroups_UserGroupId",
                table: "Users",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "UserGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_UserGroups_UserGroupId",
                table: "Quizzes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserGroups_UserGroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserGroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_UserGroupId",
                table: "Quizzes");
        }
    }
}
