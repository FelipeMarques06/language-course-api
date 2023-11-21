using LanguageCourse.Application.Dtos;
using LanguageCourse.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourse.API.Controllers
{
    [Route("api/[controller]")]
    public class EnrollmentController : Controller
    {
        private readonly EnrollmentService _enrollmentService;
        public EnrollmentController(EnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }
        [HttpPost("CreateEnrollment")]
        public IActionResult Create([FromBody] EnrollmentDtoRequest enrollment)
        {
            try
            {
                _enrollmentService.Create(enrollment);
                return Ok("Enrollment created successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var enrollment = _enrollmentService.GetById(id);
                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("DeleteEnrollment/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _enrollmentService.Delete(id);
                return Ok("Enrollment deleted successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ListEnrollments")]
        public IActionResult List()
        {
            try
            {
                var enrollments = _enrollmentService.GetAll();
                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
