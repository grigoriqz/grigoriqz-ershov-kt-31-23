using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GrigoriqzErshovKt_31_23.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    DisciplineId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar", maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_disciplines_discipline_id", x => x.DisciplineId);
                });

            migrationBuilder.CreateTable(
                name: "Specialtys",
                columns: table => new
                {
                    SpecialtyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "varchar", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "varchar", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_specialtys_specialty_id", x => x.SpecialtyId);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    Course = table.Column<int>(type: "int4", nullable: false),
                    SpecialtyId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_groups_group_id", x => x.GroupId);
                    table.ForeignKey(
                        name: "fk_groups_specialty_id",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialtys",
                        principalColumn: "SpecialtyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_students_student_id", x => x.StudentId);
                    table.ForeignKey(
                        name: "fk_students_group_id",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    GradeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<int>(type: "int4", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    DisciplineId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_grades_grade_id", x => x.GradeId);
                    table.ForeignKey(
                        name: "fk_grades_discipline_id",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "DisciplineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_grades_student_id",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_DisciplineId",
                table: "Grades",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_SpecialtyId",
                table: "Groups",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupId",
                table: "Students",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Specialtys");
        }
    }
}
