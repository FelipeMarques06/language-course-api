using LanguageCourse.Domain.Entities;
using LanguageCourse.Domain.Repositories;
using LanguageCourse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourse.Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly AppDbContext _dbContext;
        public EnrollmentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(Enrollment entity)
        {
            _dbContext.Enrollment.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var selectedEnrollment = _dbContext.Set<Enrollment>().FirstOrDefault(x => x.Id == id);
            if (selectedEnrollment == null)
            {
                throw new Exception($"Enrollment with ID {id} not found.");
            }
            _dbContext.Enrollment.Remove(selectedEnrollment);
            _dbContext.SaveChanges();
        }

        public List<Enrollment> GetAll()
        {
            return _dbContext.Enrollment.OrderBy(x => x.Id).ToList();
        }

        public Enrollment GetById(int id)
        {
            var selectedEnrollment = _dbContext.Set<Enrollment>().FirstOrDefault(x => x.Id == id);
            if (selectedEnrollment == null)
            {
                throw new Exception($"Enrollment with ID {id} not found.");
            }
            return selectedEnrollment;
        }

        public void Update(int id, Enrollment entity)
        {
            var selectedEnrollment = _dbContext.Set<Enrollment>().FirstOrDefault(x => x.Id == id);
            if (selectedEnrollment == null)
            {
                throw new Exception($"Enrollment with ID {id} not found.");
            }

            _dbContext.Entry(selectedEnrollment).State = EntityState.Detached;
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public bool IsStudentEnrolled(int studentId, int academicClassId)
        {
            return _dbContext.Enrollment
                .Any(e => e.StudentId == studentId && e.AcademicClassId == academicClassId);
        }

        public bool ClassHasEnrolledStudents(int classId)
        {
            return _dbContext.Enrollment.Any(e => e.AcademicClassId == classId);
        }
    }
}
