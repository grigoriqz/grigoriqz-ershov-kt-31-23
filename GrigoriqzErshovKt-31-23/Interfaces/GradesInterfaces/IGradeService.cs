using GrigoriqzErshovKt_31_23.Database;
using GrigoriqzErshovKt_31_23.Filters.GradeFilters;
using GrigoriqzErshovKt_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace GrigoriqzErshovKt_31_23.Interfaces.GradesInterfaces
{
    public class AverageScoreResult
    {
        public double Average { get; set; }
        public int Count { get; set; }
    }

    public interface IGradeService
    {
        Task<AverageScoreResult> GetAverageByDisciplineInGroupAsync(PerformanceFilter filter, CancellationToken cancellationToken);
        Task<Grade[]> GetStudentGradesAsync(PerformanceFilter filter, CancellationToken cancellationToken);
        Task<AverageScoreResult> GetAverageByCourseAsync(PerformanceFilter filter, CancellationToken cancellationToken);
        Task<Grade> AddOrUpdateGradeAsync(GradePayload payload, CancellationToken cancellationToken);
    }

    public class GradeService : IGradeService
    {
        private readonly StudentDbContext _dbContext;

        public GradeService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AverageScoreResult> GetAverageByDisciplineInGroupAsync(PerformanceFilter filter, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<Grade>().Where(g => !g.Student.IsDeleted);

            if (filter.GroupId.HasValue)
            {
                query = query.Where(g => g.Student.GroupId == filter.GroupId.Value);
            }

            if (filter.DisciplineId.HasValue)
            {
                query = query.Where(g => g.DisciplineId == filter.DisciplineId.Value);
            }

            var values = await query.Select(g => g.Value).ToArrayAsync(cancellationToken);

            return new AverageScoreResult
            {
                Average = values.Length == 0 ? 0 : Math.Round(values.Average(), 2),
                Count = values.Length
            };
        }

        public Task<Grade[]> GetStudentGradesAsync(PerformanceFilter filter, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<Grade>().AsNoTracking().AsQueryable();

            if (filter.StudentId.HasValue)
            {
                query = query.Where(g => g.StudentId == filter.StudentId.Value);
            }

            if (filter.DisciplineId.HasValue)
            {
                query = query.Where(g => g.DisciplineId == filter.DisciplineId.Value);
            }

            return query.ToArrayAsync(cancellationToken);
        }

        public async Task<AverageScoreResult> GetAverageByCourseAsync(PerformanceFilter filter, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<Grade>()
                .Where(g => !g.Student.IsDeleted && !g.Student.Group.IsDeleted);

            if (filter.Course.HasValue)
            {
                query = query.Where(g => g.Student.Group.Course == filter.Course.Value);
            }

            var values = await query.Select(g => g.Value).ToArrayAsync(cancellationToken);

            return new AverageScoreResult
            {
                Average = values.Length == 0 ? 0 : Math.Round(values.Average(), 2),
                Count = values.Length
            };
        }

        public async Task<Grade> AddOrUpdateGradeAsync(GradePayload payload, CancellationToken cancellationToken)
        {
            var grade = await _dbContext.Set<Grade>()
                .FirstOrDefaultAsync(g => g.StudentId == payload.StudentId && g.DisciplineId == payload.DisciplineId, cancellationToken);

            if (grade == null)
            {
                grade = new Grade
                {
                    StudentId = payload.StudentId,
                    DisciplineId = payload.DisciplineId,
                    Value = payload.Value
                };
                await _dbContext.Set<Grade>().AddAsync(grade, cancellationToken);
            }
            else
            {
                grade.Value = payload.Value;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return grade;
        }
    }
}
