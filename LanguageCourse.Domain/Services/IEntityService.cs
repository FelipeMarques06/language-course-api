using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourse.Domain.Services
{
    public interface IEntityService<TDto, TEntity>
    {
        void Create(TDto dto);
        void Update(int id, TDto dto);
        void Delete(int id);
    }
}
