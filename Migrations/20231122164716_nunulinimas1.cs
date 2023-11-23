using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomenuBazesAtsiskaitimas.Migrations
{
    /// <inheritdoc />
    public partial class nunulinimas1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Faculties_FacultyId",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Students_StudentId",
                table: "Lectures");

            migrationBuilder.DropIndex(
                name: "IX_Lectures_FacultyId",
                table: "Lectures");

            migrationBuilder.DropIndex(
                name: "IX_Lectures_StudentId",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Lectures");

            migrationBuilder.CreateTable(
                name: "FacultyLecture",
                columns: table => new
                {
                    FacultiesFacultyId = table.Column<int>(type: "int", nullable: false),
                    LecturesLectureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyLecture", x => new { x.FacultiesFacultyId, x.LecturesLectureId });
                    table.ForeignKey(
                        name: "FK_FacultyLecture_Faculties_FacultiesFacultyId",
                        column: x => x.FacultiesFacultyId,
                        principalTable: "Faculties",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultyLecture_Lectures_LecturesLectureId",
                        column: x => x.LecturesLectureId,
                        principalTable: "Lectures",
                        principalColumn: "LectureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LectureStudent",
                columns: table => new
                {
                    LecturesLectureId = table.Column<int>(type: "int", nullable: false),
                    StudentsStudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureStudent", x => new { x.LecturesLectureId, x.StudentsStudentId });
                    table.ForeignKey(
                        name: "FK_LectureStudent_Lectures_LecturesLectureId",
                        column: x => x.LecturesLectureId,
                        principalTable: "Lectures",
                        principalColumn: "LectureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LectureStudent_Students_StudentsStudentId",
                        column: x => x.StudentsStudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacultyLecture_LecturesLectureId",
                table: "FacultyLecture",
                column: "LecturesLectureId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureStudent_StudentsStudentId",
                table: "LectureStudent",
                column: "StudentsStudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacultyLecture");

            migrationBuilder.DropTable(
                name: "LectureStudent");

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Lectures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Lectures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_FacultyId",
                table: "Lectures",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_StudentId",
                table: "Lectures",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Faculties_FacultyId",
                table: "Lectures",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "FacultyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Students_StudentId",
                table: "Lectures",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");
        }
    }
}
