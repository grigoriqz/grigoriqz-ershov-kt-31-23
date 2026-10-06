using GrigoriqzErshovKt_31_23.Filters.StudentFilters;
using GrigoriqzErshovKt_31_23.Interfaces.StudentsInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrigoriqzErshovKt_31_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _studentService;

        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        [HttpPost("GetStudentByGroup")]
        public async Task<IActionResult> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken)
        {
            var students = await _studentService.GetStudentsByGroupAsync(filter, cancellationToken);

            return Ok(students);
        }

        [HttpPost("GetGradesByGroupp")]
        public async Task<IActionResult> GetGradesByGroupAsync(GradesGroupFilter filter, CancellationToken cancellationToken)
        {
            var grades = await _studentService.GetGradesByGroupAsync(filter, cancellationToken);

            return Ok(grades);
        }
    }
}