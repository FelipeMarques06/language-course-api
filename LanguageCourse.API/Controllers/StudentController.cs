using LanguageCourse.Application.Dtos;
using LanguageCourse.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourse.API.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;
        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpPost("CreateStudent")]
        public IActionResult Create([FromBody]StudentDtoRequest student)
        {
            try
            {
                _studentService.Create(student);
                return Ok("Student created successfully!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var student = _studentService.GetById(id);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("UpdateStudent/{id}")]
        public IActionResult Update(int id, [FromBody]StudentDtoRequest updatedStudent)
        {
            try
            {
                _studentService.Update(id, updatedStudent);
                return Ok("Student updated successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteStudent/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _studentService.Delete(id);
                return Ok("Student deleted successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("ListStudents")]
        public IActionResult List()
        {
            try
            {
                var students = _studentService.GetAll();
                return Ok(students);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
