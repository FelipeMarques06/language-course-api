using LanguageCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourse.Domain.Repositories
{
    public interface IAcademicClassRepository : IRepository<AcademicClass>
    {
        public bool IsClassFull(int id);
    }
}
