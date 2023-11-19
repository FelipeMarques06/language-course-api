using LanguageCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourse.Domain.Repositories
{
    public interface IRepository<T>
    {
        T GetById(int id);
        List<T> GetAll();
        void Create(T entity);
        void Update(int id, T entity);
        void Delete(int id);
    }
}
