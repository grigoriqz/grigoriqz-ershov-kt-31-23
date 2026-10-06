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

        [HttpPost("GetGradesByGroup")]
        public async Task<IActionResult> GetGradesByGroupAsync(GradesGroupFilter filter, CancellationToken cancellationToken)
        {
            var grades = await _studentService.GetGradesByGroupAsync(filter, cancellationToken);
            return Ok(grades);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetStudentsAsync(StudentFilter filter, CancellationToken cancellationToken)
        {
            return Ok(await _studentService.GetStudentsAsync(filter, cancellationToken));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddStudentAsync(StudentPayload payload, CancellationToken cancellationToken)
        {
            return Ok(await _studentService.AddStudentAsync(payload, cancellationToken));
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateStudentAsync(StudentPayload payload, CancellationToken cancellationToken)
        {
            var student = await _studentService.UpdateStudentAsync(payload, cancellationToken);
            return student == null ? NotFound() : Ok(student);
        }

        [HttpPost("delete/{studentId:int}")]
        public async Task<IActionResult> DeleteStudentAsync(int studentId, CancellationToken cancellationToken)
        {
            var deleted = await _studentService.DeleteStudentAsync(studentId, cancellationToken);
            return deleted ? Ok() : NotFound();
        }
    }
}
