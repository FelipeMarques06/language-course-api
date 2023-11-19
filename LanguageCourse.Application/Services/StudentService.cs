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
        private readonly IRepository<Student> _repository;

        public StudentService(IRepository<Student> repository)
        {
            _repository = repository;
        }

        public void Create(StudentDtoRequest dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                Cpf = dto.Cpf,
                Email = dto.Email,
            };
            _repository.Create(student);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<StudentDto> GetAll()
        {
            var students = _repository.GetAll();
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
            var student = _repository.GetById(id);
          
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

            _repository.Update(id, updatedStudent);
        }
    }
}
