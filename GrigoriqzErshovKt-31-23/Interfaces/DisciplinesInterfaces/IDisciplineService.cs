using GrigoriqzErshovKt_31_23.Database;
using GrigoriqzErshovKt_31_23.Filters.DisciplineFilters;
using GrigoriqzErshovKt_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace GrigoriqzErshovKt_31_23.Interfaces.DisciplinesInterfaces
{
    public interface IDisciplineService
    {
        Task<Discipline[]> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken);
        Task<Discipline> AddDisciplineAsync(DisciplinePayload payload, CancellationToken cancellationToken);
        Task<Discipline?> UpdateDisciplineAsync(DisciplinePayload payload, CancellationToken cancellationToken);
        Task<bool> DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken);
    }

    public class DisciplineService : IDisciplineService
    {
        private readonly StudentDbContext _dbContext;

        public DisciplineService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Discipline[]> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<Discipline>().AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(d => d.Name == filter.Name);
            }

            if (filter.IsDeleted.HasValue)
            {
                query = query.Where(d => d.IsDeleted == filter.IsDeleted.Value);
            }

            return query.ToArrayAsync(cancellationToken);
        }

        public async Task<Discipline> AddDisciplineAsync(DisciplinePayload payload, CancellationToken cancellationToken)
        {
            var discipline = new Discipline { Name = payload.Name };
            await _dbContext.Set<Discipline>().AddAsync(discipline, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return discipline;
        }

        public async Task<Discipline?> UpdateDisciplineAsync(DisciplinePayload payload, CancellationToken cancellationToken)
        {
            if (!payload.DisciplineId.HasValue)
            {
                return null;
            }

            var discipline = await _dbContext.Set<Discipline>()
                .FirstOrDefaultAsync(d => d.DisciplineId == payload.DisciplineId.Value, cancellationToken);

            if (discipline == null)
            {
                return null;
            }

            discipline.Name = payload.Name;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return discipline;
        }

        public async Task<bool> DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken)
        {
            var discipline = await _dbContext.Set<Discipline>()
                .FirstOrDefaultAsync(d => d.DisciplineId == disciplineId, cancellationToken);

            if (discipline == null)
            {
                return false;
            }

            discipline.IsDeleted = true;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
