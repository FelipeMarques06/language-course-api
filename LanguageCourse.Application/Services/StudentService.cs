using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageCourse.Application.Dtos;
using LanguageCourse.Domain.Entities;
using LanguageCourse.Domain.Repositories;
using LanguageCourse.Domain.Services;
using DocumentValidator;
using System.Text.RegularExpressions;

namespace LanguageCourse.Application.Services
{
    public class StudentService : IEntityService<StudentDtoRequest, Student>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly AcademicClassService _academicClassService;
        private readonly EnrollmentService _enrollmentService;

        public StudentService(IStudentRepository studentRepository,
                             AcademicClassService academicClassService,
                             EnrollmentService enrollmentService)
        {
            _studentRepository = studentRepository;
            _academicClassService = academicClassService;
            _enrollmentService = enrollmentService;
        }

        public void Create(StudentDtoRequest dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                Cpf = new string(dto.Cpf.Where(char.IsDigit).ToArray()), //Removing characters that are not numeric
                Email = dto.Email,
                Enrollments = new List<Enrollment>()
            };
            if (dto.AcademicClassIds == null || !dto.AcademicClassIds.Any())
            {
                throw new ArgumentException("At least one class must be specified to create the student.");
            }

            //Validating CPF format
            if (!CpfValidation.Validate(student.Cpf))
            {
                throw new ArgumentException("CPF is invalid, please check the document information again.");
            }

            //Checking if CPF was already registered
            if (_studentRepository.CpfAlreadyExists(student.Cpf))
            {
                throw new ArgumentException("This CPF is already registered. CPFs must be unique");
            }

            //Checking if email is in a valid format
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(student.Email, pattern))
            {
                throw new ArgumentException("Email format is not valid.");
            }

            //Validate Academic Class for duplicates, if it exists and if it's full
            _academicClassService.ValidateAcademicClass(dto.AcademicClassIds);

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
            _studentRepository.GetById(id);
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
            if(student.AcademicClassIds != null)
            {
                //Getting sure first to make Academic Classes validations and the student is not already enrolled in any Academic Class from the request
                _academicClassService.ValidateAcademicClass(student.AcademicClassIds);
                foreach (var classId in student.AcademicClassIds)
                {
                    _enrollmentService.IsStudentEnrolled(updatedStudent.Id, classId);                 
                }

                //Finally attaching the new Academic Classes to an already existent student            
                foreach (var classId in student.AcademicClassIds)
                {
                    var enrollment = new EnrollmentDtoRequest();
                    enrollment.StudentId = updatedStudent.Id;
                    enrollment.AcademicClassId = classId;
                    _enrollmentService.Create(enrollment);
                }
            }
            _studentRepository.Update(id, updatedStudent);
        }
    }
}
