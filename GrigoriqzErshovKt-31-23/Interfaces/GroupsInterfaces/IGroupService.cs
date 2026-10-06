using GrigoriqzErshovKt_31_23.Database;
using GrigoriqzErshovKt_31_23.Filters.GroupFilters;
using GrigoriqzErshovKt_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace GrigoriqzErshovKt_31_23.Interfaces.GroupsInterfaces
{
    public interface IGroupService
    {
        Task<Group[]> GetGroupsAsync(GroupFilter filter, CancellationToken cancellationToken);
        Task<Group> AddGroupAsync(GroupPayload payload, CancellationToken cancellationToken);
        Task<Group?> UpdateGroupAsync(GroupPayload payload, CancellationToken cancellationToken);
        Task<bool> DeleteGroupAsync(int groupId, CancellationToken cancellationToken);
    }

    public class GroupService : IGroupService
    {
        private readonly StudentDbContext _dbContext;

        public GroupService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Group[]> GetGroupsAsync(GroupFilter filter, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<Group>().AsNoTracking().AsQueryable();

            if (filter.SpecialtyId.HasValue)
            {
                query = query.Where(g => g.SpecialtyId == filter.SpecialtyId.Value);
            }

            if (filter.Course.HasValue)
            {
                query = query.Where(g => g.Course == filter.Course.Value);
            }

            if (filter.IsDeleted.HasValue)
            {
                query = query.Where(g => g.IsDeleted == filter.IsDeleted.Value);
            }

            return query.ToArrayAsync(cancellationToken);
        }

        public async Task<Group> AddGroupAsync(GroupPayload payload, CancellationToken cancellationToken)
        {
            var group = new Group
            {
                Name = payload.Name,
                Course = payload.Course,
                SpecialtyId = payload.SpecialtyId
            };

            await _dbContext.Set<Group>().AddAsync(group, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return group;
        }

        public async Task<Group?> UpdateGroupAsync(GroupPayload payload, CancellationToken cancellationToken)
        {
            if (!payload.GroupId.HasValue)
            {
                return null;
            }

            var group = await _dbContext.Set<Group>()
                .FirstOrDefaultAsync(g => g.GroupId == payload.GroupId.Value, cancellationToken);

            if (group == null)
            {
                return null;
            }

            group.Name = payload.Name;
            group.Course = payload.Course;
            group.SpecialtyId = payload.SpecialtyId;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return group;
        }

        public async Task<bool> DeleteGroupAsync(int groupId, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Set<Group>()
                .Include(g => g.Students)
                .FirstOrDefaultAsync(g => g.GroupId == groupId, cancellationToken);

            if (group == null)
            {
                return false;
            }

            group.IsDeleted = true;
            foreach (var student in group.Students)
            {
                student.IsDeleted = true;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
