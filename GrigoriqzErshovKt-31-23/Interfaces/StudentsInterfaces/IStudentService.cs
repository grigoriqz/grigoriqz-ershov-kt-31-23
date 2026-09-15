using GrigoriqzErshovKt_31_23.Database;
using GrigoriqzErshovKt_31_23.Filters.StudentFilters;
using GrigoriqzErshovKt_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace GrigoriqzErshovKt_31_23.Interfaces.StudentsInterfaces
{
    public interface IStudentService
    {
        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
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
            var students = _dbContext.Set<Student>()
                .Where(s => s.Group.Name == filter.GroupName/* && !s.IsDeleted*/)
                .ToArrayAsync(cancellationToken);

            return students;
        }
    }
}