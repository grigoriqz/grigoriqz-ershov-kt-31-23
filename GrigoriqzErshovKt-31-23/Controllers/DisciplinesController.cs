using GrigoriqzErshovKt_31_23.Filters.DisciplineFilters;
using GrigoriqzErshovKt_31_23.Interfaces.DisciplinesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrigoriqzErshovKt_31_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly IDisciplineService _disciplineService;

        public DisciplinesController(IDisciplineService disciplineService)
        {
            _disciplineService = disciplineService;
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken)
        {
            return Ok(await _disciplineService.GetDisciplinesAsync(filter, cancellationToken));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDisciplineAsync(DisciplinePayload payload, CancellationToken cancellationToken)
        {
            return Ok(await _disciplineService.AddDisciplineAsync(payload, cancellationToken));
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateDisciplineAsync(DisciplinePayload payload, CancellationToken cancellationToken)
        {
            var discipline = await _disciplineService.UpdateDisciplineAsync(payload, cancellationToken);
            return discipline == null ? NotFound() : Ok(discipline);
        }

        [HttpPost("delete/{disciplineId:int}")]
        public async Task<IActionResult> DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken)
        {
            var deleted = await _disciplineService.DeleteDisciplineAsync(disciplineId, cancellationToken);
            return deleted ? Ok() : NotFound();
        }
    }
}
