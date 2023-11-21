using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageCourse.Application.Dtos;
using LanguageCourse.Domain.Entities;
using LanguageCourse.Domain.Repositories;
using LanguageCourse.Domain.Services;

namespace LanguageCourse.Application.Services
{
    public class StudentService : IEntityService<StudentDtoRequest, Student>
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<AcademicClass> _academicClassRepository;

        public StudentService(IRepository<Student> studentRepository, IRepository<AcademicClass> academicClassRepository)
        {
            _studentRepository = studentRepository;
            _academicClassRepository = academicClassRepository;
        }

        public void Create(StudentDtoRequest dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                Cpf = dto.Cpf,
                Email = dto.Email,
                Enrollments = new List<Enrollment>()
            };
            if (dto.AcademicClassIds == null || !dto.AcademicClassIds.Any())
            {
                throw new ArgumentException("At least one class must be specified to create the student.");
            }

            //Checking if Academic Class exists
            foreach (var classId in dto.AcademicClassIds)
            {
                _academicClassRepository.GetById(classId);
            }

            //Attaching Student and Class to Enrollment
            foreach (var classId in dto.AcademicClassIds)
            {
                var enrollment = new Enrollment
                {
                    AcademicClassId = classId
                };
                student.Enrollments.Add(enrollment);
            }

            _studentRepository.Create(student);
        }

        public void Delete(int id)
        {
            var student = _studentRepository.GetById(id);
            _studentRepository.Delete(id);
        }

        public List<StudentDto> GetAll()
        {
            var students = _studentRepository.GetAll();
            var studentDtos = students.Select(student => new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Cpf = student.Cpf,
                Email = student.Email

            }).ToList();

            return studentDtos;
        }

        public StudentDto GetById(int id)
        {
            var student = _studentRepository.GetById(id);
          
            var retrievedStudent = new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Cpf = student.Cpf,
                Email = student.Email
            };
            return retrievedStudent;
        }

        public void Update(int id, StudentDtoRequest student)
        {
            var selectedStudent = GetById(id);
           
            var updatedStudent = new Student();
            updatedStudent.Id = selectedStudent.Id;

            if (student.Name != null)
            {
                updatedStudent.Name = student.Name;
            }
            else
            {
                updatedStudent.Name = selectedStudent.Name;
            }

            if (student.Cpf != null)
            {
                updatedStudent.Cpf = student.Cpf;
            }
            else
            {
                updatedStudent.Cpf = selectedStudent.Cpf;
            }

            if (student.Email != null)
            {
                updatedStudent.Email = student.Email;
            }
            else
            {
                updatedStudent.Email = selectedStudent.Email;
            }

            _studentRepository.Update(id, updatedStudent);
        }
    }
}
