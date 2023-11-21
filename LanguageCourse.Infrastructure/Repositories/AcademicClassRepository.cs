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
    public class AcademicClassRepository : IRepository<AcademicClass>
    {
        private readonly AppDbContext _dbContext;

        public AcademicClassRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(AcademicClass academicClass)
        {
            try
            {
                _dbContext.AcademicClass.Add(academicClass);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            
        }

        public void Delete(int id)
        {
            var selectedClass = _dbContext.Set<AcademicClass>().FirstOrDefault(x => x.Id == id);
            if (selectedClass == null)
            {
                throw new Exception($"Class with ID {id} not found.");
            }
            _dbContext.AcademicClass.Remove(selectedClass);
            _dbContext.SaveChanges();
        }

        public List<AcademicClass> GetAll()
        {
            return _dbContext.AcademicClass.OrderBy(x => x.Id).ToList();
        }

        public AcademicClass GetById(int id)
        {
            var selectedClass = _dbContext.Set<AcademicClass>().FirstOrDefault(x => x.Id == id);
            if (selectedClass == null)
            {
                throw new Exception($"Class with ID {id} not found.");
            }
            return selectedClass;
        }

        public void Update(int id, AcademicClass entity)
        {
            var selectedClass = _dbContext.Set<AcademicClass>().FirstOrDefault(x => x.Id == id);
            if (selectedClass == null)
            {
                throw new Exception($"Class with ID {id} not found.");
            }

            _dbContext.Entry(selectedClass).State = EntityState.Detached;
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
