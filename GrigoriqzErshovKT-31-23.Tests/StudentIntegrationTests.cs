using GrigoriqzErshovKt_31_23.Database;
using GrigoriqzErshovKt_31_23.Filters.StudentFilters;
using GrigoriqzErshovKt_31_23.Interfaces.StudentsInterfaces;
using GrigoriqzErshovKt_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace GrigoriqzErshovKT_31_23.Tests
{
    public class StudentIntegrationTests
    {
        public readonly DbContextOptions<StudentDbContext> _dbContextOptions;

        public StudentIntegrationTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetStudentsByGroupAsync_gr1_TwoObjects()
        {
            // Arrange
            var ctx = new StudentDbContext(_dbContextOptions);
            var studentService = new StudentService(ctx);
            var groups = new List<Group>
            {
                new Group
                {
                    Name = "гр1"
                },
                new Group
                {
                    Name = "гр2"
                }
            };
            await ctx.Set<Group>().AddRangeAsync(groups);

            var students = new List<Student>
            {
                new Student
                {
                    FirstName = "Иван",
                    LastName = "Иванов",
                    GroupId = 1,
                },
                new Student
                {
                    FirstName = "Иван",
                    LastName = "Петр",
                    GroupId = 2,
                },
                new Student
                {
                    FirstName = "Петров",
                    LastName = "Иван",
                    GroupId = 1,
                }
            };
            await ctx.Set<Student>().AddRangeAsync(students);

            await ctx.SaveChangesAsync();

            // Act
            var filter = new StudentGroupFilter
            {
                GroupName = "гр1"
            };
            var studentsResult = await studentService.GetStudentsByGroupAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(2, studentsResult.Length);
        }

        [Fact]
        public async Task GetGradesByGroupAsync_gr1_TwoValues()
        {
            // Arrange
            var ctx = new StudentDbContext(_dbContextOptions);
            var studentService = new StudentService(ctx);
            var groups = new List<Group>
            {
                new Group
                {
                    Name = "гр1"
                },
                new Group
                {
                    Name = "гр2"
                }
            };
            await ctx.Set<Group>().AddRangeAsync(groups);

            var students = new List<Student>
            {
                new Student
                {
                    FirstName = "Иван",
                    LastName = "Иванов",
                    GroupId = 1,
                },
                new Student
                {
                    FirstName = "Иван",
                    LastName = "Петр",
                    GroupId = 2,
                },
                new Student
                {
                    FirstName = "Петров",
                    LastName = "Иван",
                    GroupId = 1,
                }
            };
            await ctx.Set<Student>().AddRangeAsync(students);
            await ctx.Set<Discipline>().AddAsync(new Discipline { Name = "1дисп" });
            await ctx.Set<Grade>().AddRangeAsync(
                new Grade { Value = 5, StudentId = 1, DisciplineId = 1 },
                new Grade { Value = 2, StudentId = 1, DisciplineId = 1 },
                new Grade { Value = 5, StudentId = 3, DisciplineId = 1 },
                new Grade { Value = 3, StudentId = 2, DisciplineId = 1 });
            await ctx.SaveChangesAsync();

            // Act
            var filter = new GradesGroupFilter
            {
                GroupName = "гр1"
            };
            var gradesResult = await studentService.GetGradesByGroupAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(new[] { 2, 5 }, gradesResult);
        }
    }
}
