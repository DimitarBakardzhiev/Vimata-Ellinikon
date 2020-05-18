using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vimata.Data.Migrations
{
    public partial class ExercisesRework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClosedExerciseOption");

            migrationBuilder.DropTable(
                name: "DragAndDropOption");

            migrationBuilder.DropTable(
                name: "OpenExerciseAlternativeAnswer");

            migrationBuilder.DropTable(
                name: "SpeakingExercises");

            migrationBuilder.DropTable(
                name: "ClosedExercises");

            migrationBuilder.DropTable(
                name: "DragAndDropExercises");

            migrationBuilder.DropTable(
                name: "OpenExercises");

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CorrectAnswer = table.Column<string>(nullable: true),
                    TextToSpeechContent = table.Column<bool>(nullable: false),
                    TextToSpeechOptions = table.Column<bool>(nullable: false),
                    IsHearingExercise = table.Column<bool>(nullable: false),
                    LessonId = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlternativeAnswers",
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
                    table.PrimaryKey("PK_AlternativeAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlternativeAnswers_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseOptions",
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
                    table.PrimaryKey("PK_ExerciseOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseOptions_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 18, 34, 25, 438, DateTimeKind.Local).AddTicks(1932));

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 18, 34, 25, 440, DateTimeKind.Local).AddTicks(1414));

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 18, 34, 25, 440, DateTimeKind.Local).AddTicks(1452));

            migrationBuilder.CreateIndex(
                name: "IX_AlternativeAnswers_ExerciseId",
                table: "AlternativeAnswers",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseOptions_ExerciseId",
                table: "ExerciseOptions",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_LessonId",
                table: "Exercises",
                column: "LessonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlternativeAnswers");

            migrationBuilder.DropTable(
                name: "ExerciseOptions");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.CreateTable(
                name: "ClosedExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHearingExercise = table.Column<bool>(type: "bit", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TextToSpeechContent = table.Column<bool>(type: "bit", nullable: false),
                    TextToSpeechOptions = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosedExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClosedExercises_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DragAndDropExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHearingExercise = table.Column<bool>(type: "bit", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TextToSpeechContent = table.Column<bool>(type: "bit", nullable: false),
                    TextToSpeechOptions = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DragAndDropExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DragAndDropExercises_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHearingExercise = table.Column<bool>(type: "bit", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TextToSpeechContent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenExercises_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpeakingExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHearingExercise = table.Column<bool>(type: "bit", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakingExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpeakingExercises_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClosedExerciseOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosedExerciseOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClosedExerciseOption_ClosedExercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "ClosedExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DragAndDropOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DragAndDropOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DragAndDropOption_DragAndDropExercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "DragAndDropExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenExerciseAlternativeAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "IX_ClosedExerciseOption_ExerciseId",
                table: "ClosedExerciseOption",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosedExercises_LessonId",
                table: "ClosedExercises",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_DragAndDropExercises_LessonId",
                table: "DragAndDropExercises",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_DragAndDropOption_ExerciseId",
                table: "DragAndDropOption",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenExerciseAlternativeAnswer_ExerciseId",
                table: "OpenExerciseAlternativeAnswer",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenExercises_LessonId",
                table: "OpenExercises",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakingExercises_LessonId",
                table: "SpeakingExercises",
                column: "LessonId");
        }
    }
}
