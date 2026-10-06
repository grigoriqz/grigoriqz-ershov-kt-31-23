using GrigoriqzErshovKt_31_23.Database;
using GrigoriqzErshovKt_31_23.Filters.StudentFilters;
using GrigoriqzErshovKt_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace GrigoriqzErshovKt_31_23.Interfaces.StudentsInterfaces
{
    public interface IStudentService
    {
        Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
        Task<int[]> GetGradesByGroupAsync(GradesGroupFilter filter, CancellationToken cancellationToken);
        Task<Student[]> GetStudentsAsync(StudentFilter filter, CancellationToken cancellationToken);
        Task<Student> AddStudentAsync(StudentPayload payload, CancellationToken cancellationToken);
        Task<Student?> UpdateStudentAsync(StudentPayload payload, CancellationToken cancellationToken);
        Task<bool> DeleteStudentAsync(int studentId, CancellationToken cancellationToken);
    }

    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _dbContext;

        public StudentService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken)
        {
            return _dbContext.Set<Student>()
                .Where(s => s.Group.Name == filter.GroupName && !s.IsDeleted)
                .ToArrayAsync(cancellationToken);
        }

        public Task<int[]> GetGradesByGroupAsync(GradesGroupFilter filter, CancellationToken cancellationToken)
        {
            return _dbContext.Set<Grade>()
                .Where(g => g.Student.Group.Name == filter.GroupName && !g.Student.IsDeleted)
                .Select(g => g.Value)
                .Distinct()
                .OrderBy(v => v)
                .ToArrayAsync(cancellationToken);
        }

        public Task<Student[]> GetStudentsAsync(StudentFilter filter, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<Student>().AsNoTracking().AsQueryable();

            if (filter.GroupId.HasValue)
            {
                query = query.Where(s => s.GroupId == filter.GroupId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.GroupName))
            {
                query = query.Where(s => s.Group.Name == filter.GroupName);
            }

            if (!string.IsNullOrWhiteSpace(filter.Fio))
            {
                query = query.Where(s =>
                    (s.LastName + " " + s.FirstName).Contains(filter.Fio)
                    || s.LastName.Contains(filter.Fio)
                    || s.FirstName.Contains(filter.Fio));
            }

            if (filter.IsDeleted.HasValue)
            {
                query = query.Where(s => s.IsDeleted == filter.IsDeleted.Value);
            }

            return query.ToArrayAsync(cancellationToken);
        }

        public async Task<Student> AddStudentAsync(StudentPayload payload, CancellationToken cancellationToken)
        {
            var student = new Student
            {
                FirstName = payload.FirstName,
                LastName = payload.LastName,
                GroupId = payload.GroupId
            };

            await _dbContext.Set<Student>().AddAsync(student, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return student;
        }

        public async Task<Student?> UpdateStudentAsync(StudentPayload payload, CancellationToken cancellationToken)
        {
            if (!payload.StudentId.HasValue)
            {
                return null;
            }

            var student = await _dbContext.Set<Student>()
                .FirstOrDefaultAsync(s => s.StudentId == payload.StudentId.Value, cancellationToken);

            if (student == null)
            {
                return null;
            }

            student.FirstName = payload.FirstName;
            student.LastName = payload.LastName;
            student.GroupId = payload.GroupId;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return student;
        }

        public async Task<bool> DeleteStudentAsync(int studentId, CancellationToken cancellationToken)
        {
            var student = await _dbContext.Set<Student>()
                .FirstOrDefaultAsync(s => s.StudentId == studentId, cancellationToken);

            if (student == null)
            {
                return false;
            }

            student.IsDeleted = true;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
