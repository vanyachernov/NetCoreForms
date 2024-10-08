using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuestionModelFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_answers_questions_question_id1",
                table: "answers");

            migrationBuilder.DropIndex(
                name: "ix_answers_question_id1",
                table: "answers");

            migrationBuilder.DropColumn(
                name: "question_id1",
                table: "answers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "question_id1",
                table: "answers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_answers_question_id1",
                table: "answers",
                column: "question_id1");

            migrationBuilder.AddForeignKey(
                name: "fk_answers_questions_question_id1",
                table: "answers",
                column: "question_id1",
                principalTable: "questions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
