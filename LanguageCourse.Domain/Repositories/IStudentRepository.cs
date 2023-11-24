using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageCourse.Domain.Entities;

namespace LanguageCourse.Domain.Repositories
{
    public interface IStudentRepository: IRepository<Student>
    {
        public bool CpfAlreadyExists(string cpf);
    }
}
