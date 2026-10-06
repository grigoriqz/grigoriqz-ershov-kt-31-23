using GrigoriqzErshovKt_31_23.Filters.GroupFilters;
using GrigoriqzErshovKt_31_23.Interfaces.GroupsInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrigoriqzErshovKt_31_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetGroupsAsync(GroupFilter filter, CancellationToken cancellationToken)
        {
            return Ok(await _groupService.GetGroupsAsync(filter, cancellationToken));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddGroupAsync(GroupPayload payload, CancellationToken cancellationToken)
        {
            return Ok(await _groupService.AddGroupAsync(payload, cancellationToken));
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateGroupAsync(GroupPayload payload, CancellationToken cancellationToken)
        {
            var group = await _groupService.UpdateGroupAsync(payload, cancellationToken);
            return group == null ? NotFound() : Ok(group);
        }

        [HttpPost("delete/{groupId:int}")]
        public async Task<IActionResult> DeleteGroupAsync(int groupId, CancellationToken cancellationToken)
        {
            var deleted = await _groupService.DeleteGroupAsync(groupId, cancellationToken);
            return deleted ? Ok() : NotFound();
        }
    }
}
