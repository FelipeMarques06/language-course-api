using LanguageCourse.Domain.Repositories;
using LanguageCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageCourse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourse.Infrastructure.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly AppDbContext _dbContext;
        public StudentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(Student student)
        {
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var selectedStudent = _dbContext.Set<Student>().FirstOrDefault(x => x.Id == id);
            if (selectedStudent == null)
            {
                throw new Exception($"Student with ID {id} not found.");
            }
            _dbContext.Students.Remove(selectedStudent);
            _dbContext.SaveChanges();
        }

        public List<Student> GetAll()
        {
            return _dbContext.Students.OrderBy(student => student.Id).ToList();
        }

        public Student GetById(int id)
        {
            var selectedStudent = _dbContext.Set<Student>().FirstOrDefault(x => x.Id == id);
            if (selectedStudent == null)
            {
                throw new Exception($"Student with ID {id} not found.");
            }
            return selectedStudent;
        }

        public void Update(int id, Student student)
        {
            var selectedStudent = _dbContext.Set<Student>().FirstOrDefault(x => x.Id == id);
            if (selectedStudent == null)
            {
                throw new Exception($"Student with ID {id} not found.");
            }

            _dbContext.Entry(selectedStudent).State = EntityState.Detached;
            _dbContext.Entry(student).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
