﻿using LanguageCourse.Application.Dtos;
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
    public class AcademicClassService : IEntityService<AcademicClassDtoRequest, AcademicClass>
    {
        private readonly IAcademicClassRepository _repository;
        private readonly EnrollmentService _enrollmentService;
        public AcademicClassService(IAcademicClassRepository repository, EnrollmentService enrollmentService)
        {
            _repository = repository;
            _enrollmentService = enrollmentService;
        }
        public void Create(AcademicClassDtoRequest dto)
        {
            var academicClass = new AcademicClass
            {
                Name = dto.Name,
                AcademicYear = dto.AcademicYear,
            };
            _repository.Create(academicClass);
        }

        public void Delete(int id)
        {
            //Checking if class has any student enrolled before deleting and finally deleting
            _enrollmentService.ClassHasEnrolledStudents(id);
            _repository.Delete(id);
        }

        public List<AcademicClassDto> GetAll()
        {
            var academicClasses = _repository.GetAll();
            var dto = academicClasses.Select(academicClass => new AcademicClassDto
            {
                Id = academicClass.Id,
                Name = academicClass.Name,
                AcademicYear = academicClass.AcademicYear,

            }).ToList();

            return dto;
        }

        public AcademicClassDto GetById(int id)
        {
            var academicClass = _repository.GetById(id);

            var retrievedClass = new AcademicClassDto
            {
                Id = academicClass.Id,
                Name = academicClass.Name,
                AcademicYear = academicClass.AcademicYear,
            };
            return retrievedClass;
        }

        public void Update(int id, AcademicClassDtoRequest dto)
        {
            var selectedClass = GetById(id);

            var updatedClass = new AcademicClass();
            updatedClass.Id = selectedClass.Id;

            if (dto.Name != null)
            {
                updatedClass.Name = dto.Name;
            }
            else
            {
                updatedClass.Name = selectedClass.Name;
            }

            if (dto.AcademicYear != 0)
            {
                updatedClass.AcademicYear = dto.AcademicYear;
            }
            else
            {
                updatedClass.AcademicYear = selectedClass.AcademicYear;
            }

            _repository.Update(id, updatedClass);
        }

        public void ValidateAcademicClass(List<int> AcademicClassIds)
        {
            HashSet<int> seenClassIds = new HashSet<int>();
            foreach (var classId in AcademicClassIds)
            {
                var academicClass = _repository.GetById(classId);

                if (academicClass == null)
                {
                    throw new ArgumentException($"Class {classId} does not exist.");
                }
                if (!seenClassIds.Add(classId))
                {
                    throw new ArgumentException($"You tried enrolling a student in class {classId} twice or more.");
                }
                if (_repository.IsClassFull(classId))
                {
                    throw new ArgumentException($"Class {classId} already has 5 students.");
                }
            }
        }
    }
}
