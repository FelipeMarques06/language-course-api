using LanguageCourse.Application.Dtos;
using LanguageCourse.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourse.API.Controllers
{
    [Route("api/[controller]")]
    public class AcademicClassController : Controller
    {
        private readonly AcademicClassService _academicClassService;
        public AcademicClassController(AcademicClassService academicClassService)
        {
            _academicClassService = academicClassService;
        }
        [HttpPost("CreateStudent")]
        public IActionResult Create([FromBody] AcademicClassDtoRequest academicClass)
        {
            try
            {
                _academicClassService.Create(academicClass);
                return Ok("Class created successfully!");
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
                var academicClass = _academicClassService.GetById(id);
                return Ok(academicClass);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("UpdateClass/{id}")]
        public IActionResult Update(int id, [FromBody] AcademicClassDtoRequest updatedClass)
        {
            try
            {
                _academicClassService.Update(id, updatedClass);
                return Ok("Class updated successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteClass/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _academicClassService.Delete(id);
                return Ok("Class deleted successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("ListClasses")]
        public IActionResult List()
        {
            try
            {
                var academicClasses = _academicClassService.GetAll();
                return Ok(academicClasses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
