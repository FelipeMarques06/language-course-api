using LanguageCourse.Application.Dtos;
using LanguageCourse.Domain.Entities;
using LanguageCourse.Domain.Repositories;
using LanguageCourse.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourse.Application.Services
{
    public class EnrollmentService : IEntityService<EnrollmentDtoRequest, Enrollment>
    {
        private readonly IEnrollmentRepository _repository;
        public EnrollmentService(IEnrollmentRepository repository)
        {
            _repository = repository;
        }
        public void Create(EnrollmentDtoRequest dto)
        {
            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                AcademicClassId = dto.AcademicClassId,
            };
            IsStudentEnrolled(enrollment.StudentId, enrollment.AcademicClassId);
            _repository.Create(enrollment);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<EnrollmentDto> GetAll()
        {
            var enrollments = _repository.GetAll();
            var enrollmentDtos = enrollments.Select(enrollment => new EnrollmentDto
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                AcademicClassId = enrollment.AcademicClassId,

            }).ToList();

            return enrollmentDtos;
        }

        public EnrollmentDto GetById(int id)
        {
            var enrollment = _repository.GetById(id);

            var retrievedEnrollment = new EnrollmentDto
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                AcademicClassId = enrollment.AcademicClassId,
            };
            return retrievedEnrollment;
        }

        public void Update(int id, EnrollmentDtoRequest enrollment)
        {
            throw new NotImplementedException();
        }

        public void IsStudentEnrolled(int studentId, int academicClassId)
        {
            if (_repository.IsStudentEnrolled(studentId, academicClassId))
            {
                throw new Exception($"Student with ID {studentId} is already enrolled in Class {academicClassId}");
            }       
        }
    }
}
