using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vimata.Data.Migrations
{
    public partial class AlternativeAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGreekContent",
                table: "OpenExercises");

            migrationBuilder.DropColumn(
                name: "ArePiecesInGreek",
                table: "DragAndDropExercises");

            migrationBuilder.DropColumn(
                name: "IsGreekContent",
                table: "DragAndDropExercises");

            migrationBuilder.DropColumn(
                name: "AreOptionsInGreek",
                table: "ClosedExercises");

            migrationBuilder.DropColumn(
                name: "IsGreekContent",
                table: "ClosedExercises");

            migrationBuilder.AddColumn<bool>(
                name: "TextToSpeechContent",
                table: "OpenExercises",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TextToSpeechContent",
                table: "DragAndDropExercises",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TextToSpeechOptions",
                table: "DragAndDropExercises",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TextToSpeechContent",
                table: "ClosedExercises",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TextToSpeechOptions",
                table: "ClosedExercises",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "OpenExerciseAlternativeAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    ExerciseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenExerciseAlternativeAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenExerciseAlternativeAnswer_OpenExercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "OpenExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 4, 27, 17, 51, 24, 898, DateTimeKind.Local).AddTicks(1436));

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 4, 27, 17, 51, 24, 900, DateTimeKind.Local).AddTicks(1127));

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 4, 27, 17, 51, 24, 900, DateTimeKind.Local).AddTicks(1173));

            migrationBuilder.CreateIndex(
                name: "IX_OpenExerciseAlternativeAnswer_ExerciseId",
                table: "OpenExerciseAlternativeAnswer",
                column: "ExerciseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenExerciseAlternativeAnswer");

            migrationBuilder.DropColumn(
                name: "TextToSpeechContent",
                table: "OpenExercises");

            migrationBuilder.DropColumn(
                name: "TextToSpeechContent",
                table: "DragAndDropExercises");

            migrationBuilder.DropColumn(
                name: "TextToSpeechOptions",
                table: "DragAndDropExercises");

            migrationBuilder.DropColumn(
                name: "TextToSpeechContent",
                table: "ClosedExercises");

            migrationBuilder.DropColumn(
                name: "TextToSpeechOptions",
                table: "ClosedExercises");

            migrationBuilder.AddColumn<bool>(
                name: "IsGreekContent",
                table: "OpenExercises",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ArePiecesInGreek",
                table: "DragAndDropExercises",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsGreekContent",
                table: "DragAndDropExercises",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AreOptionsInGreek",
                table: "ClosedExercises",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsGreekContent",
                table: "ClosedExercises",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 4, 19, 14, 41, 57, 477, DateTimeKind.Local).AddTicks(3559));

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 4, 19, 14, 41, 57, 479, DateTimeKind.Local).AddTicks(2840));

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 4, 19, 14, 41, 57, 479, DateTimeKind.Local).AddTicks(2878));
        }
    }
}
