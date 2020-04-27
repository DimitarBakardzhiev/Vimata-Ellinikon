using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vimata.Data.Migrations
{
    public partial class AddExerciseOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pieces",
                table: "DragAndDropExercises");

            migrationBuilder.DropColumn(
                name: "Options",
                table: "ClosedExercises");

            migrationBuilder.CreateTable(
                name: "ClosedExerciseOption",
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    ExerciseId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ClosedExerciseOption_ExerciseId",
                table: "ClosedExerciseOption",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_DragAndDropOption_ExerciseId",
                table: "DragAndDropOption",
                column: "ExerciseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClosedExerciseOption");

            migrationBuilder.DropTable(
                name: "DragAndDropOption");

            migrationBuilder.AddColumn<string>(
                name: "Pieces",
                table: "DragAndDropExercises",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Options",
                table: "ClosedExercises",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 4, 17, 15, 12, 2, 687, DateTimeKind.Local).AddTicks(6544));

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 4, 17, 15, 12, 2, 689, DateTimeKind.Local).AddTicks(5322));

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 4, 17, 15, 12, 2, 689, DateTimeKind.Local).AddTicks(5364));
        }
    }
}
