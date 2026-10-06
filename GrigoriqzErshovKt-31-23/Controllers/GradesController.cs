using GrigoriqzErshovKt_31_23.Filters.GradeFilters;
using GrigoriqzErshovKt_31_23.Interfaces.GradesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrigoriqzErshovKt_31_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradesController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpPost("average-by-discipline-in-group")]
        public async Task<IActionResult> GetAverageByDisciplineInGroupAsync(PerformanceFilter filter, CancellationToken cancellationToken)
        {
            return Ok(await _gradeService.GetAverageByDisciplineInGroupAsync(filter, cancellationToken));
        }

        [HttpPost("student")]
        public async Task<IActionResult> GetStudentGradesAsync(PerformanceFilter filter, CancellationToken cancellationToken)
        {
            return Ok(await _gradeService.GetStudentGradesAsync(filter, cancellationToken));
        }

        [HttpPost("average-by-course")]
        public async Task<IActionResult> GetAverageByCourseAsync(PerformanceFilter filter, CancellationToken cancellationToken)
        {
            return Ok(await _gradeService.GetAverageByCourseAsync(filter, cancellationToken));
        }

        [HttpPost("add-or-update")]
        public async Task<IActionResult> AddOrUpdateGradeAsync(GradePayload payload, CancellationToken cancellationToken)
        {
            return Ok(await _gradeService.AddOrUpdateGradeAsync(payload, cancellationToken));
        }
    }
}
