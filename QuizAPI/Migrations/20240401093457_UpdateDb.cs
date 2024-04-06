using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserQuizzes_UserId",
                table: "UserQuizzes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuizId",
                table: "Answers",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Quizzes_QuizId",
                table: "Answers",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuizzes_Users_UserId",
                table: "UserQuizzes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Quizzes_QuizId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuizzes_Users_UserId",
                table: "UserQuizzes");

            migrationBuilder.DropIndex(
                name: "IX_UserQuizzes_UserId",
                table: "UserQuizzes");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuizId",
                table: "Answers");
        }
    }
}
